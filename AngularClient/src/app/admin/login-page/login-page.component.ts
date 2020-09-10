import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../../shared/interfaces';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl(null,
        [Validators.email, Validators.required]),
      password: new FormControl(null,
        [Validators.required, Validators.minLength(6)])
    });
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    const user: User = {
      email: this.form.value.email,
      password: this.form.value.password
    };
  }


  getEmailErrorMessage(): string {
    if (this.form.get('email').errors.required){
      return 'You must enter a value';
    }
    if (this.form.get('email').errors.email){
      return 'Invalid email';
    }
  }

  getPasswordErrorMessage(): string {
    if (this.form.get('password').errors.required){
      return 'You must enter a value';
    }
    if (this.form.get('password').errors.minlength){
      return `You must enter at least
      ${this.form.get('password').errors.minlength.requiredLength} symbols`;
    }
  }
}
