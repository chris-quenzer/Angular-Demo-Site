import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { using } from 'rxjs';
import { getBaseUrl } from '../../main';
import { Variable } from '@angular/compiler/src/render3/r3_ast';


@Component({
  selector: 'app-iss-tracker',
  templateUrl: './iss-tracker.component.html'
})
export class IssTrackerComponent {

  public issLocation: IssLocation;
  public issPassTimes: IssPassTimes;
  public peopleInSpace: PeopleInSpace;

  // Default Portland, OR
  public myLat: number = 45.5051;
  public myLng: number = 122.6750;

  constructor(private http: HttpClient) {
    // ISS Location
    http.get<IssLocation>('/api/IssTracker/GetIssLocation/').subscribe(result => {
      this.issLocation = result;
    }, error => console.error(error));

    // ISS Pass Times
    http.get<IssPassTimes>('/api/IssTracker/GetIssPassTimes?myLat=' + this.myLat + '&myLng=' + this.myLng).subscribe(result => {
      this.issPassTimes = result;
    }, error => console.error(error));

    // People In Space
    http.get<PeopleInSpace>('/api/IssTracker/GetPeopleInSpace').subscribe(result => {
      this.peopleInSpace = result;
    }, error => console.error(error));

    // Update ISS Location every 5 sec
    setInterval(() => { this.updateIssLocation() }, 5000);
  }

  public updateIssLocation() {
    this.http.get<IssLocation>('/api/IssTracker/GetIssLocation/').subscribe(result => {
      this.issLocation = result;
    }, error => console.error(error));
  }

  public updateIssPassTimes(lat, lng) {
    if (lat != null)
      this.myLat = lat;
    if (lng != null)
      this.myLng = lng;

    this.http.get<IssPassTimes>('/api/IssTracker/GetIssPassTimes?myLat=' + this.myLat + '&myLng=' + this.myLng).subscribe(result => {
      this.issPassTimes = result;
    }, error => console.error(error));
  }

  public updatePeopleInSpace() {
    this.http.get<PeopleInSpace>('/api/IssTracker/GetPeopleInSpace').subscribe(result => {
      this.peopleInSpace = result;
    }, error => console.error(error));
  }
  
}

interface IssLocation {
  message: string;
  timestamp: string;
  iss_position: {
    latitude: number;
    longitude: number;
  };
}

interface IssPassTimes {
  message: string;
  request: {
    latitude: number;
    longitude: number;
    altitude: number;
    passes: number;
    datetime: string;
  }
  response: {
    risetime: string;
    duration: number;
  }[]
}

interface PeopleInSpace {
  message: string;
  number: number;
  people: {
    name: string;
    craft: string;
  }[]
}
