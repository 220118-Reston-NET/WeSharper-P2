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
  listOfFriends: Friend[];
  listOfInComingRequest: Friend[];
  listOfOutComingRequest: Friend[];
  listRecommendedFriends: Profile[];
  friendID: string;
  friendUserName: string;

  constructor(private readonly friendService: FriendService,
    private readonly accountService: AccountService) {
    this.listOfFriends = [];
  }

  ngOnInit(): void {
    this.getAllFriends();
    this.getAllInComingFriends();
    this.getAllOutComingFriends();
    this.getRecommendedFriends();
    this.accountService.getProfile().subscribe(result => {
      this.profile = result;
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

  getRecommendedFriends() {
    this.friendService.getAllRecommendedFriends().subscribe(response => {
      this.listRecommendedFriends = response;
    }, error => {
      console.log(error);
    })
  }

  addFriend(friendID){
    this.friendService.addFriend(friendID).subscribe(result => console.log(result));
  }

  acceptFriend(friendID){
    this.friendService.acceptFriend(friendID).subscribe(result => console.log(result));
  }

  removeFriend(friendID){
    this.friendService.removeFriend(friendID).subscribe(result => console.log(result));
  }
}
