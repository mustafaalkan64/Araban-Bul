using Araba.Core.Application;
using Araba.Data.UnitOfWork;
using System.Web.Mvc;

namespace Best.Web.Framework.Controllers
{
    [Authorize(Roles = AppConstants.Role_Admin)]
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
