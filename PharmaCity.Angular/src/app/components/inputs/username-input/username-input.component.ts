import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-username-input',
  templateUrl: './username-input.component.html',
  styleUrls: ['./username-input.component.css']
})
export class UsernameInputComponent implements OnInit {


  userName: string;
  constructor() { 
    this.userName = "";
  }


  ngOnInit(): void {
  }
  
  @Output() newItemEvent = new EventEmitter<string>();

  GetInputUserName(userName:string):void{
    this.newItemEvent.emit(userName);
  }

}
