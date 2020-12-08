import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from 'rxjs';
import { store } from '../redux/store';

@Injectable()
export class JwtInterceptorService implements HttpInterceptor {

    constructor() { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            setHeaders: {
                Authorization: "Bearer " + store.getState().business?.jwtToken
            }            
        });
        return next.handle(request);
    }
}
