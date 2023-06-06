import { Gabion } from 'src/app/models/gabion.model';

export interface ApiResultGabionModel {
  succeeded: boolean;
  result: Gabion;
  errors: string[];
}
