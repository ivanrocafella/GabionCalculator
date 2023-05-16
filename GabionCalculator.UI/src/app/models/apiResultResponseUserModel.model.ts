import { ResponseUserModel } from 'src/app/models/responseUserModel.model';

export interface ApiResultResponseUserModel {
  succeeded: boolean;
  result: ResponseUserModel;
  errors: string[];
}
