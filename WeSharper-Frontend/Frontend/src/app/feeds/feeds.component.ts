import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-feeds',
  templateUrl: './feeds.component.html',
  styleUrls: ['./feeds.component.css']
})
export class FeedsComponent implements OnInit {
  feedPosts: any[];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.getFeeds().subscribe(result => {
      this.feedPosts = result;
    })
  }
}
