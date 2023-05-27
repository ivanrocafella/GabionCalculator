import { User } from 'src/app/models/user.model';

export interface ApiResultResponseListUser {
  succeeded: boolean;
  result: User[];
  errors: string[];
}
