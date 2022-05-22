using WebAPI.Models;

namespace WebAPI.Services
{
    public class ContactService
    {
        private List<Contact> contacts = new List<Contact>();


        public List<Contact> GetAll()
        {
            return contacts;
        }
        public Contact Get(string id)
        {
            return contacts.Find(x => x.id == id);
        }
        public void AddContact(Contact c)
        {
            contacts.Add(c);
        }
        public void Delete(string id)
        {
            contacts.Remove(contacts.Find(x => x.id == id));
        }

        internal object? Get(object messageId)
        {
            throw new NotImplementedException();
        }
    }
}
