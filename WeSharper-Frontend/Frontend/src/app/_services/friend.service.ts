import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friend } from '../_models/friend';
import { Post } from '../_models/post';
import { Profile } from '../_models/profile';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class FriendService {
  ApiURL = 'https://wesharper.azurewebsites.net/api/';
  baseUrl = environment.apiUrl;
  friends: Friend[] = [];
  user: User;

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.ApiURL + 'Friend/AllFriends');
  }

  getAllFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.ApiURL + 'Friend/Friends');
  }

  getAllOutComingFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.ApiURL + 'Friend/OutcomingFriends');
  }

  getAllInComingFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.ApiURL + 'Friend/IncomingFriends');
  }

  getAllRecommendedFriends(): Observable<Profile[]> {
    return this.http.get<Profile[]>(this.ApiURL + 'Friend/RecommendFriends');
  }

  addFriend(friendID: string) {
    return this.http.post(this.ApiURL + `Friend/Friends/${friendID}/Add`, friendID);
  }

  acceptFriend(friendID: string){
    return this.http.post(this.ApiURL + `Friend/Friends/${friendID}/Accept`, friendID);
  }

  removeFriend(friendID: string){
    return this.http.post(this.ApiURL + `Friend/Friends/${friendID}/Remove`, friendID);
  }

  getFriendProfileByFriendID(friendID: string) : Observable<Profile>
  {
    return this.http.get<Profile>(this.ApiURL + `Friend/Friends/${friendID}/Profile`);
  }

  getFriendRelationshipByFriendID(friendID: string) : Observable<any> {
    return this.http.get<any>(this.ApiURL + `Friend/Friends/${friendID}/Relationship`);
  }

  getUserPosts() : Observable<any> {
    return this.http.get<any>(this.ApiURL + 'UserPost/UserPosts');
  }

  getFriendPosts(friendID: string) : Observable<any> {
    return this.http.get<any>(this.ApiURL + `Friend/Friends/${friendID}/Posts`);
  }
}
