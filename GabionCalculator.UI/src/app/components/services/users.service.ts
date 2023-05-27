import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { ApiResultResponseListUser } from 'src/app/models/apiResultResponseListUserModel.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Subject, Observable } from 'rxjs';
import { LoginUserModel } from 'src/app/models/loginUserModel.model';
import { AuthUserModel } from 'src/app/models/authResponseModel.model';
import { JwtHelperService } from '@auth0/angular-jwt';

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

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public isUserAuthenticated = (): boolean | string | null => {
    console.log("working");
    const token = localStorage.getItem("token");
    console.log(token, !this.jwtHelper.isTokenExpired(token));
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  public isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token!);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role === 'admin';
  }

  public getAllUsers = () => { return this.http.get<ApiResultResponseListUser>(this.baseApiURL + '/api/User/Users') };
}
