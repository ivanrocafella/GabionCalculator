import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResultResponseListMaterial } from 'src/app/models/apiResultResponseListMaterial.model';
import { ApiResultCreateMaterialModel } from 'src/app/models/apiResultCreateMatrialModel.model';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
  
export class MaterialsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient, private router: Router) { };

  getAllMaterials(): Observable<ApiResultResponseListMaterial> { return this.http.get<ApiResultResponseListMaterial>(this.baseApiURL + '/api/Material/Materials') };

  getCreateMaterialModel(): Observable<ApiResultCreateMaterialModel> { return this.http.get<ApiResultCreateMaterialModel>(this.baseApiURL + '/api/Material/Post') };

  submitForm(formData: any) {
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
}


