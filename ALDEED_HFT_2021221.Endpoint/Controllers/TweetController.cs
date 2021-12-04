using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Endpoint.Controllers
{
    public class TweetController : ControllerBase
    {
        public TweetController(ITweetLogic tl)
        {
            this.tl = tl;
        }

        // GET
        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
            return tl.GetAll();
        }

        // GET
        [HttpGet("{id}")]
        public Tweet Get(int id)
        {
            return tl.Read(id);
        }

        // POST
        [HttpPost]
        public void Post([FromBody] Tweet value)
        {
            tl.Create(value);
        }

        // PUT
        [HttpPut]
        public void Put([FromBody] Tweet value)
        {
            tl.Update(value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            tl.Delete(id);
        }
    }
}

