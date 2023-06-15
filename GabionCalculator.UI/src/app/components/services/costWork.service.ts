import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment'
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ApiResultUpdateCostWorkModel } from 'src/app/models/apiResultUpdateCostWorklModel.model'
import { ApiResultResponseCostWorkModel } from 'src/app/models/apiResultResponseCostWorklModel.model'

@Injectable({
  providedIn: 'root'
})

export class CostWorkService {
  private baseApiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient, private router: Router) { }
  getUpdateCostWorkModel(id: number): Observable<ApiResultUpdateCostWorkModel> { return this.http.get<ApiResultUpdateCostWorkModel>(this.baseApiUrl + '/api/CostWork/Update/' + id + '') };
  submitFormPut(id: number, formData: any): Observable<ApiResultResponseCostWorkModel> { return this.http.put<ApiResultResponseCostWorkModel>(this.baseApiUrl + '/api/CostWork/Update/' + id + '', formData) };
}
