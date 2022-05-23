using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;


namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : Controller
    {
        private static UserService us = new UserService();

        int counter_invitaion = 0;

        int counter_transfer = 0;

        [Route("users")]
        [HttpGet]
        public IActionResult Index() {
            return Ok(us.GetAll());
        }

        [Route("users")]
        [HttpPost]
        public IActionResult registerUser(User U)
        {
            if (us.Get(U.Id) != null)
                return NotFound("the user name already regitered");
            U.contacts = new ContactService();
            us.AddUser(U);
            return Ok(U);
        }

        [Route("users/{id}")]
        [HttpGet]
        public IActionResult getUser(string id)
        {
            User U = us.Get(id);
            if (U == null)
                return NotFound("the user does not exist");
            return Ok(U);
        }


        [Route("invitaion")]
        [HttpPost]
        public IActionResult invite(Invitaion I) {
            User invited = us.Get(I.to);
            User temp = us.Get(I.from);
            Contact inviter = new Contact()
            {
                Id = temp.Id,
                Name = temp.NickName,
                messages = new MessageService(),
                Server = "1111"

            };
            invited.contacts.AddContact(inviter);
            return Ok();
        }

        [Route("transfer")]
        [HttpPost]
        public IActionResult transfer(Transfer T)
        {
            User reciever = us.Get(T.to);
            Contact sender = reciever.contacts.Get(T.from);
            Message M = new Message()
            {
                Id = sender.messages.next_id(),
                Content = T.content,
                Created = new DateTime(),
                Sent = false

            };
            sender.messages.AddMessage(M);
            return Ok();
        }


        [Route("contacts/{user}")]
        [HttpGet]
        public IActionResult getAll(string user)
        {
            if (us.Get(user) == null)
                return NotFound();
            return Ok(us.Get(user).contacts.GetAll());
        }

        [Route("contacts/{user}")]
        [HttpPost]
        public IActionResult addC(string user, Contact c)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            if (us.Get(c.Id) == null)
                return NotFound("you can add only registered users");
            if (U.contacts.Get(c.Id) != null)
                return NotFound("you already have this contact");
            
            Contact C = new Contact() { Id = c.Id,
                Name = c.Name,
                messages = new MessageService(),
                Server = c.Server
            };
            U.contacts.AddContact(C);
            Invitaion i = new Invitaion()
            {
                Id = ++counter_invitaion,
                from = user,
                to = c.Id,
                server = "0000"
            };
            invite(i);
            return Ok(c);
        }

        [Route("contacts/{user}/{cId}")]
        [HttpGet]
        public IActionResult getOne(string user, string cId)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
                return Ok(c);
        }

        [Route("contacts/{user}/{cId}")]
        [HttpPut]
        public IActionResult changeC(string user, string cId, string s, string n)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                c.Name = n;
                c.Server = s;
                return Ok(c);
            }
        }

        [Route("contacts/{user}/{cId}")]
        [HttpDelete]
        public IActionResult deleteC(string user, string cId)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                U.contacts.Delete(c.Id);
                return Ok("deleted");
            }
        }

        [Route("contacts/{user}/{cId}/messages")]
        [HttpGet]
        public IActionResult getMs(string user, string cId)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                return Ok(c.messages.GetAll());
            }
        }

        [Route("contacts/{user}/{cId}/messages")]
        [HttpPost]
        public IActionResult sendMs(string user, string cId, Message M)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                Message m = new Message()
                {
                    Id = c.messages.next_id(),
                    Content = M.Content,
                    Sent = true
                };
                c.messages.AddMessage(m);
                Transfer T = new Transfer()
                {
                    Id = 1,
                    from = U.Id,
                    to = c.Id,
                    content = M.Content
                };
                transfer(T);
                return Ok(m);
            }
        }

        [Route("contacts/{user}/{cId}/messages/{mId}")]
        [HttpGet]
        public IActionResult getM(string user, string cId, int mId)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                Message m = c.messages.Get(mId);
                if (c == null)
                    return NotFound();
                else
                {
                    return Ok(m);
                }
            }
        }

        [Route("contacts/{user}/{cId}/messages/{mId}")]
        [HttpPut]
        public IActionResult changeM(string user, string cId, int mId, Message M)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                Message m = c.messages.Get(mId);
                if (c == null)
                    return NotFound();
                else
                {
                    m.Content = M.Content;
                    return Ok(m);
                }
            }
        }

        [Route("contacts/{user}/{cId}/messages/{mId}")]
        [HttpDelete]
        public IActionResult delM(string user, string cId, int mId)
        {
            User U = us.Get(user);
            if (U == null)
                return NotFound();
            Contact c = U.contacts.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                Message m = c.messages.Get(mId);
                if (c == null)
                    return NotFound();
                else
                {
                    c.messages.Delete(mId);
                    return Ok();//
                }
            }
        }
    }
}

