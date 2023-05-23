import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/components/services/users.service';
import { LoginUserModel } from 'src/app/models/loginUserModel.model';
import { AuthUserModel } from 'src/app/models/authResponseModel.model';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-authentification',
  templateUrl: './user-authentification.component.html',
  styleUrls: ['./user-authentification.component.css']
})
export class UserAuthentificationComponent implements OnInit{
  private returnUrl?: string;

  loginForm!: FormGroup;
  errorMessage: string = '';
  showError!: boolean;

  constructor(private usersService: UsersService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      emailLogin: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      rememberMe: new FormControl(false)
    }),
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public validateControl = (controlName: string) => {
    return this.loginForm.get(controlName)!.invalid && this.loginForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.get(controlName)!.hasError(errorName)
  }

  public loginUser = (loginFormValue: any) => {
    this.showError = false;
    const login = { ...loginFormValue };
    const loginUserModel: LoginUserModel = {
      EmailLogin: login.emailLogin,
      Password: login.password,
      RememberMe: login.rememberMe
    };
    console.log(loginUserModel);
    this.usersService.loginUser(loginUserModel)
      .subscribe({
        next: (response: AuthUserModel) => {
          localStorage.setItem("token", response.Token);
          this.router.navigate([this.returnUrl]);
          console.log("Successful authentification", response)
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
          console.log(err.message)
        }})
  }
}
