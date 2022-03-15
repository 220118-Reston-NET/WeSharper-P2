import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';
import { FriendService } from '../_services/friend.service';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from '../_models/message';
import { MessageService } from '../_services/message.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';
import { PresenceService } from '../_services/presence.service';
import { Post } from '../_models/post';
import { FormControl, FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  ApiURL = environment.apiUrl;
  activeTab: TabDirective;
  profile: Profile;
  user: User;
  friendID: string | null;
  friendUserName: string;
  friendRelationship: string;
  messages: Message[] = [];

  userPosts: any[];
  friendPosts: any[];
  postGroup = new FormGroup({
    postContent: new FormControl("")
  });

  constructor(private route: ActivatedRoute,
              private readonly friendService: FriendService,
              public presence: PresenceService,
              private messageService: MessageService,
              private accountService: AccountService,
              private router: Router,
              private readonly http: HttpClient) { 
      this.accountService.currentUser.pipe(take(1)).subscribe(user => this.user = user);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {
    this.friendID = this.route.snapshot.paramMap.get("friendID");

    if (this.friendID == null){
      this.accountService.getProfile().subscribe(result => {
        this.profile = result;
      })

      this.friendService.getUserPosts().subscribe(result => {
        this.userPosts = result;
      })

      console.log(this.userPosts);
    } else {
      this.friendService.getFriendProfileByFriendID(this.friendID).subscribe(result => {
      this.profile = result; 
      })
      this.friendService.getFriendRelationshipByFriendID(this.friendID).subscribe(result => {
        this.friendRelationship = result.results;
      })

      this.friendService.getFriendPosts(this.friendID).subscribe(result => {
        this.friendPosts = result;
      });
    }

    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
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

  loadMessages() {
    this.messageService.getMessageThread(this.profile.user.userName).subscribe(messages => {
      this.messages = messages;
    })
  }

  getFriendPosts(friendID){
    this.friendService.getFriendPosts(friendID).subscribe(result => {
      this.friendPosts = result;
    });
  }

  postPhotoUrl = "";

  onFileSelected(e: { target: { files: Blob[]; }; }) {
    if (e.target.files) {
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload = (event: any) => {
        this.postPhotoUrl = event.target.result;
      }
    } else {
      return;
    }


    let fileToUpload = <File>e.target.files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post<any>(this.ApiURL + 'UserPost/UserPosts/UploadImage', formData)
    .subscribe(data => {
      this.postPhotoUrl = data.fileURL;
    })
  }

  postNewPost(postGroup: FormGroup) {
    let post:Post = {
      postContent: postGroup.get("postContent")?.value,
      createdAt: new Date,
      isDeleted: false,
      postComments: [],
      postReacts: [],
      postId: "",
      postPhoto: this.postPhotoUrl,
      userId: "",
    }

    this.accountService.postNewPost(post).subscribe();
  }

  deletePost(postID: string) {
    this.accountService.deletePost(postID).subscribe();
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      this.messageService.createHubConnection(this.user, this.profile.user.userName);
    } else {
      this.messageService.stopHubConnection();
    }
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
