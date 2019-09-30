using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TodoAPI.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("Api/Todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService m_TodoService;

        //---------------------------------------------------------------------------------------------------------------------//

        public TodoController(ITodoService p_TodoService)
        {
            // Constructor
            m_TodoService = p_TodoService;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetTodos")]
        public ActionResult<List<TbTodo>> GetTodos()
        {
            // Get
            var todos = m_TodoService.GetTodos();

            // Return
            return Ok(todos);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetTodo/{id}")]
        public ActionResult<TbTodo> GetTodo(int id)
        {
            // Get
            var todo = m_TodoService.GetTodo(id);

            // Return
            return Ok(todo);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpPost("AddTodo")]
        public IActionResult AddTodo([FromBody] TbTodo p_TbTodo)
        {
            // Add
            m_TodoService.AddTodo(p_TbTodo);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpDelete("DeleteTodo/{id}")]
        public IActionResult DeleteTodo(int id)
        {
            // Delete
            m_TodoService.DeleteTodo(id);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpPut("UpdateTodo")]
        public IActionResult UpdateTodo([FromBody] TbTodo p_TbTodo)
        {
            // Update
            m_TodoService.UpdateTodo(p_TbTodo);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//


    }
}