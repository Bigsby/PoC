import { Component } from '@angular/core';
import { SimpleService } from "./simple.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    constructor(private simpleService: SimpleService) { }
    data = "Nothing yet yet";
    callService(): void {
        this.simpleService.get().then(serviceData => this.data = serviceData);
    }
}