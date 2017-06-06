import { Component } from '@angular/core';

let name = "main";

@Component({
  selector: 'app-root',
  templateUrl: "../templates/" + name + ".component.html",
  styleUrls: [
    "../styles/" + name + ".component.less"
  ]
})

export class MainComponent {
  title = 'app';
}