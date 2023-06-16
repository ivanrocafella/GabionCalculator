import { ChangePasswordUserModel } from 'src/app/models/changePasswordUserModel';

export interface ApiResultChangePasswordUserModel {
  succeeded: boolean;
  result: ChangePasswordUserModel;
  errors: string[];
}
