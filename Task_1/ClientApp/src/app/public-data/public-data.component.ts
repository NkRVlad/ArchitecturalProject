import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Product } from '../models/product';

@Component({
  selector: 'app-public-data',
  templateUrl: './public-data.component.html',
  styleUrls: ['./public-data.component.css']
})
export class PublicDataComponent implements OnInit {

  public privateProduct: Array<Product>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Array<Product>>(baseUrl + 'publicdata/get-product').subscribe(
      result => {
        this.privateProduct = result;
      },
      error => {
        console.log("publicdata says" + error);
      });
  }
  ngOnInit() {
  }

}
