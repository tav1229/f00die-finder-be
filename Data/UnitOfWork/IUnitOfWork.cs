using f00die_finder_be.Data.Entities;

namespace f00die_finder_be.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task<IQueryable<T>> GetQueryableAsync<T>() where T : BaseEntity;
        Task DeleteAsync<T>(T entity, bool isHardDeleted = false) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task AddAsync<T>(T entity) where T : BaseEntity;
    }


}
