import { Component, OnInit, Input } from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';

@Component({
  selector: 'app-form-dept',
  templateUrl: './form-dept.component.html',
  styleUrls: ['./form-dept.component.css']
})
export class FormDeptComponent implements OnInit {

  @Input() service: SignalRService;

  ngOnInit(): void {
  }

}
