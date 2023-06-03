import { ResponseMaterialModel } from 'src/app/models/responseMaterialModel.model';

export interface ApiResultResponseMaterialModel {
  succeeded: boolean;
  result: ResponseMaterialModel;
  errors: string[];
}
