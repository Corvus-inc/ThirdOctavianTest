import { Component, OnInit, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';

@Component({
  selector: 'app-form-roles',
  templateUrl: './form-roles.component.html',
  styleUrls: ['./form-roles.component.css']
})
export class FormRolesComponent implements OnInit {

  @Input() service: SignalRService;

  ngOnInit(): void {
   
    
  }
  

}

