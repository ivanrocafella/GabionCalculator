export interface ChangePasswordUserModel {
  Id: string;
  UserName: string;
  OldPassword: string;
  NewPassword: string;
  NewPasswordConfirm: string;
}
