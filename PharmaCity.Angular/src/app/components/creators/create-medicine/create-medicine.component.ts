import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MedicinesService } from 'src/app/services/medicines.service';
import { Medicine } from 'src/models/Medicine';

@Component({
  selector: 'app-create-medicine',
  templateUrl: './create-medicine.component.html',
  styleUrls: ['./create-medicine.component.css']
})
export class CreateMedicineComponent implements OnInit {

  medicine: string;
  symptoms: string;
  presentation: string;
  unit: string;
  receipt: string;
  quantity: number;
  price: number;
  token: string | null;
  constructor(private medicineService: MedicinesService, private _snackbar: MatSnackBar) { 
    this.medicine = "";
    this.symptoms = "";
    this.presentation = "";
    this.unit = "";
    this.receipt = "";
    this.quantity = 0;
    this.price = 0;
    this.token = localStorage.getItem('token');
  }

  ngOnInit(): void {
  }

  GetInputMedicine(medicine:string){
    this.medicine = medicine;
  }

  GetInputPresentation(presentation: string){
    this.presentation = presentation;
  }

  GetInputUnit(unit: string){
    this.unit = unit;
  }

  GetInputSymptoms(symptoms: string){
    this.symptoms = symptoms;
  }

  GetInputQuantity(quantity: string){
    this.quantity = Number(quantity);
  }

  GetInputPrice(price:string){
    this.price = Number(price);
  }

  CreateMedicine() {
    if (this.medicine == "" || this.symptoms == "" || this.presentation == "" || this.unit == "" || this.receipt == "" || this.quantity == null || this.price == null) {
      this._snackbar.open("Debe ingresar todos los campos requeridos.", "OK");
    } else {
      const medicine = new Medicine();
      medicine.name = this.medicine;
      medicine.presentation = this.presentation;
      medicine.unit = this.unit;
      medicine.receipt = this.receipt;
      medicine.symptoms = this.symptoms;
      medicine.quantity = this.quantity;
      medicine.price = this.price;

      this.medicineService.InsertMedicine(medicine, this.token).subscribe(
        (res) => {
          alert("Se creó correctamente la medicina: " + res.name);
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

}
