import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {MyToken, LoginUser, RegistrationUser} from '../../../shared/interfaces';
import {Observable, Subject, throwError} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';


@Injectable()
export class AuthService {

  public error$: Subject<string> = new Subject<string>();

  constructor(private http: HttpClient) { }

  get token(): string {
    const expDate = new Date(localStorage.getItem('jwt-token-exp'));
    if (new Date() > expDate) {
      this.logout();
      return null;
    }
    return localStorage.getItem('jwt-token');
  }

  login(user: LoginUser): Observable<any>{
    return this.http.post('https://localhost:5001/api/authentication/login',
      user)
      .pipe(
        tap(this.setToken),
        catchError(this.handleError.bind(this))
      );
  }

  register(user: RegistrationUser): Observable<any>{
    return this.http.post('https://localhost:5001/api/authentication', user);
  }

  logout(): void {
    this.setToken(null);
  }

  setToken(response?: MyToken): void {
    if (response){
      localStorage.setItem('jwt-token', `Bearer ${response.token}`);
      localStorage.setItem('jwt-token-exp', new Date(response.expiresIn).toString());
    } else {
      localStorage.clear();
    }
  }

  private handleError(error: HttpErrorResponse): any {
    const message = `${error.error.status} ${error.error.title}`;
    if (message){
      this.error$.next('Wrong Username or Password');
    }

    return throwError(error);

  }

  isAuthenticated(): boolean{
    return !!this.token;
  }

}
