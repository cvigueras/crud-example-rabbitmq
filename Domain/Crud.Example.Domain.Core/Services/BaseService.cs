using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;

namespace Crud.Example.Domain.Core.Services
{
    /// <summary>
    /// Corresponde a la entidad con la cual van a trabajar los métodos de esta interfaz, por ejemplo: entidad de tipo Dealer/Shop/Order...
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        /// <summary>
        /// Corresponde a la interfaz de tipo IBaseRepository
        /// </summary>
        /// <param name="baseRepository"></param>
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Corresponde a la entidad que se desea agregar.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _baseRepository.Add(entity);
        }

        /// <summary>
        /// Corresponde al identificador de la entidad que se desea eliminar.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _baseRepository.Delete(id);
        }

        /// <summary>
        /// Dispose the connection.
        /// </summary>
        public void Dispose()
        {
            _baseRepository.Dispose();
        }

        /// <summary>
        /// Método que permite obtener todos los registros pertenecientes a esa entidad.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this._baseRepository.GetAll();
        }

        /// <summary>
        /// Método que permite obtener la información correspondiente a la entidad solicitada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity? GetById(int? id)
        {
            return this._baseRepository.GetById(id);
        }

        /// <summary>
        /// Método que permite actualizar la información una entidad.
        /// </summary>
        /// <param name="entity"></param>
        public int Modify(TEntity entity)
        {
            return _baseRepository.Modify(entity);
        }
    }
}
