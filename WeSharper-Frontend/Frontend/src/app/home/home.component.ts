import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  model: any = { }

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }
  login(){
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/feeds');
    })
  }
  
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');

  }

}