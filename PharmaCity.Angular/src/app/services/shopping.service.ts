import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Petition } from 'src/models/Petition';
import { Purchase } from 'src/models/Purchase';
import { PurchaseDTO } from 'src/models/PurchaseDTO';

@Injectable({
  providedIn: 'root'
})
export class ShoppingService {

  uri = `${environment.url}/api/shopping`;
  uriAccept = `${environment.url}/api/shopping/accept`;
  uriDecline = `${environment.url}/api/shopping/decline`;
  uriPetitions = `${environment.url}/api/shopping/petitions`;
  constructor(private http: HttpClient) { }

  GetPetitions(token:string|null): Observable<Petition[]>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.get<Petition[]>(this.uriPetitions, {headers:headers});
  }

  GetShopping(token:string|null): Observable<PurchaseDTO[]>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.get<PurchaseDTO[]>(this.uri, {headers:headers});
  }

  GetShoppingState(code:string): Observable<void>{
    return this.http.get<void>(`${this.uri}/${code}`, {responseType: 'text' as 'json'});
  }

  AcceptRequest(id: number, token: string|null): Observable<void>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.patch<void>(`${this.uriAccept}/${id}`, id, {headers:headers, responseType: 'text' as 'json'});
  }
  DeclineRequest(id: number, token: string|null): Observable<void>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.patch<void>(`${this.uriDecline}/${id}`, id, {headers:headers, responseType: 'text' as 'json'});
  }
}
