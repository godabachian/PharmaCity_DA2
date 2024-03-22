import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Medicine } from 'src/models/Medicine';
import { Purchase } from 'src/models/Purchase';

@Injectable({
  providedIn: 'root'
})
export class MedicinesService {

  uri = `${environment.url}/api/medicines`;
  uriBuy = `${environment.url}/api/medicines/buy`;
  constructor(private http: HttpClient) { }

  GetMedicines(): Observable<Medicine[]>{
    return this.http.get<Medicine[]>(this.uri);
  }

  GetMedicinesByFilter(nameMedicine:string|null, namePharmacy:string|null): Observable<Medicine[]>{
    const params = {'nameMedicine': `${nameMedicine}`, 'namePharmacy': `${namePharmacy}`};
    return this.http.get<Medicine[]>(this.uri, {params:params});
  }

  InsertMedicine(medicine: Medicine, token: string|null): Observable<Medicine>{
    const headers = new HttpHeaders().set('token', `${token}`); 
    return this.http.post<Medicine>(this.uri, medicine, {headers:headers});
  }

  BuyMedicines(purchase: Purchase): Observable<Purchase>{
    return this.http.post<Purchase>(this.uriBuy, purchase);
  }

  DeleteMedicine(medicineCode: string, token: string|null): Observable<void>{
    const headers = new HttpHeaders().set('token', `${token}`); 
    return this.http.delete<void>(`${this.uri}/${medicineCode}`, {headers:headers, responseType: 'text' as 'json'});
  }
}