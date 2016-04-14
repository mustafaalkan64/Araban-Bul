using Araba.Core.Domain.DbTables;
using Araba.Data.Repository;
using Araba.Data.UnitOfWork;
using System.Linq;

namespace Araba.Service.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly IGenericRepository<City> _cityRepository;
        private readonly IGenericRepository<District> _districtRepository;

        public LocationService(IUnitOfWork uow)
        {
            _cityRepository = uow.GetRepository<City>();
            _districtRepository = uow.GetRepository<District>();
        }

        /// <summary>
        /// İller
        /// </summary>
        public IQueryable<City> GetCities()
        {
            return _cityRepository.GetAll();
        }

        /// <summary>
        /// İlçeler
        /// </summary>
        /// <param name="cityId"></param>
        public IQueryable<District> GetDistricts(int cityId)
        {
            return _districtRepository.GetAll().Where(x => x.CityId == cityId);
        }
    }
}
