import { Component, OnInit, Input} from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';

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
  
  constructor() { }

  ngOnInit(): void {

  }
  OpenMain() {
    this.onmain = true;
    this.onuser = false;
    this.onrole = false;
    this.ondept = false;
  }
  OpenUsers() {
    this.signalRService.GetUsers();
    this.signalRService.GetRoles();
    this.signalRService.GetDepts();

    this.onuser = true;
    this.onmain = false;
  }
  OpenRoles() {
    this.signalRService.GetRoles();
    this.onrole = true;
    this.onmain = false;
  }
  OpenDepts() {
    this.signalRService.GetDepts();
    this.ondept = true;
    this.onmain = false;

  }
}
