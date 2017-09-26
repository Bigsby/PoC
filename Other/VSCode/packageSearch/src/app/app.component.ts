import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import 'rxjs/add/observable/of';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  packages: any;
  private searchTerms = new Subject<string>();


  constructor(private http: HttpClient) {
    this.searchTerms
      .debounceTime(300)
      .distinctUntilChanged()
      .switchMap(term => this.http.get(`http://api.jsdelivr.com/v1/cdnjs/libraries?name=*${term}*`))
      .subscribe(data => this.packages = data);
  }

  search(term: string): void {
    this.searchTerms.next(term);
  }
}