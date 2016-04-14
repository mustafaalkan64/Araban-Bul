using Araba.Core.Application;
using Araba.Core.Domain.DbTables;
using FormsAuthenticationExtensions;
using System;
using System.Web;
using System.Web.Security;

namespace Araba.Web.Framework.Membership
{
    public static class CustomMembership
    {
        public static CurrentUser CurrentUser()
        {
            var currentUser = HttpContext.Current.User;
            var ticketData = ((FormsIdentity)currentUser.Identity).Ticket.GetStructuredUserData();
            var user = new CurrentUser();

            user.Id = Int32.Parse(ticketData[AppConstants.GeneralKey_Id]);
            user.Email = ticketData[AppConstants.GeneralKey_Email];
            user.RealName = ticketData[AppConstants.GeneralKey_RealName];
            user.Name = currentUser.Identity.Name;

            return user;
        }
    }

    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string Name { get; set; }
    }
}
