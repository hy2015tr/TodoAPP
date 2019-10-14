import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { BrowserModule } from '@angular/platform-browser';
import { JwtInterceptor } from './services/jwt.interceptor';
import { ErrorInterceptor } from './services/err.interceptor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TodoAddComponent } from './components/todo-add/todo-add.component';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { TodoItemComponent } from './components/todo-item/todo-item.component';
import { TodoAPIClient_URL, TodoAPIClient } from './services/todo.service';


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
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
