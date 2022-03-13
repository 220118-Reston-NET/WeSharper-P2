import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Profile } from '../_models/profile';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {
  baseUrl = environment.apiUrl;
  progress: number;
  message: string;
  response: {fileURL: ''};

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

    this.http.post(this.baseUrl + 'User/ProfilePicture', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload successfully!';
          this.onUploadFinished.emit(event.body);
        }
      })
  }
}
