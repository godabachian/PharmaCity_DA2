import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MedicinesService } from 'src/app/services/medicines.service';
import { Medicine } from 'src/models/Medicine';
import { Petition } from 'src/models/Petition';
import { Purchase } from 'src/models/Purchase';

@Component({
  selector: 'app-medicine-list',
  templateUrl: './medicine-list.component.html',
  styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit {

  medicines: Medicine[];
  nameMedicine: string;
  namePharmacy: string;
  cart: Petition[];
  quantity: string;
  medicineSelectedId: number;
  medicineSelected: Medicine;
  constructor(private medicinesService: MedicinesService,private _snackBar: MatSnackBar) { 
    this.medicines = [];
    this.nameMedicine = "";
    this.namePharmacy = "";
    this.cart = [];
    this.quantity = "";
    this.medicineSelectedId = 0;
    this.medicineSelected = new Medicine;
  }

  ngOnInit(): void {
    this.GetMedicines();
  }
  ClearCart(): void{
    this.nameMedicine = "";
    this.namePharmacy = "";
    this.cart = [];
    this.quantity = "";
    this.medicineSelectedId = 0;
    this.medicineSelected = new Medicine;
    this._snackBar.open("El carrito se ha limpiado correctamente",'',{duration: 1500});
  }
  GetMedicines(): void{

    try{
      this.nameMedicine="";
      this.namePharmacy="";
      this.medicinesService.GetMedicines().subscribe(
        (res) => {
          this.medicines = res;
          this.medicineSelectedId = 0;
        },
        (err) => {
          alert(err.error);
        }
      );
    }catch{
      alert("No estas conectado a la api");
    }

    
  }

  GetMedicinesByFilter(): void{
    this.medicinesService.GetMedicinesByFilter(this.nameMedicine, this.namePharmacy).subscribe(
      (res) => {
        this.medicines = res;
        this.medicineSelectedId = 0;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

  SelectMedicine(medicine:Medicine){
    this.medicineSelectedId = medicine.id;
    this.medicineSelected = medicine;
  }

  AddToCartMedicine(){
    if(this.medicineSelectedId == 0){
      alert("Selecciona una medicina para comprarla.");
    }
    else{
      if(this.quantity == "" || this.quantity == "0"){
        alert("Debes ingresar una cantidad a comprar.");
      }
      else{
        const petition = new Petition();
        petition.medicineCode = this.medicineSelected.code;
        petition.quantity = Number(this.quantity);
        this.cart.push(petition);
      }
    }
  }

  BuyMedicines(){
    if(this.cart.length == 0){
      alert("El carrito esta vacío.");
    }
    else{
      const purchase = new Purchase();
      purchase.shopping = this.cart;

      this.medicinesService.BuyMedicines(purchase).subscribe(
        (res) =>{
          this.cart = [];
          alert("Has comprado con éxito");
        },
        (err) =>{
          alert(err.error.errorMessage);
        }
      );
      
    }
  }


}
