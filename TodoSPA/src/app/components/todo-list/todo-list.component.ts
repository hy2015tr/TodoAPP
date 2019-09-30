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

  constructor(private m_TodoAPIClient: TodoAPIClient) {}

  ngOnInit() {
    this.getTodos();
  }

  public getTodos(){
    this.m_TodoAPIClient.getTodos().subscribe(resp => {
      this.todos = resp;
    });
  }

  public addTodo(todo: TbTodo) {
    this.todos.push(todo);
    this.m_TodoAPIClient.addTodo(todo).subscribe();
  }

  public deleteTodo(todo: TbTodo) {
    this.m_TodoAPIClient.deleteTodo(todo.id).subscribe( () => {
      this.getTodos();
    });
  }

  public updateTodo(todo: TbTodo) {
    this.m_TodoAPIClient.updateTodo(todo).subscribe();
  }

}
