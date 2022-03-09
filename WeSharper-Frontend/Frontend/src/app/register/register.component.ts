import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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

  constructor(private http: HttpClient, private accountService: AccountService) { }

  ngOnInit(): void {
    //this.getUsers();
  }


  register(){
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  // getUsers(){
  //   this.http.get('https://localhost:7133/api/User/Profile').subscribe(users => this.users = users );
  // }
}
