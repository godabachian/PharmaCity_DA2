import { Component, OnInit, EventEmitter, Output} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-email-input',
  templateUrl: './email-input.component.html',
  styleUrls: ['./email-input.component.css']
})
export class EmailInputComponent implements OnInit {

  email: string;

  constructor() {
    this.email = "";
  }

  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  getErrorMessage() {
    if (this.emailFormControl.hasError('required')) {
      return 'Debes ingresar el email';
    }

    return this.emailFormControl.hasError('email') ? 'El email no es válido' : '';
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();
  GetInputEmail(email: string): void{
    this.newItemEvent.emit(email);
  }

}
