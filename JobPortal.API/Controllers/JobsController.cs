using JobPortal.Core.Entities;
using JobPortal.Core.Interfaces;
using JobPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetJobs()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var jobs = await _jobRepository.GetJobsWithStatusAsync(userId);
            return Ok(jobs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            job.CreatedAt = DateTime.UtcNow;

            await _jobRepository.AddAsync(job);

            return Ok(job);
        }
    }
}