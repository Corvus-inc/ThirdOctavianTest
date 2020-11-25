import { Input, Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';
import { UserDetails } from './user-details';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  hide = true;

  isselected = true;
  inputLogin: string;

  displayedColumns = ['login', 'password', 'role', 'departament'];

  @Input() service: SignalRService;
  public tableUsers: UserDetails[];
  displayedRoles: string[] = ['Role0','Role1', 'Role2', 'Role3', 'Role4'];//Заменить на ссылку в базу
  displayedDepts: string[] = ['Dept0','Dept1', 'Dept2', 'Dept3', 'Dept4'];//Заменить на ссылку в базу

  elementSelect: UserDetails = { id: 0, Login: '', Pass: '', Dept: '', Role: '' }
  constructor() { }
  

  ngOnInit(): void {
  }

  UpdateUserDetails() {
    this.tableUsers = new Array(this.service.dataUser.length);
    for (var _i = 0; _i < this.service.dataUser.length; _i++) {
      var ud: UserDetails = new UserDetails();
      console.log(this.service.dataDept);
      ud.id = this.service.dataUser[_i].id;
      ud.Login = this.service.dataUser[_i].loginPass.Key;
      ud.Pass = this.service.dataUser[_i].loginPass.Value;
      for (let d of this.service.dataDept) {
        if (d.id == this.service.dataUser[_i].departamentId){
          ud.Dept = d.name;
          break;
        }
        else ud.Dept = 'NoDepartament';
      }
      for (let r of this.service.dataRole) {
        if (r.id == this.service.dataUser[_i].roleId) {
          ud.Role = r.name;
          break;
        }
        else ud.Role = 'NoRole';
      }
      this.tableUsers[_i] = ud;
      console.log(this.tableUsers);
    }
    this.service.GetUsers();
    this.service.GetRoles();
    this.service.GetDepts();

  }

  AddUser() {

    console.log('Adduser'); console.log(this.service.dataUser);
    this.UpdateUserDetails();
  }
  UpdateUser() {
    console.log('Updateuser');
    //this.UpdateUserDetails();
  }
  DeleteUser() {
    console.log('Deleteuser');
    //this.UpdateUserDetails();
  }

  SelectRow(names: UserDetails) {
    this.elementSelect= names;
    this.isselected = true;
  }
  InputStringSave(event: any) {
    const value = event.target.value;
    this.inputLogin = value;
  }
}
