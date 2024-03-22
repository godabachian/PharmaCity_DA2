import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-medicine-input',
  templateUrl: './medicine-input.component.html',
  styleUrls: ['./medicine-input.component.css']
})
export class MedicineInputComponent implements OnInit {

  medicine: string;
  constructor() { 
    this.medicine = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputMedicine(medicine:string){
    this.newItemEvent.emit(medicine);
  }

}
