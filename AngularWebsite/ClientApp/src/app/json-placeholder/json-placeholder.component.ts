import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { using } from 'rxjs';
import { getBaseUrl } from '../../main';
import { Variable } from '@angular/compiler/src/render3/r3_ast';


@Component({
  selector: 'app-json-placeholder',
  templateUrl: './json-placeholder.component.html'
})
export class DemoComponent {
  public requestData: Request<any>[];

  public filterId: number;
  public source: string = "posts";

  constructor(private http: HttpClient) {
    http.get<Request<any>[]>('/api/DemoAPI/GetUsers/?filter_id=' + this.filterId + '&source=' + this.source).subscribe(result => {
      this.requestData = result;
    }, error => console.error(error));
  }

  public updateData(userID, dataSource) {
    if(userID != null)
      this.filterId = userID;
    if (dataSource != null)
      this.source = dataSource;

    this.http.get<Request<any>[]>('/api/DemoAPI/GetUsers/?filter_id=' + this.filterId + '&source=' + this.source).subscribe(result => {
      this.requestData = result;
    }, error => console.error(error));
    
  }
  
}

interface Request<T> {
  name: string;
  value: T;
}
