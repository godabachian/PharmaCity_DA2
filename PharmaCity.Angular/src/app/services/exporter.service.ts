import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExporterService {

  uriExport = `${environment.url}/api/export`;
  uriExporters = `${environment.url}/api/exporters`;
  constructor(private http: HttpClient) { }

  ExportMedicines(mechanismName: string, token: string|null){
    const headers = new HttpHeaders().set('token', `${token}`);
    const params = new HttpParams().set('mechanismName', mechanismName);
    return this.http.get(this.uriExport, {headers:headers,params:params});
  }

  GetExporters(token: string|null): Observable<string[]>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.get<string[]>(this.uriExporters, {headers:headers});
  }
}
