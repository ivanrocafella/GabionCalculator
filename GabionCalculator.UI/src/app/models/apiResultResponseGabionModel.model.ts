import { ResponseGabionModel } from 'src/app/models/responseGabionModel.model';

export interface ApiResultResponseGabionModel {
  succeeded: boolean;
  result: ResponseGabionModel;
  errors: string[];
}
