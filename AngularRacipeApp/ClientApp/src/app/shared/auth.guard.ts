import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const loggedInUsersDetails: any = sessionStorage.getItem('tokenKey');
    const parsedUserDetails = JSON.parse(loggedInUsersDetails);
    if (parsedUserDetails?.token) {
      return true;
    } else {
      this.router.navigate(['/users']);
      return false;
    }
  }
}
