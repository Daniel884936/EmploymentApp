using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EmploymentDbContext _context;
        public CategoryRepository(EmploymentDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAll()
        {
            var categories = _context.Category.AsEnumerable();
            return categories;
        }
    }
}
