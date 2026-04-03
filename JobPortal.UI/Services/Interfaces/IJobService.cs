using JobPortal.Core.Entities;
using JobPortal.UI.ViewModels.Job;

namespace JobPortal.UI.Services.Interfaces
{
    public interface IJobService
    {
        Task<List<JobListViewModel>> GetJobsAsync();
        Task CreateJobAsync(CreateJobViewModel model);
        Task ApplyJobAsync(Application application);
    }
}
