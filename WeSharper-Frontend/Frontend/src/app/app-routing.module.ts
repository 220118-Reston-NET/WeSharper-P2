import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedsComponent } from './feeds/feeds.component';
import { FriendsComponent } from './friends/friends.component';
import { GroupsComponent } from './groups/groups.component';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';
import { SettingComponent } from './setting/setting.component';
import { AuthGuard } from './_authGuards/auth.guard';
import { LoginGuard } from './_authGuards/login.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'profile', component: ProfileComponent },
      { path: 'profile/:friendID', component: ProfileComponent },
      { path: 'feeds', component: FeedsComponent },
      { path: 'friends', component: FriendsComponent },
      { path: 'groups', component: GroupsComponent },
      { path: 'setting', component: SettingComponent },
      // { path: 'messages', component: MessagesComponent },
      { path: '**', redirectTo: 'feeds' }
    ]
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
