import { UpdateCostWorkModel } from 'src/app/models/updateCostWorkModel.model';

export interface ApiResultUpdateCostWorkModel {
  succeeded: boolean;
  result: UpdateCostWorkModel;
  errors: string[];
}
