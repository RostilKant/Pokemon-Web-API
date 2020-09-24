import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {LoginUser} from '../../shared/interfaces';
import {AuthService} from '../shared/services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {MyValidators} from '../../shared/my.validators';
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

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
      userName: new FormControl(null,
        [Validators.required, MyValidators.containNumber(/\d/)]),
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

    const user: LoginUser = {
      userName: this.form.value.userName,
      password: this.form.value.password
    };

    this.auth.login(user).subscribe(() => {
      this.form.reset();
      this.router.navigate(['/admin', 'dashboard']);
      this.submitted = false;
    }, () => {
      this.submitted = false;
    });
  }
}
