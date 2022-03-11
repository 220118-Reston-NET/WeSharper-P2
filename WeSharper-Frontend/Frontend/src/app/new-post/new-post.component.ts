import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faImage } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent implements OnInit {

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


// export class NewPostComponent {

//   fileName = '';

//   constructor(private http: HttpClient) { }

//   onFileSelected(event: { target: { files: File[]; }; }) {

//     const file: File = event.target.files[0];

//     if (file) {

//       this.fileName = file.name;

//       const formData = new FormData();

//       formData.append("thumbnail", file);

//       const upload$ = this.http.post("/api/thumbnail-upload", formData);

//       upload$.subscribe();
//     }
//   }
// }