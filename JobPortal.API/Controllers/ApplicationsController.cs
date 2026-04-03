using JobPortal.Core.DTO;
using JobPortal.Core.Entities;
using JobPortal.Core.Interfaces;
using JobPortal.Infrastructure;
using JobPortal.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationsController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> Apply(ApplyJobDto dto)
        {
            var application = new Application
            {
                UserId = dto.UserId,
                JobId = dto.JobId,
                ResumePath = dto.ResumePath,
                Status = "Applied",
                AppliedAt = DateTime.UtcNow
            };

            await _applicationRepository.ApplyAsync(application);

            return Ok(application);
        }
    }
}
