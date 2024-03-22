import { Component, OnInit } from '@angular/core';
import { ShoppingService } from 'src/app/services/shopping.service';

@Component({
  selector: 'app-shopping-state',
  templateUrl: './shopping-state.component.html',
  styleUrls: ['./shopping-state.component.css']
})
export class ShoppingStateComponent implements OnInit {

  shoppingCode: string;
  constructor(private shoppingService:ShoppingService) {
    this.shoppingCode = "";
   }

  ngOnInit(): void {
  }


  GetShoppingState(){
    if(this.shoppingCode == ""){
      alert("Debe ingresar el codigo de su compra.");
    }else{
      this.shoppingService.GetShoppingState(this.shoppingCode).subscribe(
        (res) =>{
          alert("Su compra esta en estado: " + res);
        },
        (err) =>{
          alert(err.error);
        }
      );
    }
  }

}
