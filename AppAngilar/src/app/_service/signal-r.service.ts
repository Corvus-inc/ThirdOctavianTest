import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { User } from '../_interfaces/user';
import { GetCommandDB } from '../_interfaces/get-command';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public com: GetCommandDB;
  public us: User;
  public data: User[];
  public startConnection = () => {
    console.log('Work');
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/MessageHub')
      .build();
    this.com = 0;
    //this.us = { Login: 'Privet', Id: 24 };
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
}
