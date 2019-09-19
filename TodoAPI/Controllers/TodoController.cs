using TodoAPI.Models;
using TodoAPI.Entities;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/todos")]
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

        [HttpGet]
        public ActionResult<List<TbTodo>> GetTodos()
        {
            // Get
            var todos = m_TodoService.GetTodos();

            // Return
            return Ok(todos);
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpPost]
        [Route("add")]
        public IActionResult AddTodo([FromBody]TbTodo p_TbTodo)
        {
            // Add
            m_TodoService.AddTodo(p_TbTodo);

            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteTodo(int id)
        {
            // Delete
            m_TodoService.DeleteTodo(id);  
         
            // Return
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}