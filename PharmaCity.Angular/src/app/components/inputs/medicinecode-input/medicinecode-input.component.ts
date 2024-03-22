import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-medicinecode-input',
  templateUrl: './medicinecode-input.component.html',
  styleUrls: ['./medicinecode-input.component.css']
})
export class MedicinecodeInputComponent implements OnInit {

  medicineCode: string;
  constructor() { 
    this.medicineCode = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>()
  GetInputCode(medicineCode:string){
    this.newItemEvent.emit(medicineCode);
  }

}
