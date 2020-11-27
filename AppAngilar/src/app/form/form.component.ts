import { Input, Component } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { UserDetails } from './user-details';
import { ProcedureDB } from '../_interfaces/set-command';
import { User } from '../_interfaces/user';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent {
  @Input() service: SignalRService;
  hide = true;
  isselected = false;
  displayedColumns = ['login', 'password', 'role', 'departament'];
  elementSelect: UserDetails = { id: 0, Login: '', Pass: '', Dept: '', Role: '' };
  inputUserDetails: UserDetails = { id: 0, Login: '', Pass: '', Dept: '', Role: '' };

  uDetails: User = { id: 0, loginPass: { Key: '', Value: '' }, departamentId: 0, roleId: 0 };
  setCommand: ProcedureDB;



  AddUser() {
    console.log('Adduser');
    this.UserDetailsConvertToUser(this.inputUserDetails)
    this.setCommand = ProcedureDB.UserInsert;
    this.service.SetUser(this.uDetails, this.setCommand);
  }
  UpdateUser() {
    console.log('Updateuser');
    this.UserDetailsConvertToUser(this.inputUserDetails)
    this.setCommand = ProcedureDB.UserUpdate;
    this.service.SetUser(this.uDetails, this.setCommand)
  }
  DeleteUser() {
    console.log('Deleteuser');
    this.UserDetailsConvertToUser(this.elementSelect)
    this.setCommand = ProcedureDB.UserDelete;
    this.service.SetUser(this.uDetails, this.setCommand)
  }

  SelectRow(names: UserDetails) {
    this.elementSelect = names;
    var copy = Object.assign({}, names)
    this.inputUserDetails = copy;
    this.isselected = true;
  }
  InputStringSave(event: any) {
    if (event.type == 'input') {
      if (event.target.id == "mat-input-0") {
        this.inputUserDetails.Login = event.target.value;
      }
      if (event.target.id == "mat-input-1") {
        this.inputUserDetails.Pass = event.target.value;
      }
    }
    console.log(event);

    if (!event) {
      this.inputUserDetails.Role = this.elementSelect.Role;
    }
    if (!event) {
      this.inputUserDetails.Dept = this.elementSelect.Dept;
    }
  }
  UserDetailsConvertToUser(ud: UserDetails) {
    this.uDetails.id = ud.id;
    this.uDetails.loginPass.Key = ud.Login
    this.uDetails.loginPass.Value = ud.Pass
    for (var _i = 0; _i < this.service.dataRole.length; _i++) {
      if (this.service.dataRole[_i].name == ud.Role)
        this.uDetails.roleId = this.service.dataRole[_i].id;
    }
    for (var _i = 0; _i < this.service.dataDept.length; _i++) {
      if (this.service.dataDept[_i].name == ud.Dept)
        this.uDetails.departamentId = this.service.dataDept[_i].id;
    }
    console.log(this.uDetails);
  }
}
