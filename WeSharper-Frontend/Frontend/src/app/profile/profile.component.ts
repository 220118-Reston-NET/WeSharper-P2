import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';
import { FriendService } from '../_services/friend.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile: Profile;
  friendID: string | null;
  friendRelationship: string;

  constructor(private router:ActivatedRoute,
              private readonly friendService: FriendService,
              public accountService: AccountService) { }

  ngOnInit(): void {
    this.friendID = this.router.snapshot.paramMap.get("friendID");
    if (this.friendID == null){
      this.accountService.getProfile().subscribe(result => {
        this.profile = result;
      })
    } else {
      this.friendService.getFriendProfileByFriendID(this.friendID).subscribe(result => {
      this.profile = result; 
      })
      this.friendService.getFriendRelationshipByFriendID(this.friendID).subscribe(result => {
        this.friendRelationship = result.results;
      })
    }
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
