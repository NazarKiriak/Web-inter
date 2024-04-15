using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RESTwebAPI.Models
{
    public class HealthChecksDb : DbContext
    {
        public HealthChecksDb(DbContextOptions<HealthChecksDb> options) : base(options)
        {

        }

        public DbSet<HealthCheckResult> HealthCheckResults { get; set; }
    }
}
