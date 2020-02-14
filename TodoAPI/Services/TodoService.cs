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

        private readonly TodoDBContext TodoDBContext;

        //---------------------------------------------------------------------------------------------------------------------//

        public TodoService(TodoDBContext p_Context)
        {
            this.TodoDBContext = p_Context;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public TbTodo GetTodo(int p_Id)
        {
            return this.TodoDBContext.TbTodo.Where(p => p.Id == p_Id).SingleOrDefault();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public List<TbTodo> GetTodos()
        {
            return this.TodoDBContext.TbTodo.Where(p => p.IsDeleted == false).ToList();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void AddTodo(TbTodo p_Todo)
        {
            this.TodoDBContext.TbTodo.Add(p_Todo);
            this.TodoDBContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void DeleteTodo(int p_Id)
        {
            var todo = this.TodoDBContext.TbTodo.Find(p_Id);
            if (todo == null) return;
            todo.IsDeleted = true;
            this.TodoDBContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public void UpdateTodo(TbTodo p_Todo)
        {
            var todo = this.TodoDBContext.TbTodo.Find(p_Todo.Id);
            if (todo == null) return;
            todo.IsCompleted = p_Todo.IsCompleted;
            todo.IsDeleted = p_Todo.IsDeleted;
            todo.Title = p_Todo.Title;
            this.TodoDBContext.SaveChanges();
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }



}