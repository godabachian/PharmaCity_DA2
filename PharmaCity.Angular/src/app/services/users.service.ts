import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from 'src/models/User';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  uri = `${environment.url}/api/users`;
  constructor(private http: HttpClient) { }

  InsertUser(user:User, code:string): Observable<User>{
    const params = new HttpParams().set('code', code);
    return this.http.post<User>(this.uri, user, {params:params});
  }
}
