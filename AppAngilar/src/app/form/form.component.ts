import { Input, Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';
import { FormControl } from '@angular/forms';
import { release } from 'os';
import { element } from 'protractor';
import { ProcedureDB } from '../_interfaces/set-command';
import { Dept } from '../_interfaces/dept';
import { Role } from '../_interfaces/role';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  displayedRoles: string[] = ['Role1', 'Role2', 'Role3', 'Role4'];//Заменить на ссылку в базу
  displayedDepts: string[] = ['Dept1', 'Dept2', 'Dept3', 'Dept4'];//Заменить на ссылку в базу

  hide = true;

  isselected =  true;
  inputLogin: string;

  displayedColumns = ['login', 'password', 'role', 'departament'];

  @Input() userSource: User[];
  @Input() deptSource: Dept[];
  @Input() roleSource: Role[];
  @Input() service: SignalRService;

  public elementSelect: User = {loginpass: { key: 'Default', value: 'DaefaultPass' } , departamentid: 3, id: 0, roleid: 3
  };
  constructor() { }

  ngOnInit(): void {
  }
  AddUser() {
    console.log('Adduser'); console.log(this.service.dataUser);
    //this.service.linkSet();
    //this.service.SetUserDB(this.elementSelect, ProcedureDB.UserInsert)
    //this.service.addGetRequest();
  
  }
  UpdateUser() {
    console.log('Updateuser');
    //this.signalRService.addGetRequest();
  }
  DeleteUser() {
    console.log('Deleteuser');
    //this.signalRService.addReceive();
  }

  SelectRow(names: User) {
    this.elementSelect = names;
    this.isselected = true;
  }
  InputStringSave(event: any) {
    const value = event.target.value;
    this.inputLogin = value;
  }
  
}
