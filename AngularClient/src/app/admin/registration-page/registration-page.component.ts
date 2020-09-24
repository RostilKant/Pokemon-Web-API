import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../shared/services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {MyValidators} from '../../shared/my.validators';
import {RegistrationUser} from '../../shared/interfaces';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss']
})
export class RegistrationPageComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  error$ = this.auth.error$;
  message;
  hide = true;

  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      if (params.loginAgain) {
        // this.message = 'Please, login as admin';
        this.message = 'Session expired. Please, input your credentials again.';
      } else if (params.authFailed) {
        this.message = 'Session expired. Please, input your credentials again.';
      }
    });

    this.form = new FormGroup({
      firstName: new FormControl(null,
        [Validators.required]),
      lastName: new FormControl(null,
        [Validators.required]),
      userName: new FormControl(null,
        [Validators.required, MyValidators.containNumber(/\d/)]),
      email: new FormControl(null,
        [Validators.required]),
      phone: new FormControl(null),
      password: new FormControl(null,
        [Validators.required, Validators.minLength(8)])
    });
    localStorage.clear();
  }

  getErrorMessage(control: string, secondPartOfControl: string = ''): string {
    if (this.form.get(control.toLocaleLowerCase() + secondPartOfControl).errors.required) {
      return `${control + secondPartOfControl} is required`;
    }
    if (this.form.get(control.toLocaleLowerCase() + secondPartOfControl).errors.minlength) {
      return `${control} must contain at least
      ${this.form.get('password').errors.minlength.requiredLength} symbols`;
    }

    if (this.form.get(control.toLocaleLowerCase() + secondPartOfControl).errors.containNumber) {
      return `UserName must contain at least one number`;
    }
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    const user: RegistrationUser = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      userName: this.form.value.userName,
      email: this.form.value.email,
      phoneNumber: this.form.value.phone,
      password: this.form.value.password,
      roles: ['User']
    };

    this.auth.register(user).subscribe(() => {
      this.form.reset();
      this.router.navigate(['/admin', 'login']);
      this.submitted = false;
    }, (error: HttpErrorResponse) => {
      this.message = error.error.DuplicateEmail;
      this.submitted = false;
    });
  }

}
