using Microsoft.AspNetCore.Mvc;
using DapperMvcApp.Models;

namespace DapperMvcApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        IUserRepository repo;
        public UserController(IUserRepository r)
        {
            repo = r;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return repo.GetUsers();
        }

        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            return repo.GetUserById(id);
        }

        [HttpGet("{email}, {password}")]
        public User GetUserByEmailAndPassword(string email, string password)
        {
            User user = new User();
            user.email = email;
            user.heshPassword = password;
            return repo.GetUserByEmailAndPassword(user);
        }

        [HttpPost]
        public StatusCodeResult Create(User user)
        {
            if (repo.Create(user))
                return StatusCode(201);
            return StatusCode(405);
        }

        [HttpPut]
        public StatusCodeResult Edit(User user)
        {
            if (repo.GetUserById(user.id) == null)
                return StatusCode(405);


            if (repo.Update(user))
                return StatusCode(201);
            return StatusCode(405);
        }

        [HttpDelete]
        public StatusCodeResult Delete(int id)
        {
            if (repo.GetUserById(id) == null)
                return StatusCode(405);

            if (repo.Delete(id))
                return StatusCode(201);
            return StatusCode(405);
        }
    }
}