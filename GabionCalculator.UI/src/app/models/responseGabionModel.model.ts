import { Material } from 'src/app/models/material.model';
import { User } from 'src/app/models/user.model';

export interface ResponseGabionModel {
  Id: number;
  Height: number;
  Length: number;
  Width: number;
  CellHeight: number;
  CellWidth: number;
  OutletVert: number;
  OutletHoriz: number;
  BendRadius: number;
  Kfactor: number;
  MaterialDiameter: number;
  BarBilletVert: number;
  BarBilletHoriz: number;
  CardWidth: number;
  CardHeight: number;
  Weight: number;
  WeightCard: number; // кг
  BatchWeight: number;
  Quantity: number;
  BarsQtyVert: number;
  BarsQtyHoriz: number;
  MaterialTotalLength: number; // м
  Svg?: string;
  MaterialJson?: string; // json
  UserJson?: string; // json
  Sebes: number;
  BatchSebes: number;
  Price: number;
  BatchPrice: number;
  PriceMaterial: number;
  PriceMaterialBatch: number;
  DateStart: string;
  DateUpdate: string;
  MaterialId?: number;
  Material?: Material;
  UserId?: string;
  User?: User;
}
