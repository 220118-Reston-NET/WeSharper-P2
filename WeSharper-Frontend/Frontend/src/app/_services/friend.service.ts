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

  getAllFriends(): Observable<any[]>
  {
    return this.http.get<any[]>(this.baseUrl + 'Friend/AllFriends');
  }
}
