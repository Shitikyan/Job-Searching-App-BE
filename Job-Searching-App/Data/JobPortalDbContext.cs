using Job_Searching_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Searching_App.Data
{
    public class JobPortalDbContext : DbContext
    {
        public JobPortalDbContext(DbContextOptions<JobPortalDbContext> options) : base(options) { }
        public DbSet<Job> Jobs { get; set; }
    }
}
