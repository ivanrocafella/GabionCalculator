import { Material } from 'src/app/models/material.model';

export interface ApiResultResponseListMaterial {
  succeeded: boolean;
  result: Material[];
  errors: string[];
}
