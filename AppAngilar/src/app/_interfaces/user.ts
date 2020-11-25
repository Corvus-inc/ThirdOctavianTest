import { KeyValue } from '@angular/common';

export interface User {
  id: number;
  loginpass: KeyValue<string, string>;
  roleid: number;
  departamentid: number;
}
