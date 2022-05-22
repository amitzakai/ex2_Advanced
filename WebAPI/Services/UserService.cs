using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService
    {
        public List<User> users = new List<User>();
        
        public List<User> GetAll()
        {
            return users;
        }

        public User Get(string id)

        {
            return users.Find(x => x.Id == id);
        }

        public void AddUser(User u)
        {
            users.Add(u);
        }

        public void Delete(string id)
        {
            users.Remove(users.Find(x => x.Id == id));
        }

    }
}
