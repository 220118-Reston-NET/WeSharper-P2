import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-group-post',
  templateUrl: './new-group-post.component.html',
  styleUrls: ['./new-group-post.component.css']
})



export class NewGroupPostComponent /*implements OnInit*/ {
  selectedFile: File | null = null;

  constructor(private http: HttpClient) { }

  onFileSelect(event: { target: { files: File[]; }; }) {
    this.selectedFile = <File>event.target.files[0];
  }


  onUpload() {
    const fd = new FormData;
    if (this.selectedFile !== null) {
      fd.append('image', this.selectedFile, this.selectedFile?.name)
    }
    this.http.post('upload url here', fd)
      .subscribe(res => {
        console.log(res);
      });
  }

  ngOnInit(): void {
  }

}