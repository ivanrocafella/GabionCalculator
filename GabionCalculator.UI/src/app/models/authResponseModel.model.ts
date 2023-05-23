export interface AuthUserModel {
  IsAuthSuccessful: boolean;
  Errors?: string[];
  Token: string;
}
