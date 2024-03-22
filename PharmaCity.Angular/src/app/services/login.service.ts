import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from 'src/models/Login';
import { LoginDTO } from 'src/models/LoginDTO';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  uri = `${environment.url}/api/login`;
  constructor(private http: HttpClient){ }

  Login(login:Login): Observable<LoginDTO>{
    return this.http.post<LoginDTO>(this.uri, login);
  }
}
