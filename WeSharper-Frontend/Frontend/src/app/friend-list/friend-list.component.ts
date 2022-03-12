import { Component, OnInit } from '@angular/core';
import { Friend } from '../_models/friend';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';
import { FriendService } from '../_services/friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  profile: Profile;
  listOfUsers: Friend[]
  listOfFriends: Friend[];
  listOfInComingRequest: Friend[];
  listOfOutComingRequest: Friend[];


  constructor(private readonly friendService: FriendService,
    private readonly accountService: AccountService) {
    this.listOfUsers = [];
    this.listOfFriends = [];
  }

  ngOnInit(): void {
    this.getAllUsers();
    this.getAllFriends();
    this.getAllInComingFriends();
    this.getAllOutComingFriends();
    this.accountService.getProfile().subscribe(result => {
      this.profile = result;
    })
  }

  getAllUsers() {
    this.friendService.getAllUsers().subscribe(response => {
      console.log(response);
      this.listOfUsers = response;
    }, error => {
      console.log(error);
    })
  }

  getAllFriends() {
    this.friendService.getAllFriends().subscribe(res => {
      console.log(res);
      this.listOfFriends = res;
    }, error => {
      console.log(error);
    })
  }

  getAllOutComingFriends() {
    this.friendService.getAllOutComingFriends().subscribe(res => {
      console.log(res);
      this.listOfOutComingRequest = res;
    }, error => {
      console.log(error);
    })
  }

  getAllInComingFriends() {
    this.friendService.getAllInComingFriends().subscribe(response => {
      this.listOfInComingRequest = response;
    }, error => {
      console.log(error);
    })
  }
}
