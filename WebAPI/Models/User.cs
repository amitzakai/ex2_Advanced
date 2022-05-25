using System.ComponentModel.DataAnnotations;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class User
    {
        public string Id { get; set; } //user name
        [Required]
        public string Password { get; set; }
        [Required]
        public string NickName { get; set; }
        public ContactService contacts { get; set; }
        public string server { get; set; }


    }
}
