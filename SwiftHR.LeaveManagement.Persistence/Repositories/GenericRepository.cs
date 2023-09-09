using Microsoft.EntityFrameworkCore;
using SwiftHR.LeaveManagement.Application.Interfaces.Persistence;
using SwiftHR.LeaveManagement.Domain.Common;
using SwiftHR.LeaveManagement.Persistence.Data;
using System.Collections.Generic;


namespace SwiftHR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SwiftHRDataContext _context;

        public GenericRepository(SwiftHRDataContext context) => _context = context;
        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
