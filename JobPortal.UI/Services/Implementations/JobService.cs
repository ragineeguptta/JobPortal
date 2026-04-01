using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Job;
using System.Net.Http.Headers;

namespace JobPortal.UI.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobService(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            _client = factory.CreateClient("API");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<JobListViewModel>> GetJobsAsync()
        {
            AddJwtToken(); //  ADD THIS

            var response = await _client.GetAsync("api/jobs");

            if (!response.IsSuccessStatusCode)
                return new List<JobListViewModel>();

            return await response.Content.ReadFromJsonAsync<List<JobListViewModel>>();
        }

        public async Task CreateJobAsync(CreateJobViewModel model)
        {
            AddJwtToken(); //  ADD THIS

            var response = await _client.PostAsJsonAsync("api/jobs", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create job");
            }
        }
    }
}