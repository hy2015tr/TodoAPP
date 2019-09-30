import { Router } from "@angular/router";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { TodoAPIClient, TbUser } from "src/app/services/todo.service-client";
import { catchError } from "rxjs/operators";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html"
})
export class LoginComponent implements OnInit {
  public requestForm: FormGroup;
  public errorText: string = null;

  constructor(
    private m_Router: Router,
    private m_FormBuilder: FormBuilder,
    private m_TodoAPIClient: TodoAPIClient
  ) {}

  ngOnInit() {
    this.requestForm = this.m_FormBuilder.group({
      username: ["", Validators.required],
      password: ["", Validators.required]
    });
  }

  public onLoginClick() {
    // User
    let user: TbUser = new TbUser();
    user.username = this.requestForm.controls["username"].value;
    user.password = this.requestForm.controls["password"].value;
    // Login
    this.m_TodoAPIClient.login(user).subscribe(
      resp => {
        this.errorText = null;
        localStorage.setItem("token", resp.token);
        this.m_Router.navigate(["todos"]);
      },
      err => (this.errorText = JSON.parse(err.response).message)
    );
  }
}
