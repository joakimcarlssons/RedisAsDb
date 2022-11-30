using System.ComponentModel.DataAnnotations;

namespace RedisAsDb.API.Models
{
    public class Platform
    {
        [Required]
        public string Id { get; set; } = $"platform:{Guid.NewGuid()}";

        [Required]
        public string Name { get; set; } = String.Empty;
    }
}
