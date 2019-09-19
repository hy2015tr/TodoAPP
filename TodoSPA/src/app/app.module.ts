import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AddTodoComponent } from './components/todo-add/todo-add.component';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { TodoItemComponent } from './components/todo-item/todo-item.component';
import { TodoAPIClient_URL, TodoAPIClient } from './services/todo.service-client';

@NgModule({
  declarations: [
    AppComponent,
    AddTodoComponent,
    TodoItemComponent,
    TodoListComponent, 
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    TodoAPIClient,
    { provide: TodoAPIClient_URL, useValue: 'http://localhost:5000' },    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
