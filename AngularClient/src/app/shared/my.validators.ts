import {AbstractControl, ValidatorFn} from '@angular/forms';

export class MyValidators {
  static containNumber(nameRe: RegExp): ValidatorFn {
    return (control: AbstractControl): {[key: string]: any} | null => {
      const containNumber = nameRe.test(control.value);
      return containNumber ? null : {containNumber: {value: control.value}};
    };
  }
}
