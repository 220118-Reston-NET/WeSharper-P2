import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerMode = false;
  model: any = {}
  //users: any;

  constructor(private http: HttpClient, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    //this.getUsers();
  }


  register(){
    this.accountService.register(this.model).subscribe(response => {
      this.router.navigateByUrl('/feeds')
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

}
