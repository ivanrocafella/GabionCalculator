import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {
  constructor(private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = this.handleError(error);
          console.log(errorMessage)
          return throwError(() => new Error(errorMessage!));
        })
      )
  }

  private handleError = (error: HttpErrorResponse): string | null => {
    if (error.status === 404) {
      return this.handleNotFound(error);
    }
    else if (error.status === 400) {
      console.log(error.status);
      return this.handleBadRequest(error);
    }
    else if (error.status === 401) {
      return this.handleUnauthorized(error);
    }
    else if (error.status === 403) {
      console.log(error.status);
      return this.handleForbidden(error);
    }
    else {
      return null;
    }
  }
  
  private handleNotFound = (error: HttpErrorResponse): string => {
    this.router.navigate(['/404']);
    return error.message;
  }

  private handleBadRequest = (error: HttpErrorResponse): string | null => {
    console.log(this.router.url);
    if (this.router.url === '/User/Register' || this.router.url === '/' || this.router.url.includes('/User/ChangePassword/')) {
      let message = '';
      const values = Object.values(error.error.errors);
      values.map((m: string | unknown) => {
        message += m + '<br>';
      })
      console.log(message.slice(0, -4))
      return message.slice(0, -4);
    }
    else {
      return error.error ? error.error : error.message;
    }
  }

  private handleUnauthorized = (error: HttpErrorResponse) => {
    if (this.router.url === '/User/Login') {
      let message = '';
      const values = Object.values(error.error.errors);
      values.map((m: string | unknown) => {
        message += m + '<br>';
      })
      return message.slice(0, -4);
    }
    else {
      this.router.navigate(['/User/Login']);
      return error.message;
    }
  }

  private handleForbidden = (error: HttpErrorResponse) => {
    this.router.navigate(["/Forbidden"], { queryParams: { returnUrl: this.router.url } });
    return "Forbidden";
  } 
}
