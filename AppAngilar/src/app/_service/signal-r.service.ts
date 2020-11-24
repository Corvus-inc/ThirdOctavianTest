import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { User } from '../_interfaces/user';
import { GetCommandDB } from '../_interfaces/get-command';
import { ProcedureDB } from '../_interfaces/set-command';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public link: string;
  public com: GetCommandDB;
  public us: User;
  public data: User[];
  public startConnection = () => {
    console.log('Work');
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/MessageHub')
      .build();
    this.com = GetCommandDB.GetAllUser;
    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('GetRequest', this.com);
      });

  }
  public addReceive = () => {
    this.hubConnection.on('SetArray', (data) => {
      this.data = data;
      console.log(data);
    });
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

}
