using System.ComponentModel.DataAnnotations;
using WebAPI.Services;
namespace WebAPI.Models
{
    public class Contact
    {
        public string id { get; set; } //user name
        [Required]
        public string name { get; set; } // nickname

        public MessageService messages { get; set; } // check if needed

        [Required]
        public string server { get; set; }

        public string? last { get; set; }

        public string? lastdate { get; set; }
    }
}
