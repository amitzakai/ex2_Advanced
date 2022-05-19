using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService
    {
        private static List<Message> messages = new List<Message>();

        public List<Message> GetAll()
        {
            return messages;
        }
        public Message Get(int id)
        {
            return messages.Find(x => x.Id == id);
        }
        public void AddMessage(Message message)
        {
            messages.Add(message);
        }
        public void Delete(int id)
        {
            messages.Remove(messages.Find(x => x.Id == id));
        }

        internal object? Get(object messageId)
        {
            throw new NotImplementedException();
        }

        public Message GetLast()
        {
            if (messages.Count == 0)
                return null;
            return messages[messages.Count - 1];
        }
    }
}
