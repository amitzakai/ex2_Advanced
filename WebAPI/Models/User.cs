using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User
    {
        public string Id { get; set; } //user name
        [Required]
        public string Password { get; set; }
        [Required]
        public string NickName { get; set; }
        public List<Contact> ContactList { get; set; }
    }
}
