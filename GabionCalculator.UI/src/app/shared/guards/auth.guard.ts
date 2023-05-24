import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UsersService } from 'src/app/components/services/users.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate{

  constructor(private usersService: UsersService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log(false)
    if (this.usersService.isUserAuthenticated()) {
      console.log(true)
      return true;  
    }
    else {
       this.router.navigate(['/User/Login'], { queryParams: { returnUrl: state.url } });
       return false;
    }   
  }  
}
