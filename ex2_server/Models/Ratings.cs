using System.ComponentModel.DataAnnotations;

namespace ex2_server.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(1, 5)]
        public int rate { get; set; }
        public string? feedback { get; set; }
    }
}
