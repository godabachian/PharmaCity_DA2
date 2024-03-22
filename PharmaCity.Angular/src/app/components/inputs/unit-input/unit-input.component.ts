import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-unit-input',
  templateUrl: './unit-input.component.html',
  styleUrls: ['./unit-input.component.css']
})
export class UnitInputComponent implements OnInit {

  unit: string;
  constructor() {
    this.unit = "";
   }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputUnit(unit: string){
    this.newItemEvent.emit(unit);
  }
}
