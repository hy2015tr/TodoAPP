import { Component, OnInit } from "@angular/core";
import { Todo } from "src/app/models/todo";
import { TodoAPIClient, TbTodo } from "src/app/services/todo.service-client";

@Component({
  selector: "app-todo-list",
  templateUrl: "./todo-list.component.html"
})
export class TodoListComponent implements OnInit {
  public todos: TbTodo[] = [];

  constructor(private todoAPIClient: TodoAPIClient) {}

  ngOnInit() {
    this.todoAPIClient.getTodos().subscribe(resp => {
      this.todos = resp;
    });
  }

  public addTodo(todo: TbTodo) {
    this.todos.push(todo);
    this.todoAPIClient.addTodo(todo).subscribe();
  }

  public clearTodos() {
    this.todos = [];
  }

  public deleteTodo(todo: TbTodo) {
    // Delete from UI
    this.todos = this.todos.filter(t => t.title !== todo.title);

    // Delete from DB
    this.todoAPIClient.deleteTodo(todo.id).subscribe();
  }
}
