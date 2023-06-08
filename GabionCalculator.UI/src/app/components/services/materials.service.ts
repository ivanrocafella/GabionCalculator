import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResultResponseListMaterial } from 'src/app/models/apiResultResponseListMaterial.model';
import { ApiResultCreateMaterialModel } from 'src/app/models/apiResultCreateMatrialModel.model';
import { ApiResultUpdateMaterialModel } from 'src/app/models/apiResultUpdateMaterialModel.model';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ApiResultResponseMaterialModel } from '../../models/apiResultResponseMaterialModel.model';

@Injectable({
  providedIn: 'root'
})
  
export class MaterialsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient, private router: Router) { };

  getAllMaterials(): Observable<ApiResultResponseListMaterial> { return this.http.get<ApiResultResponseListMaterial>(this.baseApiURL + '/api/Material/Materials') };
  getCreateMaterialModel(): Observable<ApiResultCreateMaterialModel> { return this.http.get<ApiResultCreateMaterialModel>(this.baseApiURL + '/api/Material/Post') };
  getUpdateMaterialModel(id: number): Observable<ApiResultUpdateMaterialModel> { return this.http.get<ApiResultUpdateMaterialModel>(this.baseApiURL + '/api/Material/Update/' + id +'') };
  submitFormPost(formData: any) {
    this.http.post(this.baseApiURL + '/api/Material/Post', formData).subscribe(
      (response) => {
        console.log('Form submitted', response);
        this.router.navigate(["/Material/Materials"]);
      },
      (error) => {
        console.error('Form has not been submitted', error);
      }
    );
  }
  submitFormPut(id: number, formData: any) {
    this.http.put(this.baseApiURL + '/api/Material/Update/' + id + '', formData).subscribe(
      (response) => {
        console.log('Form submitted', response);
        this.router.navigate(["/Material/Materials"]);
      },
      (error) => {
        console.error('Form has not been submitted', error);
      }
    );
  }
  public deleteMaterial = (id: number) => {
    return this.http.post<ApiResultResponseMaterialModel>(this.baseApiURL + '/api/Material/Remove/' + id + '', null);
  }
}


