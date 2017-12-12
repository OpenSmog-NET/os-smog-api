namespace OS.DAL
{
    /// <summary>
    /// Performs 2-way entity mapping
    /// </summary>
    /// <typeparam name="TDomainModel">Domain object type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IEntityMapper<TDomainModel, TEntity>
        where TDomainModel : class
        where TEntity : class
    {
        /// <summary>
        /// Map value from domain to underlying entity
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        TEntity MapToModel(TDomainModel @object);

        /// <summary>
        /// Map value from underlying entity to domain
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDomainModel MapFromModel(TEntity @entity);
    }
}