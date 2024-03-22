import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-direction-input',
  templateUrl: './direction-input.component.html',
  styleUrls: ['./direction-input.component.css']
})
export class DirectionInputComponent implements OnInit {

  direction: string;
  constructor() { 
    this.direction = ""
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  GetInputDirection(direction:string){
    this.newItemEvent.emit(direction);
  }

}
