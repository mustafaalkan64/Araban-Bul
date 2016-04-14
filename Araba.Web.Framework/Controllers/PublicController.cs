using Araba.Data.UnitOfWork;

namespace Best.Web.Framework.Controllers
{
    public class PublicController : BaseController
    {
        public PublicController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
