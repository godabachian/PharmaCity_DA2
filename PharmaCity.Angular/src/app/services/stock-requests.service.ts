import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { StockRequest } from 'src/models/StockRequest';

@Injectable({
  providedIn: 'root'
})
export class StockRequestsService {

  uri = `${environment.url}/api/stockRequest`;
  uriAccept = `${environment.url}/api/stockrequest/accept`;
  uriDecline = `${environment.url}/api/stockrequest/decline`;
  constructor(private http: HttpClient) { }

  GetStockRequests(token:string|null): Observable<StockRequest[]>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.get<StockRequest[]>(this.uri, {headers:headers});
  }

  InsertStockRequest(stockRequest: StockRequest, token: string|null): Observable<StockRequest>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.post<StockRequest>(this.uri, stockRequest, {headers:headers});
  }

  AcceptStockRequest(id: number, token: string|null): Observable<void>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.patch<void>(`${this.uriAccept}/${id}`, id, {headers:headers, responseType: 'text' as 'json'});
  }

  DeclineStockRequest(id: number, token: string|null): Observable<void>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.patch<void>(`${this.uriDecline}/${id}`, id, {headers:headers, responseType: 'text' as 'json'});
  }
}
