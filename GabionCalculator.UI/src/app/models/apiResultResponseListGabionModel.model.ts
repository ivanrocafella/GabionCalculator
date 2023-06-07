import { ResponseGabionModel } from 'src/app/models/responseGabionModel.model';

export interface ApiResultResponseListGabion {
  succeeded: boolean;
  result: ResponseGabionModel[];
  errors: string[];
}
