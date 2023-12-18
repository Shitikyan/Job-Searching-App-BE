using Job_Searching_App.Models;

namespace Job_Searching_App.Repositories.Interfaces
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(Guid jobId);
        Task AddJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(Guid jobId);
    }
}
