import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { Pharmacy } from 'src/models/Pharmacy';

@Component({
  selector: 'app-create-pharmacy',
  templateUrl: './create-pharmacy.component.html',
  styleUrls: ['./create-pharmacy.component.css']
})
export class CreatePharmacyComponent implements OnInit {

  pharmacy: string;
  direction: string;
  token: string | null;
  constructor(private pharmacyService: PharmacyService,private _snackbar: MatSnackBar) {
    this.pharmacy = "";
    this.direction = "";
    this.token = localStorage.getItem('token');
   }

  ngOnInit(): void {
  }

  GetInputPharmacy(pharmacy:string){
    this.pharmacy = pharmacy;
  }

  GetInputDirection(direction: string){
    this.direction = direction;
  }

  InsertPharmacy() {
    if (this.pharmacy == null || this.direction == "") {
      this._snackbar.open("Debe ingresar todos los campos requeridos.", "OK");
    } else {
      const pharmacy = new Pharmacy();
      pharmacy.name = this.pharmacy;
      pharmacy.direction = this.direction;

      this.pharmacyService.InsertPharmacy(pharmacy, this.token).subscribe(
        (res) => {
          alert("Se creó correctamente la farmacia: " + res.name);
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

}
