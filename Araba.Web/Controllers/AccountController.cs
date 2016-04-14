using Araba.Core.Application;
using Araba.Core.Domain.DbTables;
using Araba.Data.UnitOfWork;
using Araba.Service.LocationServices;
using Araba.Service.UserServices;
using Araba.Utilities.ImageOperations;
using Araba.Web.Framework.Membership;
using Araba.Web.Models;
using Best.Web.Framework.Controllers;
using FormsAuthenticationExtensions;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace Araba.Web.Controllers
{
    public class AccountController : PublicController
    {
        private readonly IUserService _userService;
        private readonly ILocationService _locationService;

        public AccountController(IUnitOfWork uow, IUserService userService, ILocationService locationService)
            : base(uow)
        {
            _userService = userService;
            _locationService = locationService;
        }

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            var user = _userService.FindByUserNameAndPassword(model.UserName, model.Password);
            if (ModelState.IsValid && user != null)
            {
                if (!user.IsConfirmed)
                {
                    TempData[AppConstants.TempDataKey_EmailConfirm] = "E-posta adresiniz onaylı değildir. Lütfen e-posta adresinizdeki linki kullanarak e-posta adresinizi onaylayınız.";

                    return View();
                }

                var ticketData = new NameValueCollection
                {
                    { AppConstants.GeneralKey_Email, user.Email },
                    { AppConstants.GeneralKey_RealName, user.RealName },
                    { AppConstants.GeneralKey_Id, user.Id.ToString() }
                };

                new FormsAuthentication().SetAuthCookie(model.UserName, model.RememberMe, ticketData);

                //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve ya şifre geçersiz!");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();
            model.Cities = _locationService.GetCities();
            model.Districts = Enumerable.Empty<District>();
            model.UserTypes = _userService.GetUserTypes();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User();

                    if (model.Img != null && model.Img.ContentLength > 0)
                    {
                        var image = model.Img;
                        var fileName = model.UserName + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(image.FileName);

                        var imageDirectory = Server.MapPath("~/Content/Images/uploads/Users/" + model.UserName);
                        var imageDirectoryBig = Server.MapPath("~/Content/Images/uploads/Users/" + model.UserName + "/Big");
                        var imageDirectoryMiddle = Server.MapPath("~/Content/Images/uploads/Users/" + model.UserName + "/Middle");
                        var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/Users/" + model.UserName + "/Small");

                        // dizin yoksa oluştur.
                        if (!Directory.Exists(imageDirectory))
                        {
                            Directory.CreateDirectory(imageDirectory);
                            Directory.CreateDirectory(imageDirectoryBig);
                            Directory.CreateDirectory(imageDirectoryMiddle);
                            Directory.CreateDirectory(imageDirectorySmall);
                        }

                        // resmi sunucuya kaydet
                        image.SaveAs(Path.Combine(imageDirectory, fileName));

                        // resmi küçük boyutta kaydet
                        ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(150, 150), imageDirectorySmall, fileName);
                        ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(450, 450), imageDirectoryMiddle, fileName);
                        ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(750, 750), imageDirectoryBig, fileName);

                        user.ImgUrl = Path.Combine("Content/Images/uploads/Users/" + model.UserName + "/", fileName);
                        user.ImgUrlBig = Path.Combine("Content/Images/uploads/Users/" + model.UserName + "/Big/", fileName);
                        user.ImgUrlMiddle = Path.Combine("Content/Images/uploads/Users/" + model.UserName + "/Middle/", fileName);
                        user.ImgUrlSmall = Path.Combine("Content/Images/uploads/Users/" + model.UserName + "/Small/", fileName);
                    }

                    user.ConfirmationId = Guid.NewGuid();
                    user.IsConfirmed = true;
                    user.LastLoginDate = DateTime.Now;
                    user.LastLoginIP = Request.UserHostAddress;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.IsActive = true;
                    user.InsertDate = DateTime.Now;
                    user.UpdateDate = DateTime.Now;
                    user.InsertUserId = CustomMembership.CurrentUser().Id;
                    user.UpdateUserId = CustomMembership.CurrentUser().Id;
                    user.Adress = model.Adress;
                    user.Description = model.Description;
                    user.DistrictId = model.DistrictId;
                    user.Phone1 = model.Phone1;
                    user.Phone2 = model.Phone2;
                    user.Phone3 = model.Phone3;
                    user.Fax = model.Fax;
                    user.RealName = String.IsNullOrEmpty(model.RealName) ? model.UserName : model.RealName;
                    user.Street = model.Street;
                    user.UserTypeId = model.UserTypeId;
                    user.Website = model.Website;

                    _userService.Insert(user);
                    _uow.SaveChanges();
                    //_userService.SendConfirmationMail(user, Request.Url.GetLeftPart(UriPartial.Authority));

                    FormsAuthentication.SetAuthCookie(model.UserName, true);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturma başarısız! Hata: " + ex.Message);
                }
            }

            return View(model);
        }

        // RegisterModel içerisindeki Email alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateEmail(string Email)
        {
            var result = _userService.ValidateEmail(Email);

            if (result)
            {
                return Json("Girdiğiniz e-posta adresi sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        // RegisterModel içerisindeki UserName alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateUserName(string UserName)
        {
            var result = _userService.ValidateUserName(UserName);

            if (result)
            {
                return Json("Girdiğiniz kullanıcı adı sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmUser(Guid confirmationId)
        {
            if (string.IsNullOrEmpty(confirmationId.ToString()) || (!Regex.IsMatch(confirmationId.ToString(),
                   @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
            {
                TempData[AppConstants.TempDataKey_EmailConfirm] = "Hesap geçerli değil. Lütfen e-posta adresinizdeki linke tekrar tıklayınız.";

                return View();
            }
            else
            {
                var user = _userService.FindByConfirmationId(confirmationId);

                if (!user.IsConfirmed)
                {
                    user.IsConfirmed = true;
                    _userService.Update(user);
                    _uow.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    TempData[AppConstants.TempDataKey_EmailConfirm] = "E-posta adresinizi onayladığınız için teşekkürler. Artık sitemize üyesiniz.";

                    return RedirectToAction("Wellcome");
                }
                else
                {
                    TempData[AppConstants.TempDataKey_EmailConfirm] = "E-posta adresiniz zaten onaylı. Giriş yapabilirsiniz.";

                    return RedirectToAction("GirisYap");
                }
            }
        }

        #region private methods
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}