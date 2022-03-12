import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/post';
import { Profile } from '../_models/profile';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'Authentication/Login', model, {responseType: 'json'}).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          console.log(user.token);
          this.currentUserSource.next(user);
        }
      })
    ) 
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'Authentication/Register', model, {responseType: 'json'}).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    user.friend = [];
    const token = this.getDecodeToken(user.token);
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.router.navigateByUrl('login');
    this.currentUserSource.next(null);
  }

  getDecodeToken(token){
    return JSON.parse(atob(token.split('.')[1]));
  }

  getProfile() : Observable<Profile>
    {
      return this.http.get<Profile>(this.baseUrl + 'User/Profile');
    }

  getPost() : Observable<Post>
  {
    return this.http.get<Post>(this.baseUrl + 'UserPost/UserPosts');
  }
}

