import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { LoginUserModel } from 'src/app/models/loginUserModel.model';
import { AuthUserModel } from 'src/app/models/authResponseModel.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  public registerUser = (body: RegisterUserModel) => {
    return this.http.post<ApiResultResponseUserModel>(this.baseApiURL + '/api/User/Register', body);
  }
  public loginUser = (body: LoginUserModel) => {
    return this.http.post<AuthUserModel>(this.baseApiURL + '/api/User/Login', body);
  }
}
