using Job_Searching_App.Data;
using Job_Searching_App.Models;
using Job_Searching_App.Repositories;
using Job_Searching_App.Repositories.Interfaces;

public class JobRepository : BaseRepository<Job>, IJobRepository
{
    public JobRepository(JobPortalDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Job>> GetAllJobsAsync()
    {
        try
        {
            return await base.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching jobs: {ex.Message}", ex);
        }
    }

    public async Task<Job> GetJobByIdAsync(Guid jobId)
    {
        try
        {
            return await base.GetByIdAsync(jobId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching job by ID: {ex.Message}", ex);
        }
    }

    public async Task AddJobAsync(Job job)
    {
        try
        {
            await base.AddAsync(job);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while adding job: {ex.Message}", ex);
        }
    }

    public async Task UpdateJobAsync(Job job)
    {
        try
        {
            await base.UpdateAsync(job);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while updating job: {ex.Message}", ex);
        }
    }

    public async Task DeleteJobAsync(Guid jobId)
    {
        try
        {
            var job = await GetByIdAsync(jobId);
            if (job == null)
            {
                throw new KeyNotFoundException($"Job with ID {jobId} not found.");
            }

            await base.DeleteAsync(job);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while deleting job: {ex.Message}", ex);
        }
    }
}
