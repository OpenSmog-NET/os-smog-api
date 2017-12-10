namespace OS.DAL
{
    /// <summary>
    /// Interface for Entity Mappers
    /// </summary>
    /// <typeparam name="TDomain">Domain object type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IEntityMapper<TDomain, TEntity>
        where TDomain : class
        where TEntity : class
    {
        /// <summary>
        /// Map value from domain to underlying entity
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        TEntity MapToModel(TDomain @object);

        /// <summary>
        /// Map value from underlying entity to domain
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDomain MapFromModel(TEntity @entity);
    }
}