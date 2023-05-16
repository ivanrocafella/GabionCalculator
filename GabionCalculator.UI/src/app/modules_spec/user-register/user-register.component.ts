import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { UsersService } from 'src/app/components/services/users.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit{
  registerForm: FormGroup;
  constructor(private usersService: UsersService, fb: FormBuilder) {
    this.registerForm = fb.group({
      userName: fb.control('', [Validators.required]),
      email: fb.control('', [Validators.required, Validators.email]),
      password: fb.control('', [Validators.required]),
      passwordConfirm: fb.control('')
    },
      {
        validator: this.ConfirmedValidator('password', 'passwordConfirm'),
      }
    );
  }

  ngOnInit(): void {
    this.registerForm.reset();
  }
    
  public validateControl = (controlName: string) => {
      return this.registerForm.get(controlName)!.invalid && this.registerForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
      return this.registerForm.get(controlName)!.hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
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
        next: (response) => console.log("Successful registration", response),
        error: (err: HttpErrorResponse) => console.log(err.error.errors)
      })
  }

  ConfirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors['confirmedValidator']
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
  
}
