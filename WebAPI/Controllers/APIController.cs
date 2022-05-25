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
                id = temp.Id,
                name = temp.NickName,
                messages = new MessageService(),
                server = "1111"

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
                id = sender.messages.next_id(),
                content = T.content,
                created = T.created,
                sent = false

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
            if (us.Get(c.id) == null)
                return NotFound("you can add only registered users");
            if (U.contacts.Get(c.id) != null)
                return NotFound("you already have this contact");
            
            Contact C = new Contact() { id = c.id,
                name = c.name,
                messages = new MessageService(),
                server = c.server
            };
            U.contacts.AddContact(C);
            Invitaion i = new Invitaion()
            {
                Id = ++counter_invitaion,
                from = user,
                to = c.id,
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
                c.name = n;
                c.server = s;
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
                U.contacts.Delete(c.id);
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
                //DateTime t = new DateTime();
                Message m = new Message()
                {
                    id = c.messages.next_id(),
                    content = M.content,
                    sent = true,
                    created = M.created
                };
                c.messages.AddMessage(m);
                c.last = M.content;
                c.lastdate = M.created;
                Transfer T = new Transfer()
                {
                    Id = 1,
                    from = U.Id,
                    to = c.id,
                    content = M.content,
                    created = M.created
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
                    m.content = M.content;
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

