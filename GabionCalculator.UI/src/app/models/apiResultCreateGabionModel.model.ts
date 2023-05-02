import { CreateGabionModel } from 'src/app/models/createGabionModel.model';

export interface ApiResultCreateGabionModel {
  succeeded: boolean;
  result: CreateGabionModel;
  errors: string[];
}
