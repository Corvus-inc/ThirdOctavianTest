import { Component, OnInit, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';

@Component({
  selector: 'app-form-roles',
  templateUrl: './form-roles.component.html',
  styleUrls: ['./form-roles.component.css']
})
export class FormRolesComponent implements OnInit {
  isselected: boolean = false;
  @Input() service: SignalRService;

  elementSelect: string;
  SelectRole(value: any) {
    this.elementSelect = value;
    this.isselected = true;
  }
  inputRole: string;
  InputStringSave(event: any) {
    const value = event.target.value;
    this.inputRole = value;
  }
  ngOnInit(): void {
   
    
  }
  Add() {

  }
  Update() {

  }
  Delete() {

  }

}

