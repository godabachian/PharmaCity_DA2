import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Invitation } from 'src/models/Invitation';
import { InvitationIN } from 'src/models/InvitationIN';

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {

  uri = `${environment.url}/api/invitations`;
  constructor(private http: HttpClient) { }

  GetInvitations(token: string|null): Observable<Invitation[]>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.get<Invitation[]>(this.uri, {headers:headers});
  }

  InsertInvitation(invitation:Invitation, token:string|null): Observable<Invitation>{
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.post<Invitation>(this.uri, invitation, {headers:headers});
  }

  UpdateInvitation(invitation: InvitationIN, invitationSelected:number, token:string|null){
    const headers = new HttpHeaders().set('token', `${token}`);
    return this.http.put(`${this.uri}/${invitationSelected}`, invitation, {headers:headers});
  }
}
