import { Component, OnInit, Input } from '@angular/core';
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
  @Input() signalRService: SignalRService;
  dataSource: User[];
  constructor() { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addArrayUsersListener();
    this.signalRService.addArrayDeptsListener();
    this.signalRService.addArrayRolesListener();
  }
  OpenUsers() {
    this.onuser = true;
    this.onmain = false;
    this.signalRService.GetUsers();
    this.signalRService.GetRoles();
    this.signalRService.GetDepts();
    
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
