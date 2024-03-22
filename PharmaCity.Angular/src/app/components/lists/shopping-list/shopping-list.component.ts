import { Component, OnInit } from '@angular/core';
import { ShoppingService } from 'src/app/services/shopping.service';
import { Petition } from 'src/models/Petition';
import { Purchase } from 'src/models/Purchase';
import { PurchaseDTO } from 'src/models/PurchaseDTO';
import { State } from 'src/models/State';
import { __values } from 'tslib';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.css']
})
export class ShoppingListComponent implements OnInit {

  shopping: PurchaseDTO[];
  petitions: Petition[];
  token: string|null;
  selectedPurchase: number;
  constructor(private shoppingService: ShoppingService) {
    this.shopping = [];
    this.petitions = [];
    this.token = localStorage.getItem('token');
    this.selectedPurchase = 0;
   }

  ngOnInit(): void {
    this.GetShopping();
  }

  GetShopping(): void{
    this.shoppingService.GetShopping(this.token).subscribe(
      (res) => {
        this.shopping = res;
      },
      (err) => {
        alert(err.error.errorMessage);
      }
    );
  }
  SelectPurchase(shop: PurchaseDTO): void{
    this.selectedPurchase = shop.id;
  }
  AcceptPurchase(): void{
    if(this.selectedPurchase == 0){
      alert("Debes seleccionar una solicitud y luego aceptarla.");
    }else{
      this.shoppingService.AcceptRequest(this.selectedPurchase,this.token).subscribe(
        (res) =>{
          alert(res);
          this.ngOnInit();
          this.petitions = [];
          this.selectedPurchase = 0;
        },
        (err) =>{
          alert(err.error.errorMessage);
        }
      );
    }
  }
  DeclinePurchase(): void{
    if(this.selectedPurchase == 0){
      alert("Debes seleccionar una solicitud y luego rechazarla.");
    }else{
      this.shoppingService.DeclineRequest(this.selectedPurchase,this.token).subscribe(
        (res) =>{
          alert(res);
          this.ngOnInit();
          this.petitions = [];
          this.selectedPurchase = 0;
        },
        (err) =>{
          alert(err.error.errorMessage);
        }
      );
    }
  }
  ChargePetitions(shop: PurchaseDTO){
    shop.shopping.forEach(petition => {
      petition.stateString = this.GetStringState(petition.state);
    });
    this.petitions = shop.shopping;
    
  }
  GetStringState(state: State): string {
    var stateString = State[state];
    return stateString;
  }

}
