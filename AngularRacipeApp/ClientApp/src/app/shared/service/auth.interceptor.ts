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
    const authToken = 'abcxyz';
    const authRequest = request.clone({
      headers: request.headers.set('Authorization', `Bearer ${authToken}`),
    });

    return next.handle(authRequest).pipe(
      catchError((err: HttpErrorResponse) => {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
          errorMessage = `Error: ${err.error.message}`;
        } else {
          errorMessage = `Error Code: ${err.status}\nMessage: ${err.message}`;
        }

        console.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
