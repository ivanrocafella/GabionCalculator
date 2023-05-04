import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GabionsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  getCreateGabionModel(): Observable<ApiResultCreateGabionModel> { return this.http.get<ApiResultCreateGabionModel>(this.baseApiURL + '/api/Gabion') };
  submitForm(formData: any): Observable<ApiResultResponseGabionModel> { return this.http.post<ApiResultResponseGabionModel>(this.baseApiURL + '/api/Gabion/GetTemporaryGabion', formData) };
}
