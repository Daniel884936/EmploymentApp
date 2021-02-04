using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using System.Threading.Tasks;

namespace EmploymentApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmploymentDbContext _context;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<TypeSchedule> _typeScheduleRepository;
        private readonly IRepository<Status> _starusRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IJobRepository _jobRepository;


        public UnitOfWork(EmploymentDbContext context)
        {
            _context = context;
        }
        public IRepository<Category> CategoryRepository => _categoryRepository ?? new BaseRepository<Category>(_context);
        public IRepository<TypeSchedule> TypeScheduleRepository => _typeScheduleRepository ?? new BaseRepository<TypeSchedule>(_context);
        public IRepository<Status> StatusRepository => _starusRepository ?? new BaseRepository<Status>(_context);
        public IRepository<Role> RoleRepository => _roleRepository ?? new BaseRepository<Role>(_context);
        public IJobRepository JobRepository => _jobRepository?? new JobRepository (_context);

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
        public void SaveChanges()=> _context.SaveChanges();
        
        public async Task SaveChangesAsync() =>   await _context.SaveChangesAsync();
        
    }
}
