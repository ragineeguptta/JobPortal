using JobPortal.Core.DTO;
using JobPortal.Core.Entities;
using JobPortal.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobPortalDbContext _context;

        public JobRepository(JobPortalDbContext context)
        {
            _context = context;
        }
        public async Task<List<JobListDto>> GetJobsWithStatusAsync(int userId)
        {
            var jobs = await _context.Jobs.ToListAsync();

            var appliedJobIds = await _context.Applications
                .Where(a => a.UserId == userId)
                .Select(a => a.JobId)
                .ToListAsync();

            var result = jobs.Select(job => new JobListDto
            {
                Id = job.Id,
                Title = job.Title,
                Company = job.Company,
                Location = job.Location,
                IsApplied = appliedJobIds.Contains(job.Id) // 🔥 KEY
            }).ToList();

            return result;
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }
    }
    
}
