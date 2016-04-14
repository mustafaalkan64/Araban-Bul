using Araba.Core.Domain.DbTables;
using System;
using System.Linq;

namespace Araba.Service.UserServices
{
    public interface IUserService : IGenericService<User>
    {
        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User FindByUserNameAndPassword(string userName, string password);

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="confirmationId"></param>
        /// <returns></returns>
        User FindByConfirmationId(Guid confirmationId);

        /// <summary>
        /// Kullanıcı Tipleri.
        /// </summary>
        /// <returns></returns>
        IQueryable<UserType> GetUserTypes();

        /// <summary>
        /// Yeni üye olan kullanıcıya onay mesajı gönder.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfirmationUrl"></param>
        /// <returns>Email send success status</returns>
        bool SendConfirmationMail(User user, string ConfirmationUrl);

        /// <summary>
        /// Eposta sistemde kayıtlı mı.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool ValidateEmail(string email);

        /// <summary>
        /// Kullanıcı adı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool ValidateUserName(string userName);
    }
}
