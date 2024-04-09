using Microsoft.AspNetCore.Mvc;

namespace RESTwebAPI.Models
{
    public class HealthCheckResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
