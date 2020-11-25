import { KeyValue } from './key-value';

export interface User {
  id: number;
  loginPass: KeyValue<string, string>;
  roleId: number;
  departamentId: number;
}
