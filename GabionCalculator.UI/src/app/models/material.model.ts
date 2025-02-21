import { Gabion } from "./gabion.model";

export interface Material {
  Id: number;
  Name: string;
  FullName: string;
  Size: number;
  PricePerKg: number;
  MaterialKindId: number;
  MaterialKind: number;
  DateStart: string;
  DateUpdate: string;
  Gabions?: Gabion[];
}
