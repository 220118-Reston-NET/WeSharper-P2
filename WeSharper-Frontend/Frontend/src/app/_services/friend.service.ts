import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friend } from '../_models/friend';

@Injectable({
  providedIn: 'root'
})
export class FriendService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllFriends(): Observable<Friend[]>
  {
    //return this.http.get<Friend[]>(this.baseUrl + 'Friend/Friend');
    return this.http.get<Friend[]>(this.baseUrl + 'Friend/Friend');
  }
}
