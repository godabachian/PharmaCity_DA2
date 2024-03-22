import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-quantity-input',
  templateUrl: './quantity-input.component.html',
  styleUrls: ['./quantity-input.component.css']
})
export class QuantityInputComponent implements OnInit {

  quantity: string;
  constructor() { 
    this.quantity = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  GetInputQuantity(quantity: string){
    this.newItemEvent.emit(quantity);
  }
}
