import { Component, OnInit } from "@angular/core";
import { TodoAPIClient, TbTodo } from "src/app/services/todo.service-client";
import { TagPlaceholder } from '@angular/compiler/src/i18n/i18n_ast';
import { VirtualTimeScheduler } from 'rxjs';

@Component({
  selector: "app-todo-list",
  templateUrl: "./todo-list.component.html"
})
export class TodoListComponent implements OnInit {
  public todos: TbTodo[] = [];

  constructor(private todoAPIClient: TodoAPIClient) {}

  ngOnInit() {
    this.getTodos();
  }

  public getTodos(){
    this.todoAPIClient.getTodos().subscribe(resp => {
      this.todos = resp;
    });
  }

  public clearTodos() {
    this.todos = [];
  }

  public addTodo(todo: TbTodo) {
    this.todos.push(todo);
    this.todoAPIClient.addTodo(todo).subscribe();
  }

  public deleteTodo(todo: TbTodo) {
    // this.todos = this.todos.filter(t => t.title !== todo.title);
    // this.todoAPIClient.deleteTodo(todo.id).subscribe();

    this.todoAPIClient.deleteTodo(todo.id).subscribe( () => {
      this.getTodos();
    });

  }

  public updateTodo(todo: TbTodo) {
    this.todoAPIClient.updateTodo(todo).subscribe();
  }

}
