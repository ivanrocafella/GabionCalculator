import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Material } from 'src/app/models/material.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MaterialsService {
  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };
  getAllMaterials(): Observable<Material[]> { return this.http.get<Material[]>(this.baseApiURL + '/api/Material/Materials') };
}


