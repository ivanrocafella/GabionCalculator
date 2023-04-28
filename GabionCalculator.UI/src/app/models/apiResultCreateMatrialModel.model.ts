import { CreateMaterialModel } from 'src/app/models/createMaterialModel.model';

export interface ApiResultCreateMaterialModel {
  succeeded: boolean;
  result: CreateMaterialModel;
  errors: string[];
}
