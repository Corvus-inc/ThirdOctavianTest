import { Injectable, } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { User } from '../_interfaces/user';
import { ProcedureDB } from '../_interfaces/set-command';
import { Role } from '../_interfaces/role';
import { Dept } from '../_interfaces/dept';
import { UserDetails } from '../form/user-details';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  // Именование свойств в TypeScript начинаются со строчной буквы. Видимо, не просто так. Не запутаться....
  private hubConnection: signalR.HubConnection;
  dataUser: User[];
  dataRole: Role[];
  dataDept: Dept[];
  tableUsers: UserDetails[];
  displayedRoles: string[];
  displayedDepts: string[];

  public link: string;

  public startConnection = () => {

    console.log('Start Cnnection to SignalR');
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/MessageHub')
      .build();
    this.hubConnection
      .start()
      .then(() => { this.hubConnection.invoke('Send', "Message for stabile connection"); })
      .catch(err => console.log('Error while starting connection: ' + err));
  }
  public addArrayUsersListener = () => {
    this.hubConnection.on('addArrayUsers', (dataUser) => {
      this.dataUser = dataUser;
      this.UpdateUserDetails();
    });
  }
  public addArrayRolesListener = () => {
    this.hubConnection.on('addArrayRoles', (dataRole) => {
      this.dataRole = dataRole;
      this.UpdateUserDetails();
      this.GeneratedStringsRoles();
    });
  }
  public addArrayDeptsListener = () => {
    this.hubConnection.on('addArrayDepts', (dataDept) => {
      this.dataDept = dataDept; 
      this.UpdateUserDetails();
      this.GeneratedStringsDepts();
    });
  }
  public GetUsers = () => {
    this.hubConnection.invoke('GetUsers')
      .catch(err => console.error(err));
  }
  public GetRoles = () => {
    this.hubConnection.invoke('GetRoles')
      .catch(err => console.error(err));
  }
  public GetDepts = () => {
    this.hubConnection.invoke('GetDepts')
      .catch(err => console.error(err));
  }

  public linkSet = () => {
    this.hubConnection.on('linkMethod', (link) => {
      this.link = link;
      console.log(link);
    });
  }
  public SetUserDB = (userDetails: User, setcom: ProcedureDB) => {
    this.hubConnection.invoke('SetRequest', userDetails, setcom)
      .catch(err => console.error(err));
  }
  public addGetRequest = () => {
    this.hubConnection.invoke('GetRequest', 0)
      .catch(err => console.error(err));
  }

  UpdateUserDetails() {
    if (this.dataUser != null) {
      this.tableUsers = new Array(this.dataUser.length);
      for (var _i = 0; _i < this.dataUser.length; _i++) {
        var ud: UserDetails = new UserDetails();
        ud.id = this.dataUser[_i].id;
        ud.Login = this.dataUser[_i].loginPass.Key;
        ud.Pass = this.dataUser[_i].loginPass.Value;
        if (this.dataDept != null) {
          for (let d of this.dataDept) {
            if (d.id == this.dataUser[_i].departamentId) {
              ud.Dept = d.name;
              break;
            }
            else ud.Dept = 'NoDepartament';
          }
        }
        if (this.dataDept != null) {
          for (let r of this.dataRole) {
            if (r.id == this.dataUser[_i].roleId) {
              ud.Role = r.name;
              break;
            }
            else ud.Role = 'NoRole';
          }
        }
        this.tableUsers[_i] = ud;
      }
    }
  }
  GeneratedStringsDepts() {
    if (this.dataDept != null) {
      this.displayedDepts = new Array(this.dataDept.length);
      for (var _i = 0; _i < this.dataDept.length; _i++) {
        this.displayedDepts[_i] = this.dataDept[_i].name;
      }
    }
  }
  GeneratedStringsRoles() {
    if (this.dataRole != null) {
      this.displayedRoles = new Array(this.dataRole.length);
      for (var _i = 0; _i < this.dataRole.length; _i++) {
        this.displayedRoles[_i] = this.dataRole[_i].name;
      }
    }
  }
}
