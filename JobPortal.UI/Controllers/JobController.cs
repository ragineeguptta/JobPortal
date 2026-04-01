using JobPortal.Core.Entities;
using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}