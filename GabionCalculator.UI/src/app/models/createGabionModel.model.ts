import { Material } from 'src/app/models/material.model';

export interface CreateGabionModel {
  Height: number;
  Length: number;
  Width: number;
  CellHeight: number;
  CellWidth: number;
  MaterialDiameter: number;
  Quantity: number;
  MaterialId: number;
  UserName?: string;
  Materials: Material[];
}
