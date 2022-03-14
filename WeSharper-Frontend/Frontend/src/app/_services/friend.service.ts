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
  baseUrl = environment.apiUrl;
  friends: Friend[] = [];
  user: User;

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.baseUrl + 'Friend/AllFriends');
  }

  getAllFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.baseUrl + 'Friend/Friends');
  }

  getAllOutComingFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.baseUrl + 'Friend/OutcomingFriends');
  }

  getAllInComingFriends(): Observable<Friend[]> {
    return this.http.get<Friend[]>(this.baseUrl + 'Friend/IncomingFriends');
  }

  getAllRecommendedFriends(): Observable<Profile[]> {
    return this.http.get<Profile[]>(this.baseUrl + 'Friend/RecommendFriends');
  }

  addFriend(friendID: string) {
    return this.http.post(this.baseUrl + `Friend/Friends/${friendID}/Add`, friendID);
  }

  acceptFriend(friendID: string){
    return this.http.post(this.baseUrl + `Friend/Friends/${friendID}/Accept`, friendID);
  }

  removeFriend(friendID: string){
    return this.http.post(this.baseUrl + `Friend/Friends/${friendID}/Remove`, friendID);
  }

  getFriendProfileByFriendID(friendID: string) : Observable<Profile>
  {
    return this.http.get<Profile>(this.baseUrl + `Friend/Friends/${friendID}/Profile`);
  }

  getFriendRelationshipByFriendID(friendID: string) : Observable<any> {
    return this.http.get<any>(this.baseUrl + `Friend/Friends/${friendID}/Relationship`);
  }

  getUserPosts() : Observable<any> {
    return this.http.get<any>(this.baseUrl + 'UserPost/UserPosts');
  }

  getFriendPosts(friendID: string) : Observable<any> {
    return this.http.get<any>(this.baseUrl + `Friend/Friends/${friendID}/Posts`);
  }
}
