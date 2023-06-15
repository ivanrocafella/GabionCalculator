import { ResponseCostWorkModel } from 'src/app/models/responseCostWork.model';

export interface ApiResultResponseCostWorkModel {
  succeeded: boolean;
  result: ResponseCostWorkModel;
  errors: string[];
}
