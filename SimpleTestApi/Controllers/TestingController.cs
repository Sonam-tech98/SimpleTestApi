using Microsoft.AspNetCore.Mvc;
using SimpleTestApi.Model;

namespace SimpleTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private static List<User> _user = new List<User>
        {
            new User { Id = 1, Name = "Laptop", Email = "Laptop@gmail.com", Address="Lko" },
            new User { Id = 2, Name = "Mouse", Email = "Mouse@gmail.com", Address="Agra" },
            new User { Id = 3, Name = "Keyboard", Email = "Keyboard@gmail.com", Address="Kanpur" }
        };
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_user);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _user.FirstOrDefault(p => p.Id == id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }


        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            user.Id = _user.Max(p => p.Id) + 1;
            _user.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var index = _user.FindIndex(p => p.Id == id);
            if (index == -1) return NotFound();

            _user[index] = user;
            _user[index].Id = id;
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _user.FirstOrDefault(p => p.Id == id);
            if (user == null) return NotFound();

            _user.Remove(user);
            return NoContent();
        }
    }
}
