using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForClients.Models;

namespace WebApiForClients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UsersContext db;
        public UsersController(UsersContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new Models.User { Name = "Ilya", DateSale = DateTime.Now.ToString() });
                db.Users.Add(new Models.User { Name = "Vasya", DateSale = DateTime.Now.ToString() });
                db.Users.Add(new Models.User { Name = "Petya", DateSale = DateTime.Now.ToString() });
                db.Users.Add(new Models.User { Name = "Anna", DateSale = DateTime.Now.ToString() });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
        
            return await db.Users.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<User>>> GetById([FromRoute] int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<User>>> GetName([FromRoute] string name)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Name == name);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("{dateSale:datetime}")]
        public async Task<ActionResult<IEnumerable<User>>> GetDateSale(string dateSale)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.DateSale == dateSale);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
