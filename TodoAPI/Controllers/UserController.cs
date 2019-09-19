using TodoAPI.Entities;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TodoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService m_UserService;

        //---------------------------------------------------------------------------------------------------------------------//

        public UserController(IUserService p_UserService)
        {
            // Constructor
            m_UserService = p_UserService;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(User dtoUser)
        {
            // Get
            var user = m_UserService.Authenticate(dtoUser.Username, dtoUser.Password);

            // Check
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
  
            // Return
            return Ok(user);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IActionResult GetAll()
        {
            // Get
            var users = m_UserService.GetAll();

            // Return
            return Ok(users);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("{id}")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult GetById(int id)
        {
            // Get 
            var user = m_UserService.GetById(id);

            // Check
            if (user == null) return NotFound();

            // Return
            return Ok(user);
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}