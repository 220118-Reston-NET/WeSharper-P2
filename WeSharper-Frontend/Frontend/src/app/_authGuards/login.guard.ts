import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private readonly accountService: AccountService, private readonly router: Router) { }

  canActivate(): boolean {
    if (this.accountService.currentUser) {
      return true;
    } else {
      this.router.navigateByUrl('')
      return false;
    }
  }
}