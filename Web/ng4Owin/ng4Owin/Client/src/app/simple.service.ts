import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/toPromise';

const apiBaseUrl = "http://localhost:60478/api/";

@Injectable()
export class SimpleService {

    constructor(private http: Http) { }

    get(): Promise<string> {
        return this.http.get(`${apiBaseUrl}Simple/Get`)
            .toPromise()
            .then(response => response.text());
    }
}
