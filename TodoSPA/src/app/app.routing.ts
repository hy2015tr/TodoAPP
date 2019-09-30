import { AuthGuard } from './services/auth.guard';
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./components/login/login.component";
import { TodoListComponent } from "./components/todo-list/todo-list.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "todos", component: TodoListComponent,canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'login' },
];

export const AppRoutingModule = RouterModule.forRoot(routes);
