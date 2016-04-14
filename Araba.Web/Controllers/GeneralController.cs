using Araba.Data.UnitOfWork;
using Araba.Service.LocationServices;
using Best.Web.Framework.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Araba.Web.Controllers
{
    public class GeneralController : PublicController
    {
        private readonly ILocationService _locationService;

        public GeneralController(IUnitOfWork uow, ILocationService locationService)
            : base(uow)
        {
            _locationService = locationService;
        }

        public ActionResult JsonDistricts(int cityId)
        {
            var result = _locationService.GetDistricts(cityId).Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}