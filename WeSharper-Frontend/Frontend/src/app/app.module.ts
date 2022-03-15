import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { NavComponent } from './nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from './profile/profile.component';
import { FeedsComponent } from './feeds/feeds.component';
import { GroupsComponent } from './groups/groups.component';
import { FriendsComponent } from './friends/friends.component';
import { SettingComponent } from './setting/setting.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FriendListComponent } from './friend-list/friend-list.component';
import { ToastrModule } from 'ngx-toastr';
import { NewPostComponent } from './new-post/new-post.component';
import { NewGroupPostComponent } from './new-group-post/new-group-post.component';
import { PostComponent } from './post/post.component';
import { ProfileUserComponent } from './profile-user/profile-user.component';
import { MessagesComponent } from './messages/messages.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoaderInterceptor } from './_interceptors/loader.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SharedModule } from './_modules/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    NavComponent,
    ProfileComponent,
    FeedsComponent,
    GroupsComponent,
    FriendsComponent,
    SettingComponent,
    FriendListComponent,
    NewPostComponent,
    NewGroupPostComponent,
    PostComponent,
    ProfileUserComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FontAwesomeModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    SharedModule,
    NgxSpinnerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
