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
            U.contacts.AddContact(c);
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
                c.NickName = n;
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
                Message lastM = c.messages.GetLast();
                int newId;
                if (lastM == null)
                    newId = 1;
                else
                    newId = lastM.Id + 1;
                Message m = new Message()
                {
                    Id = newId,
                    Content = M.Content,
                    Sent = true
                };
                c.messages.AddMessage(m);
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
                    return Ok();
                }
            }
        }
    }
}

