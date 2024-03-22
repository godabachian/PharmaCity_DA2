import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { StockRequestsService } from 'src/app/services/stock-requests.service';
import { Petition } from 'src/models/Petition';
import { StockRequest } from 'src/models/StockRequest';
import { CodeInputComponent } from '../../inputs/code-input/code-input.component';
import { MedicineInputComponent } from '../../inputs/medicine-input/medicine-input.component';

@Component({
  selector: 'app-add-stockrequest',
  templateUrl: './add-stockrequest.component.html',
  styleUrls: ['./add-stockrequest.component.css']
})
export class AddStockrequestComponent implements OnInit {
  medicineCode: string;
  quantity: number;
  petitions: Petition[];
  token: string|null;
  constructor(private stockRequestService: StockRequestsService, private _snackbar: MatSnackBar) {
    this.medicineCode = "";
    this.quantity = 0;
    this.petitions = [];
    this.token = localStorage.getItem('token');
  }

  ngOnInit(): void {
  }

  GetInputMedicineCode(medicineCode: string) {
    this.medicineCode = medicineCode;
  }
  ClearPetitions(){
    this.petitions = [];
    this._snackbar.open("Se han eliminado las peticiones correctamente", "", {duration: 1500});
  }

  GetInputQuantity(quantity: string) {
    this.quantity = Number(quantity);
  }

  CreatePetition() {
    if(this.medicineCode == ""||this.quantity == 0)
    {
      this._snackbar.open("Debe ingresar un codigo de medicina y una cantidad","Ok")
    } else
    {
      const petition = new Petition();
      petition.medicineCode = this.medicineCode.toUpperCase();
      petition.quantity = this.quantity;
      this.petitions.push(petition);
    }
      }


  InsertStockRequest() {

    if (this.petitions.length == 0) {
      alert("Debes cargar peticiones a la solicitud de stock.");
    } else {
      const stockRequest = new StockRequest()
      stockRequest.petitions = this.petitions;
      this.stockRequestService.InsertStockRequest(stockRequest, this.token).subscribe(
        (res) => {
          alert("Se ha creado correctamente la solicitud.");
          this.petitions = [];
        },
        (erro) => {
          alert(erro.error.errorMessage);
        }
      );
    }

  }

}
