import { Component, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { Role } from '../_interfaces/role';
import { ProcedureDB } from '../_interfaces/set-command';

@Component({
  selector: 'app-form-roles',
  templateUrl: './form-roles.component.html',
  styleUrls: ['./form-roles.component.css']
})
export class FormRolesComponent {
  isselected: boolean = false;
  @Input() service: SignalRService;

  elementSelect: string = '';
  SelectRole(value: any) {
    this.elementSelect = value;
    this.isselected = true;
  }
  setcom: ProcedureDB;
  selectRole: Role = { id: 0, name: '' };
  Add() {
    this.setcom = ProcedureDB.RoleInsert;
    this.service.SetRole(this.selectRole, this.setcom);
    this.service.GetRoles();
  }
  Update() {
    this.IdSelectRole();
    this.setcom = ProcedureDB.RoleUpdate;
    this.service.SetRole(this.selectRole, this.setcom);
    this.service.GetRoles();
  }
  Delete() {
    this.IdSelectRole();
    this.selectRole.name = this.elementSelect;
    this.setcom = ProcedureDB.RoleDelete;
    this.service.SetRole(this.selectRole, this.setcom);
    this.service.GetRoles();
  }
  IdSelectRole() {
    for (var _i = 0; _i < this.service.dataRole.length; _i++) {
      if (this.service.dataRole[_i].name == this.elementSelect) {
        this.selectRole.id = this.service.dataRole[_i].id;
      }
    }
  }
  InputStringSave(event: any) {
    if (event.type == 'input') {
      this.selectRole.name = event.target.value;
      console.log(this.selectRole.name);
    }
  }
}



