import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-code-input',
  templateUrl: './code-input.component.html',
  styleUrls: ['./code-input.component.css']
})
export class CodeInputComponent implements OnInit {

  code: string;
  constructor() {
    this.code = "";
   }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputCode(code:string){
    this.newItemEvent.emit(code);
  }

}
