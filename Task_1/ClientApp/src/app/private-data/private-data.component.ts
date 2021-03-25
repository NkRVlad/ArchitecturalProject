import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Product } from '../models/product';
import { User } from '../models/user';

@Component({
  selector: 'app-private-data',
  templateUrl: './private-data.component.html',
  styleUrls: ['./private-data.component.css']
})
export class PrivateDataComponent implements OnInit {

  public privateUser: Array<User>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Array<User>>(baseUrl + 'privatedata/get-user').subscribe(
      result => {
        this.privateUser = result;
      },
      error => {
        console.log("privatedata says" + error);
      });
  }

  ngOnInit() {
  }

}
