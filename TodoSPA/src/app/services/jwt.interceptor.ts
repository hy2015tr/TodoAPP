import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const errorText = document.getElementById("errorText");
        if (errorText) errorText.innerHTML = '';

        const userToken = localStorage.getItem("token");

        if (userToken) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${userToken}`
                }
            });
        }

        return next.handle(request);
    }
}