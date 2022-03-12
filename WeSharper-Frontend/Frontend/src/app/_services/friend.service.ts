import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friend } from '../_models/friend';
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
}
