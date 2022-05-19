using System.ComponentModel.DataAnnotations;
using WebAPI.Services;
namespace WebAPI.Models
{
    public class Contact
    {
        public string Id { get; set; } //user name
        [Required]
        public string NickName { get; set; }

        public MessageService messages { get; set; }

        [Required]
        public string Server { get; set; }
    }
}
