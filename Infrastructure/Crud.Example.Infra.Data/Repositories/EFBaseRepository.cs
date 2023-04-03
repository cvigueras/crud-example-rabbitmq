using Crud.Example.Domain.Repositories;
using Crud.Example.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud.Example.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Generic class that can be used by any entity,
    /// takes care of querying the Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFBaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public EFBaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// Method that allows to insert an entity.
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public int Add(TEntity entity)
        {
            try
            {
                _applicationDbContext.Add<TEntity>(entity);
                return _applicationDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No se puede guardar el registro", ex);
            }
        }

        /// <summary>
        /// Method that allows to remove an entity
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(int id)
        {
            try
            {
                var entity = _applicationDbContext.Set<TEntity>().Find(id);
                if (entity != null)
                {
                    _applicationDbContext.Set<TEntity>().Remove(entity);
                    _applicationDbContext.SaveChanges();
                }

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No se puede eliminar el registro", ex);
            }
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

        /// <summary>
        /// Method that allows obtaining all the records belonging to that entity.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _applicationDbContext.Set<TEntity>().ToList();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No se pudieron recuperar los registros", ex);
            }
        }

        /// <summary>
        /// Method that allows obtaining the information corresponding to the requested entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TEntity? GetById(int? id)
        {
            try
            {
                return _applicationDbContext.Set<TEntity>().Find(id);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No se pudo recuperar el registro", ex);
            }
        }

        /// <summary>
        /// Method that allows updating the information of an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="Exception"></exception>
        public int Modify(TEntity entity)
        {
            try
            {
                _applicationDbContext.Entry(entity).State = EntityState.Modified;
                return _applicationDbContext.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("No se puede actualizar el registro", ex);
            }
        }
    }
}
