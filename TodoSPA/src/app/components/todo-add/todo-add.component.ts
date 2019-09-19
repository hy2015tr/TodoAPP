import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Todo } from 'src/app/models/todo';


@Component({
  selector: 'app-todo-add',
  templateUrl: './todo-add.component.html',
  styleUrls: ['./todo-add.component.css']

})
export class AddTodoComponent {

  @Output() addTodo = new EventEmitter();
  @Output() clearTodos = new EventEmitter();
  public newTitle: string;

  public onSubmit() {
    // Check Empty
    if (this.newTitle ==="") return;

    // Create New
    const todo:Todo = {
      id: 0,
      title : this.newTitle,
      isCompleted : false,
      isDeleted : false
    }
    
    // Add
    this.addTodo.emit(todo);
    
    // Reset
    this.newTitle = "";
  }

  public onClearAll(){
    // Clear All
    this.clearTodos.emit();
  }

}
