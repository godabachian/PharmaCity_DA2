import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-symptoms-input',
  templateUrl: './symptoms-input.component.html',
  styleUrls: ['./symptoms-input.component.css']
})
export class SymptomsInputComponent implements OnInit {

  symptoms: string;
  constructor() {
    this.symptoms = "";
   }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputSymptoms(symptoms: string){
    this.newItemEvent.emit(symptoms);
  }
}
