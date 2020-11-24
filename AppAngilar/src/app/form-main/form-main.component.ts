import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';

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

  dataSource: User[];
  constructor(public signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addReceive();
  }
  OpenUsers() {
    this.onuser = true;
    this.onmain = false;
    this.signalRService.addGetRequest();
    this.dataSource = this.signalRService.data;
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
