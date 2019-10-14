import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  
  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(

        catchError((err: HttpErrorResponse) => {

        const error = err.message || err.statusText;

        const errorText = document.getElementById("errorText");
        if (errorText) errorText.innerHTML = error;

        if (err.status === 401) {
          localStorage.removeItem("token");
        }

        return throwError(err);
      })
    );

  }
}
