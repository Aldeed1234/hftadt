using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Endpoint.Controllers
{
    public class CommentController : ControllerBase
    {
        ICommentLogic cl;

        public CarController(ICommentLogic cl)
        {
            this.cl = cl;
        }

        // GET
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return cl.GetAll();
        }

        // GET
        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            return cl.Read(id);
        }

        // POST
        [HttpPost]
        public void Post([FromBody] Comment value)
        {
            cl.Create(value);
        }

        // PUT
        [HttpPut]
        public void Put([FromBody] Comment value)
        {
            cl.Update(value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.Delete(id);
        }
    }
}
