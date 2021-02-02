using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _entities;
        public BaseRepository(EmploymentDbContext context)
        {
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _entities.AsEnumerable();
        public async Task<T> GetById(int id) => await _entities.FirstOrDefaultAsync(e => e.Id == id);
        public void Update(T entity) => _entities.Update(entity);
        public async Task Add(T entity) => await _entities.AddAsync(entity);
        public void Remove(T entity)=> _entities.Remove(entity);
        
    }
}
