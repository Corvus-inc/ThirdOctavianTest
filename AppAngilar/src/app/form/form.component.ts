import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';
import { FormControl } from '@angular/forms';
import { release } from 'os';
import { element } from 'protractor';
import { ProcedureDB } from '../_interfaces/set-command';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  displayedRoles: string[] = ['Role1', 'Role2', 'Role3', 'Role4'];//Заменить на ссылку в базу
  displayedDepts: string[] = ['Dept1', 'Dept2', 'Dept3', 'Dept4'];//Заменить на ссылку в базу

  hide = true;
  isselected =  false;

  inputLogin: string;

  displayedColumns = ['login', 'password', 'role', 'departament'];
  dataSource: User[];
  public elementSelect: User = { login: 'DefUser', password: '123', role: '', departament: '', departamentId: 3, id: 0, roleId: 3 };
  constructor(public signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addReceive();
  }
  AddUser() {
    console.log('Adduser');
    this.signalRService.linkSet();
    this.signalRService.SetUserDB(this.elementSelect, ProcedureDB.UserInsert)
  
  }
  UpdateUser() {
    console.log('Updateuser');
  }
  DeleteUser() {
    console.log('Deleteuser');
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
