import { Component, OnInit, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';

@Component({
  selector: 'app-form-dept',
  templateUrl: './form-dept.component.html',
  styleUrls: ['./form-dept.component.css']
})
export class FormDeptComponent {
  isselected: boolean= false;
  @Input() service: SignalRService;

  elementSelect: string;
  SelectDept(value: any) {
    this.elementSelect = value;
    this.isselected = true;
  }

  inputDept: string;
  InputStringSave(event: any) {
    const value = event.target.value;
    this.inputDept = value;
  }
  Add() {

  }
  Update() {

  }
  Delete() {

  }

}
