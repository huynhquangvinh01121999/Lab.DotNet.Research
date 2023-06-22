using TodoList.BlazorWasm.Domain.Entities;
using TodoList.BlazorWasm.Domain.Interfaces;
using TodoList.BlazorWasm.Persistence.Contexts;
using TodoList.BlazorWasm.Persistence.Repositories.Base;

namespace TodoList.BlazorWasm.Persistence.Repositories
{
    public class TypesRepository : Repository<Types>, ITypesRepository
    {
        public TypesRepository(AppDbContext dbContext) : base (dbContext)
        {
        }
    }
}
