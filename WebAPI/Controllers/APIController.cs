using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;


namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : Controller
    {
        private static ContactService cs = new ContactService();
        [Route("contacts")]
        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(cs.GetAll());
        }
        [Route("contacts")]
        [HttpPost]
        public IActionResult addC(Contact c)
        {
            cs.AddContact(c);
            return Ok(cs.GetAll());
        }

        [Route("contacts/{cId}")]
        [HttpGet]
        public IActionResult getOne(string cId)
        {
            Contact c = cs.Get(cId);
            if (c == null)
                return NotFound();
            else
                return Ok(c);
        }

        [Route("contacts/{cId}")]
        [HttpPut]
        public IActionResult changeC(string cId, string s, string n)
        {
            Contact c = cs.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                c.NickName = n;
                c.Server = s;
                return Ok(c);
            }
        }

        [Route("contacts/{cId}")]
        [HttpDelete]
        public IActionResult deleteC(string cId)
        {
            Contact c = cs.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                cs.Delete(c.Id);
                return Ok(cs.GetAll());
            }
        }

        [Route("contacts/{cId}/messages")]
        [HttpGet]
        public IActionResult getMs(string cId)
        {
            Contact c = cs.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                return Ok(c.messages.GetAll());
            }
        }

        [Route("contacts/{cId}/messages")]
        [HttpPost]
        public IActionResult sendMs(string cId, string con, bool sender)
        {
            Contact c = cs.Get(cId);
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
                    Content = con,
                    Sent = true
                };
                c.messages.AddMessage(m);
                return Ok(m);
            }
        }

        [Route("contacts/{cId}/messages/{mId}")]
        [HttpGet]
        public IActionResult getM(string cId, int mId)
        {
            Contact c = cs.Get(cId);
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

        [Route("contacts/{cId}/messages/{mId}")]
        [HttpPut]
        public IActionResult changeM(string cId, int mId, string con)
        {
            Contact c = cs.Get(cId);
            if (c == null)
                return NotFound();
            else
            {
                Message m = c.messages.Get(mId);
                if (c == null)
                    return NotFound();
                else
                {
                    m.Content = con;
                    return Ok(m);
                }
            }
        }

        [Route("contacts/{cId}/messages/{mId}")]
        [HttpDelete]
        public IActionResult delM(string cId, int mId)
        {
            Contact c = cs.Get(cId);
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

