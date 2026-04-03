using JobPortal.Core.Entities;
using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortal.UI.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetJobsAsync();
            return View(jobs);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(int JobId, IFormFile Resume)
        {
            string fileName = null;

            if (Resume != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resumes");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                fileName = Guid.NewGuid() + Path.GetExtension(Resume.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Resume.CopyToAsync(stream);
                }
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var application = new Application
            {
                JobId = JobId,
                UserId = userId,
                ResumePath = "/resumes/" + fileName,
            };

            await _jobService.ApplyJobAsync(application);

            return RedirectToAction("Index");
        }
    }
}