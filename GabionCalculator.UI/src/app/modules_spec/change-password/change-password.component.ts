import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from 'src/app/components/services/users.service'
import { PasswordConfirmationValidatorService } from '../../components/validators/password-confirmation-validator.service';
import { ApiResultChangePasswordUserModel } from '../../models/apiResultChangePasswordUserModel';
import { ChangePasswordUserModel } from '../../models/changePasswordUserModel';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit{
  id: string = '';
  public errorMessage: string = '';
  public showError!: boolean;
  changePasswordForm!: FormGroup;
  apiResult: Partial<ApiResultChangePasswordUserModel> = {};

  constructor(private usersSrvice: UsersService, private passConfValidator: PasswordConfirmationValidatorService, private route: ActivatedRoute, private router: Router) { };

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required]),
      newPasswordConfirm: new FormControl('')
    }),
      this.changePasswordForm.get('newPasswordConfirm')!.setValidators([Validators.required,
        this.passConfValidator.validateConfirmPassword(this.changePasswordForm.get('newPassword')!)]);
  }

  public changePasswordUser = (changePasswordFormValue: any) => {
    this.showError = false;
    const formValues = { ...changePasswordFormValue };
    const changePasswordUserModel: ChangePasswordUserModel = {
        OldPassword: formValues.oldPassword,
        NewPassword: formValues.newPassword,
        NewPasswordConfirm: formValues.newPasswordConfirm,
        Id: '',
        UserName: ''
    };
    console.log(changePasswordUserModel);
    this.usersSrvice.changePasswordUser(this.id,changePasswordUserModel)
      .subscribe({
        next: (response) => { this.router.navigate(["/User/PrivateCabinet"]), console.log("Successful registration", response) },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }

  public validateControl = (controlName: string) => {
    return this.changePasswordForm.get(controlName)!.invalid && this.changePasswordForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.changePasswordForm.get(controlName)!.hasError(errorName)
  }

}
