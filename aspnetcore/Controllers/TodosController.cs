using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myAppApi.Models;

namespace myAppApi.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        public TodoDbContext Db { get; }

        public TodosController(TodoDbContext dbContext)
        {
             this.Db = dbContext;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var todos = Db.TodoItem.ToList();
            return Ok(todos);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = Db.TodoItem.Find(id);
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
            var item = Db.TodoItem.Find(id);
            if(item!=null)
            {
                Db.TodoItem.Remove(item);
                Db.SaveChanges();
            }
            return NoContent();
        }
    }
}
