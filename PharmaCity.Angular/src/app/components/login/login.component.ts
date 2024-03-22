import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/services/login.service';
import { Login } from 'src/models/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string;
  password: string;
  token: string;
  constructor(private _snackBar: MatSnackBar, private loginService: LoginService, private router: Router) {
    this.email = "";
    this.password = "";
    this.token = "";
   }

  ngOnInit(): void {
  }

  GetInputEmail(email: string): void {
    this.email = email;
  }

  GetInputPassword(password: string): void {
    this.password = password;
  }

  @Output() newItemEvent = new EventEmitter<string>();

  Login(){
    if(this.email == "" || this.password == "")
    {
      this._snackBar.open("Debe ingresar todos los campos requeridos.", "OK");
    }
    else{
      const login = new Login();
      login.email = this.email;
      login.password = this.password;

      this.loginService.Login(login).subscribe(
        (result) => {
          this.token = result.token;
          localStorage.setItem('token', result.token);
          localStorage.setItem('role', result.role);
          alert("has iniciado sesion correctamente. Bienvenido, " + result.user);
          window.location.reload();
        },
        (err) => {
          alert(err.error.errorMessage);
        }
      );
    }
  }

}
