import { Component, OnInit } from '@angular/core';
import { MedicinesService } from 'src/app/services/medicines.service';

@Component({
  selector: 'app-delete-medicine',
  templateUrl: './delete-medicine.component.html',
  styleUrls: ['./delete-medicine.component.css']
})
export class DeleteMedicineComponent implements OnInit {

  medicineCode: string;
  token: string|null;
  constructor(private medicineService: MedicinesService) {
    this.medicineCode = "";
    this.token = localStorage.getItem('token');
   }

  ngOnInit(): void {
  }

  DeleteMedicine(){
    if(this.medicineCode == ""){
      alert("Debes ingresar un código de medicina.");
    }else{
      this.medicineService.DeleteMedicine(this.medicineCode, this.token).subscribe(
        (res) =>{
          alert("Se ha eliminado correctamente la medicina.");
        },
        (err) =>{
          alert("Se ha eliminado correctamente la medicina.");
        }
      );
    }
  }

}
