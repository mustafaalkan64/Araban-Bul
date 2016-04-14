using Araba.Data.UnitOfWork;
using System.Web.Mvc;

namespace Best.Web.Framework.Controllers
{
    [Authorize]
    public class AuthorizedController : BaseController
    {
        public AuthorizedController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
