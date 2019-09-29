import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Todo } from 'src/app/models/todo';
import { TouchSequence } from 'selenium-webdriver';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
})
export class TodoItemComponent {

  @Input() todo: Todo;
  @Output() deleteTodo = new EventEmitter();
  @Output() updateTodo = new EventEmitter();

  public onDelete(){
    this.deleteTodo.emit(this.todo);
  }

  public onUpdate(){
    this.todo.isCompleted = !this.todo.isCompleted;
    this.updateTodo.emit(this.todo);
  }

  public checkComplete(){
    return this.todo.isCompleted ? "is-complete":"";
  }

}
