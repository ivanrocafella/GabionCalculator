import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GabionsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  getCreateGabionModel(): Observable<ApiResultCreateGabionModel> { return this.http.get<ApiResultCreateGabionModel>(this.baseApiURL + '/api/Gabion') };
  submitForm(formData: any) {
    this.http.post(this.baseApiURL + '/api/Gabion/GetTemporaryGabion', formData).subscribe(
      (response) => {
        console.log('РЈСЃРїРµС€РЅРѕ РѕС‚РїСЂР°РІР»РµРЅРѕ', response);
        // Р”РѕРїРѕР»РЅРёС‚РµР»СЊРЅР°СЏ Р»РѕРіРёРєР° РїСЂРё СѓСЃРїРµС€РЅРѕР№ РѕС‚РїСЂР°РІРєРµ
      },
      (error) => {
        console.error('РћС€РёР±РєР° РѕС‚РїСЂР°РІРєРё', error);  
        // Р”РѕРїРѕР»РЅРёС‚РµР»СЊРЅР°СЏ Р»РѕРіРёРєР° РїСЂРё РѕС€РёР±РєРµ РѕС‚РїСЂР°РІРєРё
      }
    );
  }
}
