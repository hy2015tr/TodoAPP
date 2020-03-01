using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TodoAPI.Entities;

namespace TodoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/Todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService TodoService;

        //---------------------------------------------------------------------------------------------------------------------//

        public TodoController(ITodoService p_TodoService)
        {
            // Constructor
            this.TodoService = p_TodoService;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetTodos")]
        public ActionResult<List<TbTodo>> GetTodos()
        {
            // Get
            var todos = this.TodoService.GetTodos();

            // Return
            return Ok(todos);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetTodo/{id}")]
        public ActionResult<TbTodo> GetTodo(int id)
        {
            // Get
            var todo = this.TodoService.GetTodo(id);

            // Return
            return Ok(todo);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpPost("AddTodo")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult AddTodo([FromBody] TbTodo p_TbTodo)
        {
            // Add
            this.TodoService.AddTodo(p_TbTodo);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpDelete("DeleteTodo/{id}")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DeleteTodo(int id)
        {
            // Delete
            this.TodoService.DeleteTodo(id);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpPut("UpdateTodo")]
        public IActionResult UpdateTodo([FromBody] TbTodo p_TbTodo)
        {
            // Update
            this.TodoService.UpdateTodo(p_TbTodo);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//


    }
}