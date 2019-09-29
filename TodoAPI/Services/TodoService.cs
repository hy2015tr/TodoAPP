using System.Linq;
using TodoAPI.Models;
using System.Collections.Generic;

namespace TodoAPI.Services
{

    //-------------------------------------------------------------------------------------------------------------------------//

    public interface ITodoService
    {
        TbTodo GetTodo(int p_Id);
        List<TbTodo> GetTodos();
        void AddTodo(TbTodo p_Todo);
        void DeleteTodo(int p_Id);
        void UpdateTodo(TbTodo p_Todo);
    }

    //-------------------------------------------------------------------------------------------------------------------------//

    public class TodoService : ITodoService
    {

        //---------------------------------------------------------------------------------------------------------------------//

        private readonly TodoContext m_TodoContext;

        //---------------------------------------------------------------------------------------------------------------------//

        public TodoService(TodoContext p_Context)
        {
            this.m_TodoContext = p_Context;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public TbTodo GetTodo(int p_Id)
        {
            return m_TodoContext.TbTodo.Where(p => p.Id == p_Id).SingleOrDefault();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public List<TbTodo> GetTodos()
        {
            return m_TodoContext.TbTodo.Where(p => p.IsDeleted == false).ToList();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void AddTodo(TbTodo p_Todo)
        {
            m_TodoContext.TbTodo.Add(p_Todo);
            m_TodoContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void DeleteTodo(int p_Id)
        {
            var todo = m_TodoContext.TbTodo.Find(p_Id);
            if (todo == null) return;
            todo.IsDeleted = true;
            m_TodoContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void UpdateTodo(TbTodo p_Todo)
        {
            var todo = m_TodoContext.TbTodo.Find(p_Todo.Id);
            if (todo == null) return;
            todo.IsCompleted = p_Todo.IsCompleted;
            todo.IsDeleted = p_Todo.IsDeleted;
            todo.Title = p_Todo.Title;
            m_TodoContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }



}