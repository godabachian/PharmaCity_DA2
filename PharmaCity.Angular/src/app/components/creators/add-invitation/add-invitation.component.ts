import { Component, OnInit } from '@angular/core';
import { MatSelect } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { InvitationsService } from 'src/app/services/invitations.service';
import { Invitation } from 'src/models/Invitation';

@Component({
  selector: 'app-add-invitation',
  templateUrl: './add-invitation.component.html',
  styleUrls: ['./add-invitation.component.css']
})
export class AddInvitationComponent implements OnInit {

  userName: string;
  pharmacy: string;
  role: number;
  ver: boolean = false;

  token: string | null;
  constructor(private invitationService: InvitationsService, private _snackbar: MatSnackBar) {
    this.userName = "";
    this.pharmacy = "";
    this.role = 0;
    this.token = localStorage.getItem('token');
   }

  ngOnInit(): void {
  }

  GetInputUserName(userName:string){
    this.userName = userName;
  }
  
  GetInputPharmacy(pharmacy:string){
    this.pharmacy = pharmacy;
  }

  GetInputRole(role:string){
    this.role = Number(role);
  }

  CreateInvitation() {
    if (this.role == null || this.userName == "") {
      this._snackbar.open("Debe ingresar todos los campos requeridos.", "OK");
    } else {
      const invitation = new Invitation();
      invitation.userName = this.userName;
      invitation.role = this.role;
      invitation.pharmacyName = this.pharmacy;

      this.invitationService.InsertInvitation(invitation, this.token).subscribe(
        (res) => {
          alert("Se creo la invitacion correctamente.");
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

}
