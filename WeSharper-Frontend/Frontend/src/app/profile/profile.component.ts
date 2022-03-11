import { Component, OnInit } from '@angular/core';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile:Profile;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.getProfile().subscribe(result => {
      this.profile = result;
    })
  }
  
}
