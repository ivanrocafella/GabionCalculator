import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CellDividinngValidatorService {

  constructor() { }

  public dividingValidator = (min: number, max: number, divide: number): ValidatorFn => {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const value = control.value;
      if (value >= min && value <= max && value % divide !== 0) {
        console.log("dividingValidator");
        return { noDivide: true };
      }
      console.log("succes");
      return null;
    }
  }
}
