using Araba.Data.Context;
using Araba.Data.Repository;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Araba.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArabaContext _context;
        private bool disposed = false;

        public UnitOfWork(ArabaContext context)
        {
            // veritabanında değişiklik olursa, değişikliği kaydet
            Database.SetInitializer<ArabaContext>(null);

            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            int affectedRow = 0;

            try
            {
                if (_context == null)
                    throw new ArgumentNullException("_context");

                affectedRow = _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);

                throw fail;
            }

            return affectedRow;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
