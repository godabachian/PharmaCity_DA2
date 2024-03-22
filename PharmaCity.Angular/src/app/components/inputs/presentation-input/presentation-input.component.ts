import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-presentation-input',
  templateUrl: './presentation-input.component.html',
  styleUrls: ['./presentation-input.component.css']
})
export class PresentationInputComponent implements OnInit {

  presentation: string;
  constructor() { 
    this.presentation = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputPresentation(presentation: string){
    this.newItemEvent.emit(presentation);
  }
}
