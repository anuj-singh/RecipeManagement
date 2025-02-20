import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let loggedInUsersDetails = sessionStorage.getItem('tokenKey');

    let token: string | null = null; // Initialize token

    if (loggedInUsersDetails) {
      const parsedAdmin = JSON.parse(loggedInUsersDetails);
      token = parsedAdmin.token; // Extract token
    }

    // Only set the Authorization header if token is available
    const authRequest = request.clone({
      headers: request.headers.set('Authorization', `Bearer ${token}`),
    });

    return next.handle(authRequest).pipe(
      catchError((err: HttpErrorResponse) => {
        let errorMessage = '';
        if (err.status === 401) {
          errorMessage = err.error.message;
          alert(errorMessage);
        }else{
          alert("Something went wrong!")
        }

        return throwError(errorMessage);
      })
    );
  }
}
