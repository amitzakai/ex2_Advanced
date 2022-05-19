using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public bool Sent { get; set; }
    }
}
