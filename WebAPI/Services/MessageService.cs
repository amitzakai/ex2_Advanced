using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService
    {

        private List<Message> messages = new List<Message>();

        public List<Message> GetAll()
        {
            return messages;
        }
        public Message Get(int id)
        {
            return messages.Find(x => x.id == id);
        }
        public void AddMessage(Message message)
        {
            messages.Add(message);
        }
        public void Delete(int id)
        {
            messages.Remove(messages.Find(x => x.id == id));
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

        public int next_id()
        {
            Message lastM = GetLast();
            int newId;
            if (lastM == null)
                newId = 1;
            else
                newId = lastM.id + 1;
            return newId;
        }
    }
}
