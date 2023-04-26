import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResultResponseListMaterial } from 'src/app/models/apiResultResponseListMaterial.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
  
export class MaterialsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  getAllMaterials(): Observable<ApiResultResponseListMaterial> { return this.http.get<ApiResultResponseListMaterial>(this.baseApiURL + '/api/Material/Materials') };
}


