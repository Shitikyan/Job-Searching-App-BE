using Job_Searching_App.Data;
using Job_Searching_App.Models;
using Job_Searching_App.Services.Interfaces;
using static Job_Searching_App.Enums.JobEnums;

namespace Job_Searching_App.Services
{
    public class JobService : IJobService
    {
        private readonly JobPortalDbContext _context;

        public JobService(JobPortalDbContext context)
        {
            _context = context;
        }

        public async void UpdateJobStatus(IEnumerable<Job> jobs)
        {
            try
            {
                var jobsToUpdate = jobs
                        .Where(job => job.ApplicationDeadline < DateTime.Now && job.Status == JobStatus.Open)
                        .ToList();

                if (jobsToUpdate == null)
                {
                    return;
                }

                foreach (var job in jobsToUpdate)
                {
                    job.Status = JobStatus.Closed;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occurred while updating job statuses: {ex.Message}", ex);
            }
        }

        public IEnumerable<Job> SearchJobs(IEnumerable<Job> jobs, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return jobs;
            }

            var searchTermToLower = searchTerm.ToLower();
            return jobs.Where(job => job.Company.ToLower().Contains(searchTermToLower) || job.Title.ToLower().Contains(searchTermToLower));
        }
    }
}
