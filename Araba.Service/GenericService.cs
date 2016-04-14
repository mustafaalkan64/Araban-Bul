using Araba.Data.Repository;
using Araba.Data.UnitOfWork;
using System.Linq;

namespace Araba.Service
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork uow)
        {
            _genericRepository = uow.GetRepository<TEntity>();
        }

        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _genericRepository.GetAll();
        }

        /// <summary>
        /// Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Find(int id)
        {
            return _genericRepository.Find(id);
        }

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            _genericRepository.Insert(entity);
        }

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            _genericRepository.Update(entity);
        }

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entity">Kayıt</param>
        public virtual void Delete(TEntity entity)
        {
            _genericRepository.Delete(entity);
        }
    }
}
