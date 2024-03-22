import { Component, OnInit,Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.css']
})
export class PasswordInputComponent implements OnInit {

  password:string;
  constructor() { 
    this.password = ""
  }
  hide = true;

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputPassword(password: string): void{
    this.newItemEvent.emit(password);
  }
}
