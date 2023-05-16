import { RegisterUserModel } from 'src/app/models/registerUserModel.model';
import { ApiResultResponseUserModel } from 'src/app/models/apiResultResponseUserModel.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  public registerUser = (body: RegisterUserModel) => {
    return this.http.post<ApiResultResponseUserModel>(this.baseApiURL + '/api/User/Post', body);
  }
}
