import { ResponseMaterialModel } from 'src/app/models/responseMaterialModel.model';

export interface ApiResultResponseListMaterial {
  succeeded: boolean;
  result: ResponseMaterialModel[];
  errors: string[];
}
