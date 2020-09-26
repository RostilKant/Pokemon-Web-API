import {FormGroup} from '@angular/forms';


export class MyValidators {
  static checkPass(group: FormGroup): {[key: string]: boolean} {
    const pass = group.get('password').value;
    const confirmPass = group.get('confirmPassword').value;

    return pass === confirmPass ? null : { passwordMismatch: true };
  }
}
