import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PasswordConfirmationValidatorService {

  constructor() { }

  public validateConfirmPassword = (passwordControl: AbstractControl): ValidatorFn => {
    return (confirmationControl: AbstractControl): { [key: string]: boolean } | null => {
      const confirmValue = confirmationControl.value;
      const passwordValue = passwordControl.value;
      console.log(confirmValue, passwordValue)
      console.log('PasswordConfirmationValidatorService')
      if (confirmValue === '') {
        return null;
      }
      if (confirmValue !== passwordValue) {
        return { mustMatch: true }
      }
      return null;
    };
  }
}
