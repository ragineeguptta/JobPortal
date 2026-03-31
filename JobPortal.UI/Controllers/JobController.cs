using JobPortal.Core.Entities;
using JobPortal.UI.ViewModels.Job;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.UI.Controllers
{
    public class JobController : Controller
    {
        private readonly HttpClient _client;

        public JobController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:7052/");
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _client.GetFromJsonAsync<List<JobListViewModel>>("api/jobs");
            return View(jobs);
        }
    }
}
