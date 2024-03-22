import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class OwnerGuard implements CanActivate{

    role: string|null;

    constructor(private router: Router, private _snackBar: MatSnackBar){
        this.role = localStorage.getItem('role');
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        if(this.role == "Owner"){
            return true;
        }
        else{
            this._snackBar.open("Debes iniciar sesión como Owner", "Ok", {duration: 2000});
            this.router.navigate(['login']);
            return false;
        }
    }
}