using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Job;

namespace JobPortal.UI.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly HttpClient _client;

        public JobService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<JobListViewModel>> GetJobsAsync()
        {
            var response = await _client.GetAsync("api/jobs");

            if (!response.IsSuccessStatusCode)
                return new List<JobListViewModel>();

            return await response.Content.ReadFromJsonAsync<List<JobListViewModel>>();
        }

        public async Task CreateJobAsync(CreateJobViewModel model)
        {
            var response = await _client.PostAsJsonAsync("api/jobs", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create job");
            }
        }
    }

}
