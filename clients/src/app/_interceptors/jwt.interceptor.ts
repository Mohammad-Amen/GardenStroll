import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor} from '@angular/common/http';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class jwtInterceptor  implements HttpInterceptor
{
 constructor(public accountService: AccountService) {}

 intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
  {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      if (user) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${user.token}`
          }
        })
      }
    })
    return next.handle(request);
  }
}