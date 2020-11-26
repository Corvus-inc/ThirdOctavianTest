import { Component, OnInit} from '@angular/core';
import { SignalRService } from './_service/signal-r.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
 constructor(public signalRService: SignalRService) { }
  title = 'AppAngular';
  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addArrayUsersListener();
    this.signalRService.addArrayDeptsListener();
    this.signalRService.addArrayRolesListener();
  }
}
