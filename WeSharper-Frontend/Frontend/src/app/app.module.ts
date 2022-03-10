import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
//import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
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
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FriendListComponent } from './friend-list/friend-list.component';
import { ToastrModule } from 'ngx-toastr';

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
    FriendListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass:"toast-bottom-right"
    }),
    //BsDropdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
