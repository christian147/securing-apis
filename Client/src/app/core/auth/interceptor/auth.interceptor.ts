import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { from, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthenticationService } from '@app/core/auth/services/authentication.service';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(public authenticationService: AuthenticationService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return from(this.authenticationService.getAccessToken())
      .pipe(
        switchMap(token => {
          request = request.clone({
            url: `${environment.apiUrl}/${request.url}`,
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          });
          return next.handle(request);
        }));
  }
}
