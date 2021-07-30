using Assignment.Data;
using Assignment.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        protected AssignmentDBContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(
            AssignmentDBContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
           
            return true;
        }

        public async Task<bool> Save()
        {
            context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
                return await dbSet.FindAsync(Id);
        }
    }
}
