import { AbstractControl, ValidationErrors } from '@angular/forms';

export function customValidator(control: AbstractControl): ValidationErrors | null {
  // Perform validation logic
  // If validation fails, return a validation error
  // If validation passes, return null

  // Example: Validate that the input contains only letters
  const value: string = control.value;
  if (!/^[a-zA-Z]+$/.test(value)) {
    return { customError: true };
  }

  return null;
}
