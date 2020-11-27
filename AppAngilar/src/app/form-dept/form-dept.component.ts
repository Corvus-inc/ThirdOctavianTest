import { Component, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { ProcedureDB } from '../_interfaces/set-command';
import { Dept } from '../_interfaces/dept';

@Component({
  selector: 'app-form-dept',
  templateUrl: './form-dept.component.html',
  styleUrls: ['./form-dept.component.css']
})
export class FormDeptComponent {
  isselected: boolean= false;
  @Input() service: SignalRService;

  elementSelect: string ='';
  SelectDept(value: any) {
    this.elementSelect = value; 
    this.isselected = true;
  }
  setcom: ProcedureDB;
  selectDept: Dept = { id: 0, name: '' };
 Add() {
    this.setcom = ProcedureDB.DeptInsert;
    console.log(this.selectDept);
    this.service.SetDept(this.selectDept, this.setcom);
  }
  Update() {
    this.IdSelectDept();
    this.setcom = ProcedureDB.DeptUpdate;
    console.log(this.selectDept);
    this.service.SetDept(this.selectDept, this.setcom);
  }
  Delete() {
    this.IdSelectDept();
    this.selectDept.name = this.elementSelect;
    this.setcom = ProcedureDB.DeptDelete;

    this.service.SetDept(this.selectDept, this.setcom);
  }
  IdSelectDept() {
    for (var _i = 0; _i < this.service.dataDept.length; _i++) {
      if (this.service.dataDept[_i].name == this.elementSelect) {
        this.selectDept.id = this.service.dataDept[_i].id;
      }
    }
  }
  InputStringSave(event: any) {
    console.log(event.type);

    if (event.type == 'input') {
      this.selectDept.name = event.target.value;
      console.log(this.selectDept.name);
    }
  }
}
