import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class EmployeeGuard implements CanActivate{

    role: string|null;

    constructor(private router: Router, private _snackBar: MatSnackBar){
        this.role = localStorage.getItem('role');
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        if(this.role == "Employee"){
            return true;
        }
        else{
            this._snackBar.open("Debes iniciar sesión como Employee", "Ok", {duration: 2000});
            this.router.navigate(['login']);
            return false;
        }
    }
}