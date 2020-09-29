using Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace WebAppMongo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_service.GetAllUsers() as List<User>);
        }

        [Route("/api/user/count/")]
        public ActionResult<long> CountUser()
        {
            return Ok(_service.CountThemAll());
        }

        [Route("/api/user/GetUserById/{id}")]
        public ActionResult<User> GetUserById(string id)
        {
            User user = _service.GetSingle(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [Route("/api/user/createuser/", Name = "Create User")]
        public ActionResult<User> CreateUser(User user)
        {

            if (user == null)
            {
                return BadRequest();
            }
            _service.AddUser(user);

            return Ok(user);
        }

        [HttpPatch]
        [Route("/api/user/updateuser/{id}", Name = "Update User")]
        public IActionResult UpdateUser(string id, [FromBody]JsonPatchDocument<User> patchDoc)
        {
            User foundUser = _service.GetSingle(id);
            patchDoc.ApplyTo(foundUser);
            _service.UpdateUser(foundUser);

            return Ok();
        }



        [Route("/api/user/DeleteUser/{id}")]
        public IActionResult DeleteUser(string id)
        {
            User user = _service.GetSingle(id);

            if (user == null)
            {
                return NotFound();
            }

            _service.RemoveUser(user);

            return Ok();
        }
    }
}