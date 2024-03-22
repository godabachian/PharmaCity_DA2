import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-role-select',
  templateUrl: './role-select.component.html',
  styleUrls: ['./role-select.component.css']
})
export class RoleSelectComponent implements OnInit {

  role: string;
  constructor() { 
    this.role = "";
  }

  ngOnInit(): void {
  }

  @Output() newItemEvent = new EventEmitter<string>();

  GetInputRole(role:string){
    this.newItemEvent.emit(role);
  }

}
