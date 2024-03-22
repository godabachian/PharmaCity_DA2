import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pharmacy } from 'src/models/Pharmacy';

@Injectable({
  providedIn: 'root'
})
export class PharmacyService {

  uri = `${environment.url}/api/pharmacy`;
  constructor(private http: HttpClient) { }

  InsertPharmacy(pharmacy: Pharmacy, token: string|null): Observable<Pharmacy>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.post<Pharmacy>(this.uri, pharmacy, {headers: headers});
  }

  GetPharmacies(): Observable<Pharmacy[]>{
    return this.http.get<Pharmacy[]>(this.uri);
  }
}
