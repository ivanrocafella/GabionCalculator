import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  baseApiURL: string = environment.apiUrl;
  constructor(private http: HttpClient) { };

  public getClaims = () => {
    return this.http.get(this.baseApiURL + '/api/Material/Privacy');
  }
}
