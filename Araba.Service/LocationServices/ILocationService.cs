using Araba.Core.Domain.DbTables;
using System.Linq;

namespace Araba.Service.LocationServices
{
    public interface ILocationService
    {
        /// <summary>
        /// İller
        /// </summary>
        IQueryable<City> GetCities();

        /// <summary>
        /// İlçeler
        /// </summary>
        /// <param name="cityId"></param>
        IQueryable<District> GetDistricts(int cityId);
    }
}
