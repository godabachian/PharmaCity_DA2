import { InvokeFunctionExpr } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/models/Invitation';
import { InvitationIN } from 'src/models/InvitationIN';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrls: ['./invitation-list.component.css']
})
export class InvitationListComponent implements OnInit {
  invitations: Invitation[];
  invitationSelectedId: number;
  token: string|null;
  userName: string;
  pharmacyName: string;
  role: string;
  invitationCode: string;
  constructor(private invitationsService:InvitationsService, private _snackbar: MatSnackBar) { 
    this.invitations = [];
    this.invitationSelectedId = 0;
    this.token = localStorage.getItem('token');
    this.userName = "";
    this.pharmacyName = "";
    this.role = "";
    this.invitationCode = "";
  }

  ngOnInit(): void {
    this.GetInvitations();
  }


  GetInvitationInfo(invitation: Invitation){
    this.userName = invitation.userName;
    this.role = invitation.role.toString();
    this.pharmacyName = invitation.pharmacyName;
    this.invitationSelectedId = invitation.id;
    this.invitationCode = invitation.code;
  }

  GetNumberOfRole(role:string): number{
    if(role == "Administrator"){
      return 0;
    }
    if(role == "Owner"){
      return 1;
    }
    return 2;
  }

  GetInvitations(): void {

    this.invitationsService.GetInvitations(this.token).subscribe(
      (res) => {
        this.invitations = res;
      },
      (err) => {
        alert(err.error);
      }
    );

  }

  UpdateInvitation() {
    if (this.pharmacyName == "" || this.userName == "" || this.role == null) {
      this._snackbar.open("No hay datos suficientes", '', { duration: 1500 })
    } else {
      const invitation = new InvitationIN;
      invitation.userName = this.userName;
      invitation.role = this.GetNumberOfRole(this.role);
      invitation.pharmacyName = this.pharmacyName;

      this.invitationsService.UpdateInvitation(invitation, this.invitationSelectedId, this.token).subscribe(
        (res) => {
          alert("Se actualizo correctamente la invitación.");
          this.ngOnInit();
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

  @Output() newItemEvent = new EventEmitter<Invitation>();

  GetSelectedItem(invitation: Invitation){
    this.newItemEvent.emit(invitation);
  }

}
