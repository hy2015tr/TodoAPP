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
        private readonly IUserService UserService;

        //---------------------------------------------------------------------------------------------------------------------//

        public UserController(IUserService p_UserService)
        {
            // Constructor
            this.UserService = p_UserService;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [AllowAnonymous]
        [HttpGet("Test")]
        public ActionResult<string> Test()
        {
            // Return
            return Ok(System.DateTime.Now.ToLongTimeString());
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<TbUser> Login([FromBody] TbUser p_TbUser)
        {
            // Get
            var user = this.UserService.Authenticate(p_TbUser.Username, p_TbUser.Password);

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
            var users = this.UserService.GetUsers();

            // Return
            return Ok(users);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetUser/{id}")]
        [Authorize(Roles = Role.Admin)]
        public ActionResult<TbUser> GetUser(int id)
        {
            // Get 
            var user = this.UserService.GetUser(id);

            // Check
            if (user == null) return NotFound();

            // Return
            return Ok(user);
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}