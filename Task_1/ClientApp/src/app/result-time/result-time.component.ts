import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResultTime } from './ResultTime';

@Component({
  selector: 'app-result-time',
  templateUrl: './result-time.component.html',
  styleUrls: ['./result-time.component.css']
})
export class ResultTimeComponent implements OnInit {

  resultTime: ResultTime;

  
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      http.get<ResultTime>(baseUrl + 'data-provider/get-result-time').subscribe(
        result => {
          console.log(result);
          this.resultTime = result;
        },
        error => {
          console.log("Error" + error);
        });
    }


  ngOnInit() {
  }

}
