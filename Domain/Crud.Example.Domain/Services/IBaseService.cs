    namespace Crud.Example.Domain.Services;
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add Entity.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Delete an Entity.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Update Entity.
        /// </summary>
        /// <param name="entity"></param>
        int Modify(TEntity entity);

        /// <summary>
        /// Get all the records belonging to that entity.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Allows obtaining the information corresponding to the requested entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity? GetById(int? id);
    }