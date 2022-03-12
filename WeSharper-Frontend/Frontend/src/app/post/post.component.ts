import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})

export class PostComponent implements OnInit {
  post:Post;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.getPost().subscribe(result => {
      this.post = result;
  })

}
}
