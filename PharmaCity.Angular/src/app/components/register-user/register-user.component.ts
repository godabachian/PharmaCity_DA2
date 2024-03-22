import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/models/User';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  invitationCode: string;
  userName: string;
  direction:string;
  email: string;
  password: string;

  constructor(private userService: UsersService,private _snackbar: MatSnackBar) { 
    this.invitationCode = "";
    this.userName = "";
    this.direction = "";
    this.email = "";
    this.password = "";
  }
  ClearVariables():void{
    this.invitationCode = "";
    this.userName = "";
    this.direction = "";
    this.email = "";
    this.password = "";
  }
  ngOnInit(): void {
  }

  GetInputUserName(userName:string){
    this.userName = userName;
  }

  GetInputDirection(direction:string){
    this.direction = direction;
  }

  GetInputEmail(email:string){
    this.email = email;
  }

  GetInputPassword(password:string){
    this.password = password;
  }

  GetInputCode(code:string){
    this.invitationCode = code;
  }

  CreateUser(){
    if(this.email == "" || this.password == "" || this.invitationCode == ""|| this.userName == ""|| this.direction == "")
    {
      this._snackbar.open("Debe ingresar todos los campos requeridos.", "OK");
    }else{
      const user = new User();
      user.email = this.email;
      user.userName = this.userName;
      user.direction = this.direction;
      user.password = this.password;

      this.userService.InsertUser(user,this.invitationCode).subscribe(
        (res) =>{
          alert("Usuario creado");
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

}
