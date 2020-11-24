import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-form-main',
  templateUrl: './form-main.component.html',
  styleUrls: ['./form-main.component.css']
})
export class FormMainComponent implements OnInit {
  onmain = true;
  onuser = false;
  onrole = false;
  ondept = false;
  constructor() { }

  ngOnInit(): void {
  }
  OpenUsers() {
    this.onuser = true;
    this.onmain = false;
  }
  OpenRoles() {
    this.onrole = true;
    this.onmain = false;

  }
  OpenDepts() {
    this.ondept = true;
    this.onmain = false;
  }
  OpenMain() {
    this.onmain = true;
    this.onuser = false;
    this.onrole = false;
    this.ondept = false;
  }
}
