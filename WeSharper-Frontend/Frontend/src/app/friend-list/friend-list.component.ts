import { Component, OnInit } from '@angular/core';
import { Friend } from '../_models/friend';
import { FriendService } from '../_services/friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  listOfFriends: any[];

  constructor(private friendService: FriendService) { 
    this.listOfFriends = [];
  }

  ngOnInit(): void {
    this.getAllFriends();
  }

  getAllFriends(){
    this.friendService.getAllFriends().subscribe(response => {
      console.log(response);
      this.listOfFriends = response;
    }, error => {
      console.log(error);
    })
  }

}
