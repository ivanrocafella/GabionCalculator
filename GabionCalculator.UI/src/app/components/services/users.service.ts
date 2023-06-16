import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { ApiResultResponseListUser } from 'src/app/models/apiResultResponseListUserModel.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Subject, Observable } from 'rxjs';
import { LoginUserModel } from 'src/app/models/loginUserModel.model';
import { AuthUserModel } from 'src/app/models/authResponseModel.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ApiResultChangePasswordUserModel } from '../../models/apiResultChangePasswordUserModel';
import { ChangePasswordUserModel } from '../../models/changePasswordUserModel';
import { User } from '../../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  private isAdminChangeSub = new Subject<boolean>()
  public isAdminChanged = this.isAdminChangeSub.asObservable();
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { };

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public sendIsAdminStateChangeNotification = (isAdmin: boolean) => {
    this.isAdminChangeSub.next(isAdmin);
  }

  public registerUser = (body: RegisterUserModel) => {
    return this.http.post<ApiResultResponseUserModel>(this.baseApiURL + '/api/User/Register', body);
  }

  public loginUser = (body: LoginUserModel) => {
    return this.http.post<AuthUserModel>(this.baseApiURL + '/api/User/Login', body);
  }

  public deleteUser = (id: string) => {
    return this.http.post<ApiResultResponseUserModel>(this.baseApiURL + '/api/User/Remove/' + id +'', null);
  }

  public getUser = () => {
    return this.http.get<ApiResultResponseUserModel>(this.baseApiURL + '/api/User/GetUser');
  }

  public changePasswordUser = (id: string, body: ChangePasswordUserModel) => {
    return this.http.put<User>(this.baseApiURL + '/api/User/ChangePassword/' + id + '', body);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
    this.sendIsAdminStateChangeNotification(false);
  }

  public isUserAuthenticated = (): boolean | string | null => {
    const token = localStorage.getItem("token");
    var isTokenExpired = true;
    if (token !== null) {
      isTokenExpired = !this.jwtHelper.isTokenExpired(token);
    }
    return token && isTokenExpired;
  }

  public isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    var decodedToken = null;
    var role = '';
    if (token !== null) {
      decodedToken = this.jwtHelper.decodeToken(token!);
      role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    }
    return role === 'admin';
  }

  public getAllUsers = () => { return this.http.get<ApiResultResponseListUser>(this.baseApiURL + '/api/User/Users') };
}
