import { Input, Component} from '@angular/core';
import { SignalRService } from '../_service/signal-r.service';
import { UserDetails } from './user-details';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent{
  @Input() service: SignalRService;
  hide = true;
  isselected = false;
  inputLogin: string;
  displayedColumns = ['login', 'password', 'role', 'departament'];
  elementSelect: UserDetails = { id: 0, Login: '', Pass: '', Dept: '', Role: '' }

  AddUser() {
    console.log('Adduser');
    
  }
  UpdateUser() {
    console.log('Updateuser');
    
  }
  DeleteUser() {
    console.log('Deleteuser');
    
  }
  SelectRow(names: UserDetails) {
    this.elementSelect = names;
    this.isselected = true;
  }
  InputStringSave(event: any) {
    const value = event.target.value;
    this.inputLogin = value;
  }
}
