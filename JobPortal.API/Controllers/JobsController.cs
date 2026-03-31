using JobPortal.Core.Entities;
using JobPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly JobPortalDbContext _context;

        public JobsController(JobPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            return Ok(_context.Jobs.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateJob(Job job)
        {
            job.CreatedAt = DateTime.UtcNow;
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return Ok(job);
        }
    }
}
