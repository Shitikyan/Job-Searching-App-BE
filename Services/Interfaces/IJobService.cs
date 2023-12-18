using Job_Searching_App.Models;

namespace Job_Searching_App.Services.Interfaces
{
    public interface IJobService
    {
        void UpdateJobStatus(IEnumerable<Job> jobs);
        IEnumerable<Job> SearchJobs(IEnumerable<Job> jobs, string searchTerm);
    }
}
