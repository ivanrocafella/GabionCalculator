import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { UsersService } from 'src/app/components/services/users.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordMatchValidator } from 'src/app/components/validators/confirmedPass.validator';
import { PasswordConfirmationValidatorService } from 'src/app/components/validators/password-confirmation-validator.service'

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit{
  registerForm!: FormGroup;
  public errorMessage: string = '';
  public showError!: boolean;
  constructor(private usersService: UsersService, private passConfValidator: PasswordConfirmationValidatorService, private router: Router) {
  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      passwordConfirm: new FormControl('', [Validators.required]),
    }),
    this.registerForm.get('passwordConfirm')!.setValidators([Validators.required,
    this.passConfValidator.validateConfirmPassword(this.registerForm.get('password')!)]);
  }
    
  public validateControl = (controlName: string) => {
      return this.registerForm.get(controlName)!.invalid && this.registerForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
      return this.registerForm.get(controlName)!.hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.showError = false;
    const formValues = { ...registerFormValue };
    const user: RegisterUserModel = {
      UserName: formValues.userName,
      Email: formValues.email,
      Password: formValues.password,
      PasswordConfirm: formValues.passwordConfirm
    };
    console.log(user);
    this.usersService.registerUser(user)
      .subscribe({
        next: (response) => { this.router.navigate(["/User/PrivateCabinet"]), console.log("Successful registration", response) },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
          console.log(err.message)
        } 
      })
  }


  
}
