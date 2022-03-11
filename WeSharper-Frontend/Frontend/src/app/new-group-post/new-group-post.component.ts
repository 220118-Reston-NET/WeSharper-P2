import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faImage } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-new-group-post',
  templateUrl: './new-group-post.component.html',
  styleUrls: ['./new-group-post.component.css']
})



export class NewGroupPostComponent /*implements OnInit*/ {
  selectedFile: File | null = null;

  faImage = faImage;

  constructor(private http: HttpClient) { }


  onUpload() {
    const fd = new FormData;
    if (this.selectedFile !== null) {
      fd.append('image', this.selectedFile, this.selectedFile?.name)
    }
    this.http.post('url request insert here', fd)
      .subscribe(res => {
        console.log(res);
      });
  }

  ngOnInit(): void {
  }

  url = null;

  onFileSelected(e: { target: { files: Blob[]; }; }) {
    if (e.target.files) {
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload = (event: any) => {
        this.url = event.target.result;
      }
    }
  }
}
