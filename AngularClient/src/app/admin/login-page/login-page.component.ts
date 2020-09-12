import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../../shared/interfaces';
import {AuthService} from '../shared/services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
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

  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      if (params.loginAgain) {
        this.message = 'Please, login as admin';
      }
    });

    this.form = new FormGroup({
      userName: new FormControl(null,
        [Validators.required]),
      password: new FormControl(null,
        [Validators.required, Validators.minLength(8)])
    });
    localStorage.clear();
  }

  getUserNameErrorMessage(): string {
    if (this.form.get('userName').errors.required){
      return 'You must enter a value';
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

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    const user: User = {
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
