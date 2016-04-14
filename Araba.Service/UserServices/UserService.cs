using Araba.Core.Domain.DbTables;
using Araba.Data.Repository;
using Araba.Data.UnitOfWork;
using System;
using System.Linq;
using System.Net.Mail;

namespace Araba.Service.UserServices
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<UserType> _userTypeRepository;

        public UserService(IUnitOfWork uow)
            : base(uow)
        {
            _userRepository = uow.GetRepository<User>();
            _userTypeRepository = uow.GetRepository<UserType>();
        }

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User FindByUserNameAndPassword(string userName, string password)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="confirmationId"></param>
        /// <returns></returns>
        public User FindByConfirmationId(Guid confirmationId)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.ConfirmationId == confirmationId);
        }

        /// <summary>
        /// Kullanıcı Tipleri.
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserType> GetUserTypes()
        {
            return _userTypeRepository.GetAll();
        }

        /// <summary>
        /// Yeni üye olan kullanıcıya onay mesajı gönder.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfirmationUrl"></param>
        /// <returns>Email send success status</returns>
        public bool SendConfirmationMail(User user, string ConfirmationUrl)
        {
            var status = false;
            string confirmationId = user.ConfirmationId.ToString();
            ConfirmationUrl += "/Account/ConfirmUser?confirmationId=" + confirmationId;

            var message = new MailMessage("info@gundemdisi.net", user.Email)
            {
                Subject = "Lütfen e-posta adresinizi onaylayınız.",
                Body = ConfirmationUrl
            };

            var client = new SmtpClient();
            try
            {
                client.Send(message);
                status = true;
            }
            catch (System.Exception)
            {
                return status;
            }

            return status;
        }

        /// <summary>
        /// Eposta sistemde kayıtlı mı.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        {
            return _userRepository.GetAll().Any(x => x.Email == email);
        }

        /// <summary>
        /// Kullanıcı adı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ValidateUserName(string userName)
        {
            return _userRepository.GetAll().Any(x => x.UserName == userName);
        }
    }
}
