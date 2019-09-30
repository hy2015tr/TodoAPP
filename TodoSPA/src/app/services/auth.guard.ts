import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private m_Router: Router) {}

  public canActivate(): boolean {
    if (localStorage.getItem("token")) {
      return true;
    } else {
      this.m_Router.navigate(["login"]);
      return false;
    }
  }
}
