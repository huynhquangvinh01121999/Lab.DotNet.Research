using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.BlazorWasm.Domain.Interfaces.Base;
using TodoList.BlazorWasm.Persistence.Contexts;

namespace TodoList.BlazorWasm.Persistence.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            var result = await SaveChangeAsync();

            return result > 0 ? entity : null;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            var result = await SaveChangeAsync();

            return result > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);

            var result = await SaveChangeAsync();

            return result > 0 ? true : false;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task<int> SaveChangeAsync() => await _context.SaveChangesAsync();
    }
}
