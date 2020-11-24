import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { User } from '../_interfaces/user';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  hide = true;

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
