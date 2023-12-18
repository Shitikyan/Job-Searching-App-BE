using static Job_Searching_App.Enums.JobEnums;

namespace Job_Searching_App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobType Type { get; set; }
        public JobStatus Status { get; set; }
        public string Location { get; set; }
        public float? Salary { get; set; }
        public string Company { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime DatePosted { get; set; }
        public EducationLevel EducationRequirement { get; set; }
        public ExperienceLevel ExperienceRequirement { get; set; }
    }
}
