import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  hide = true;

  sefForm = new FormControl();
  displayedRoles: string[] = ['Role1', 'Role2', 'Role3', 'Role4'];//Заменить на ссылку в базу
  displayedDepts: string[] = ['Dept1', 'Dept2', 'Dept3', 'Dept4'];//Заменить на ссылку в базу

  displayedColumns = ['login', 'password', 'role', 'departament'];
  dataSource: User[];
  public elementSelect: User = { login: '', password: '', role: '', departament: '', departamentId: 0, id: 0, roleId: 0}; 
  constructor(public signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addReceive();
  }
  SelectRow(names: User) {
    this.elementSelect = names;
  }
}
