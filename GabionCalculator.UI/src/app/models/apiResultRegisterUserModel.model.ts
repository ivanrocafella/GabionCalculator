import { RegisterUserModel } from 'src/app/models/registerUserModel.model';

export interface ApiResultRegisterUserModel {
  succeeded: boolean;
  result: RegisterUserModel;
  errors: string[];
}
