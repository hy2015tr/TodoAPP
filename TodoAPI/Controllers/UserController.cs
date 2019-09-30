using TodoAPI.Models;
using TodoAPI.Entities;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TodoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/Users")]
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
        [HttpPost("Login")]
        public ActionResult<TbUser> Login([FromBody] TbUser p_TbUser)
        {
            // Get
            var user = m_UserService.Authenticate(p_TbUser.Username, p_TbUser.Password);

            // Check
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            // Return
            return Ok(user);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetUsers")]
        [Authorize(Roles = Role.Admin)]
        public ActionResult<List<TbUser>> GetUsers()
        {
            // Get
            var users = m_UserService.GetUsers();

            // Return
            return Ok(users);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetUser/{id}")]
        [Authorize(Roles = Role.Admin)]
        public ActionResult<TbUser> GetUser(int id)
        {
            // Get 
            var user = m_UserService.GetUser(id);

            // Check
            if (user == null) return NotFound();

            // Return
            return Ok(user);
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}