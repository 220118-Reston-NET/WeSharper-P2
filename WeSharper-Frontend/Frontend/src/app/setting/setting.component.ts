import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {
  ApiURL = 'https://wesharper.azurewebsites.net/api/';
  baseUrl = environment.apiUrl;
  progress: number;
  message: string;
  response: {fileURL: ''};

  profileGroup = new FormGroup({
    userFirstName: new FormControl(""),
    userLastName: new FormControl(""),
    userBio: new FormControl("")
  });

  profile:Profile;
  @Output() onUploadFinished = new EventEmitter();

  constructor(public accountService: AccountService, private readonly http: HttpClient) { }

  ngOnInit(): void {
    this.accountService.getProfile().subscribe(result => {
      this.profile = result;
    })
  }

  uploadFile = (files) => {
    if (files.length === 0){
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(this.ApiURL + 'User/ProfilePicture', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload successfully!';
          this.onUploadFinished.emit(event.body);
        }
      })
  }

  updateProfile(profileGroup: FormGroup) {
    let profile:Profile = {
      bio: profileGroup.get("userBio")?.value,
      createdAt: this.profile.createdAt,
      firstName: profileGroup.get("userFirstName")?.value,
      lastName: profileGroup.get("userLastName")?.value,
      profileId: this.profile.profileId,
      profilePictureUrl: this.profile.profilePictureUrl,
      user: this.profile.user,
      userId: this.profile.userId
    }

    this.accountService.updateProfile(profile).subscribe();
  }
}
