import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { User } from '../_interfaces/user';
import { GetCommandDB } from '../_interfaces/get-command';
import { ProcedureDB } from '../_interfaces/set-command';
import { Role } from '../_interfaces/role';
import { Dept } from '../_interfaces/dept';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  // Именование свойств в TypeScript начинаются со строчной буквы. Видимо, не просто так. Не запутаться....
  private hubConnection: signalR.HubConnection;
  public link: string;
  public us: User;
  public dataUser: User[];
  public dataRole: Role[];
  public dataDept: Dept[];
  public startConnection = () => {
    console.log('Work');
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
      console.log(this.dataUser);
    });

  }
  public addArrayRolesListener = () => {
    this.hubConnection.on('addArrayRoles', (dataRole) => {
      this.dataRole = dataRole;});
  }
  public addArrayDeptsListener = () => {
    this.hubConnection.on('addArrayDepts', (dataDept) => {
      this.dataDept = dataDept;
      console.log(this.dataDept)
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

  public SetUserDB = (userDetails: User, setcom: ProcedureDB ) => {
    this.hubConnection.invoke('SetRequest', userDetails, setcom)
      .catch(err => console.error(err));
  }
  public addGetRequest = () => {
    this.hubConnection.invoke('GetRequest', 0)
      .catch(err => console.error(err));
  }
}
//.then(() => { this.hubConnection.invoke('Send', "Message for stabile connection"); })
//  .catch(err => console.log('Error while starting connection: ' + err));
//this.hubConnection.on('Receive', (data) => { console.log(data); });
//  }
//  public addGetRequest = () => {
//  this.hubConnection.invoke('GetRequest', this.com)
//    .catch(err => console.error(err));
