using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aspnetwebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnetwebapi.Controllers
{
     //[Authorize]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        public tododbContext Db { get; }

        public TodosController(tododbContext dbContext)
        {
             this.Db = dbContext;
        }
        // GET api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var todos = Db.TodoItems.ToList();
            return Ok(todos);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = Db.TodoItems.Find(id);
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]TodoItem value)
        {
            Db.Add(value);
            Db.SaveChanges();
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TodoItem value)
        {
            Db.Attach(value);
            Db.Entry(value).State = EntityState.Modified;
            Db.SaveChanges();
            return NoContent();
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Db.TodoItems.Find(id);
            if(item!=null)
            {
                Db.TodoItems.Remove(item);
                Db.SaveChanges();
            }
            return NoContent();
        }
    }
}
