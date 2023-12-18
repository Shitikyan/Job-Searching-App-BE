using Job_Searching_App.Models;
using Job_Searching_App.Repositories.Interfaces;
using Job_Searching_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Job_Searching_App.Enums.JobEnums;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJobRepository _jobRepository;
    private readonly IJobService _jobService;

    public JobsController(
        IJobRepository jobRepository,
        IJobService jobService)
    {
        _jobRepository = jobRepository;
        _jobService = jobService;
    }

    // GET: api/jobs
    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetJobs(int skip = 0, int take = 10, string searchTerm = "")
    {
        try
        {
            var jobs = await _jobRepository.GetAllJobsAsync();

            jobs = _jobService.SearchJobs(jobs, searchTerm);

            _jobService.UpdateJobStatus(jobs);

            var paginatedJobs = jobs.Skip(skip).Take(take).ToList();
            return Ok(paginatedJobs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving jobs.");
        }
    }

    // GET: api/jobs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetJobById(Guid id)
    {
        try
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the job.");
        }
    }

    // POST: api/jobs
    [HttpPost]
    public async Task<ActionResult<Job>> PostJob(Job job)
    {
        try
        {
            job.Id = Guid.NewGuid();
            job.DatePosted = DateTime.UtcNow;
            job.Status = JobStatus.Open;

            if (!IsValidApplicationDeadline(job.ApplicationDeadline))
            {
                return BadRequest("Application Deadline must be in the future.");
            }

            await _jobRepository.AddJobAsync(job);
            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, job);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while posting the job.");
        }
    }

    // PUT: api/jobs/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(Guid id, Job job)
    {
        try
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            await _jobRepository.UpdateJobAsync(job);
            return Ok(job);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the job.");
        }
    }

    // DELETE: api/jobs/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(Guid id)
    {
        try
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await _jobRepository.DeleteJobAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the job.");
        }
    }

    private bool IsValidApplicationDeadline(DateTime applicationDeadline)
    {
        return applicationDeadline >= DateTime.UtcNow;
    }
}
