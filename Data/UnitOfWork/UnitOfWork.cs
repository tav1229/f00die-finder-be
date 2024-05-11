using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public Task<IQueryable<T>> GetAllAsync<T>() where T : BaseEntity
        {
            return Task.FromResult(_context.Set<T>().Where(e => e.IsDeleted == false));
        }

        public async Task DeleteAsync<T>(T entity, bool isHardDeleted = false) where T : BaseEntity
        {
            if (isHardDeleted)
            {
                _context.Set<T>().Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task AddAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            UpdateTimestamps();
            await _context.SaveChangesAsync();
        }

        private void UpdateTimestamps()
        {
            var entities = _context.ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentDateTime = DateTime.Now;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = currentDateTime;
                }

                ((BaseEntity)entity.Entity).LastUpdatedDate = currentDateTime;
            }
        }
    }

}
