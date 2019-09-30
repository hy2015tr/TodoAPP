import { Router } from '@angular/router';
import { Todo } from 'src/app/models/todo';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-todo-add',
  templateUrl: './todo-add.component.html',
  styleUrls: ['./todo-add.component.css']

})
export class TodoAddComponent {

  @Output() addTodo = new EventEmitter();
  public newTodoText: string;

  constructor(private m_Router: Router) {}

  public onSubmit() {
    // Check Empty
    if (this.newTodoText ==="") return;

    // Create New
    const todo:Todo = {
      id: 0,
      title : this.newTodoText,
      isCompleted : false,
      isDeleted : false
    }
    
    // Add
    this.addTodo.emit(todo);
    
    // Reset
    this.newTodoText = "";
  }

  public onLogOut(){
    localStorage.removeItem('token');
    this.m_Router.navigate(['login']);
  }

}
