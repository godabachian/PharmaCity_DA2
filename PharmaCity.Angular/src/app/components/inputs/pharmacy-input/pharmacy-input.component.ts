import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pharmacy-input',
  templateUrl: './pharmacy-input.component.html',
  styleUrls: ['./pharmacy-input.component.css']
})
export class PharmacyInputComponent implements OnInit {

  pharmacy: string;
  constructor() { 
    this.pharmacy = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  GetInputPharmacy(pharmacy:string){
    this.newItemEvent.emit(pharmacy);
  }

}
