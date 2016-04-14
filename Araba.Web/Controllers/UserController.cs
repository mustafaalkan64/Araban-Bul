using Araba.Core.Domain.DbTables;
using Araba.Data.UnitOfWork;
using Araba.Service.LocationServices;
using Araba.Service.UserServices;
using Araba.Utilities.ImageOperations;
using Araba.Web.Framework.Membership;
using Araba.Web.Models;
using Best.Web.Framework.Controllers;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Araba.Web.Controllers
{
    public class UserController : AuthorizedController
    {
        private readonly IUserService _userService;
        private readonly ILocationService _locationService;

        public UserController(IUnitOfWork uow, IUserService userService, ILocationService locationService)
            : base(uow)
        {
            _userService = userService;
            _locationService = locationService;
        }

        public ActionResult AddCar() {
            return View();
        }

        public ActionResult UserProfile()
        {
            var user = _userService.Find(CustomMembership.CurrentUser().Id);

            return View(user);
        }

        public ActionResult EditProfile()
        {
            var model = new RegisterModel();
            var user = _userService.Find(CustomMembership.CurrentUser().Id);

            model.Adress = user.Adress;
            model.CityId = user.District != null ? user.District.CityId : 0;
            model.Description = user.Description;
            model.DistrictId = user.District != null ? user.DistrictId : 0;
            model.Fax = user.Fax;
            model.ImgUrl = user.ImgUrlSmall;
            model.Phone1 = user.Phone1;
            model.Phone2 = user.Phone2;
            model.Phone3 = user.Phone3;
            model.RealName = user.RealName;
            model.Street = user.Street;
            model.Website = user.Website;
            
            // zorunlu alanlar modelState hata 
            // vermesin diye intialize ediliyor
            model.Email = user.Email;
            model.Id = user.Id;
            model.Password = user.Password;
            model.UserName = user.UserName;
            model.UserTypeId = user.UserTypeId;
            model.ConfirmPassword = user.Password;

            model.Cities = _locationService.GetCities();
            model.Districts = user.District != null ? _locationService.GetDistricts(user.District.CityId) : Enumerable.Empty<District>();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(RegisterModel model)
        {
            User user = _userService.Find(model.Id);

            if (ModelState.IsValid)
            {
                try
                {
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

                    user.LastLoginDate = DateTime.Now;
                    user.LastLoginIP = Request.UserHostAddress;
                    user.Email = model.Email;
                    user.UpdateDate = DateTime.Now;
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
                    user.Website = model.Website;

                    _userService.Update(user);
                    _uow.SaveChanges();

                    return RedirectToAction("UserProfile", "User");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı düzenleme başarısız! Hata: " + ex.Message);
                }
            }

            model.Cities = _locationService.GetCities();
            model.Districts = model.DistrictId != null ? _locationService.GetDistricts(model.CityId.Value) : Enumerable.Empty<District>();

            return View(model);
        }
    }
}