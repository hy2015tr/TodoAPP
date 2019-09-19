using System.Linq;
using TodoAPI.Models;
using System.Collections.Generic;

namespace TodoAPI.Services
{

    //-------------------------------------------------------------------------------------------------------------------------//

    public interface ITodoService
    {
        List<TbTodo> GetTodos();
        void AddTodo(TbTodo p_Todo);
        void DeleteTodo(int p_Id);
    }

    //-------------------------------------------------------------------------------------------------------------------------//

    public class TodoService: ITodoService{

        //---------------------------------------------------------------------------------------------------------------------//

        private readonly TodoContext m_TodoContext;

        //---------------------------------------------------------------------------------------------------------------------//

        public TodoService(TodoContext p_Context){
            this.m_TodoContext = p_Context;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public List<TbTodo> GetTodos(){
            return m_TodoContext.TbTodo.ToList();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void AddTodo(TbTodo p_Todo){
            m_TodoContext.TbTodo.Add(p_Todo);
            m_TodoContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void DeleteTodo(int p_Id){
            var todo = m_TodoContext.TbTodo.Find(p_Id);
            m_TodoContext.TbTodo.Remove(todo);
            m_TodoContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }



}