import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-id-input',
  templateUrl: './id-input.component.html',
  styleUrls: ['./id-input.component.css']
})
export class IdInputComponent implements OnInit {

  idNumber: string;
  constructor() { 
    this.idNumber = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  
  GetInputIdNumber(idNumber:string){
    this.newItemEvent.emit(idNumber);
  }

}
