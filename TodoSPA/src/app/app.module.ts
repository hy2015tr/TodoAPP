import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { AuthGuard } from './services/auth.guard';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TodoAddComponent } from './components/todo-add/todo-add.component';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { TodoItemComponent } from './components/todo-item/todo-item.component';
import { TodoAPIClient_URL, TodoAPIClient } from './services/todo.service-client';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TodoAddComponent,
    TodoItemComponent,
    TodoListComponent, 
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    TodoAPIClient,
    { provide: TodoAPIClient_URL, useValue: 'http://localhost:5000' },    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
