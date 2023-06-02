import { UpdateMaterialModel } from 'src/app/models/updateMaterialModel.model';

export interface ApiResultUpdateMaterialModel {
  succeeded: boolean;
  result: UpdateMaterialModel;
  errors: string[];
}
