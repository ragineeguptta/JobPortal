using JobPortal.Core.Entities;
using JobPortal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly JobPortalDbContext _context;
        public ApplicationRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        public async Task ApplyAsync(Application app)
        {
            await _context.Applications.AddAsync(app);
            await _context.SaveChangesAsync();
        }
    }
}
