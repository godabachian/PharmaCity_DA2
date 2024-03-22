import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-price-input',
  templateUrl: './price-input.component.html',
  styleUrls: ['./price-input.component.css']
})
export class PriceInputComponent implements OnInit {

  price: string;
  constructor() {
    this.price = "";
   }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  GetInputPrice(price:string){
    this.newItemEvent.emit(price);
  }

}
