import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { User } from '../_interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public us: User;
  public data: string
  public startConnection = () => {
    console.log('Work');
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/MessageHub')
      .build();
    this.us = { Login: 'Privet', Id: 24 };
    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('Send', this.us);
      });
  }
  public addReceive = () => {
    this.hubConnection.on('Receive', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
