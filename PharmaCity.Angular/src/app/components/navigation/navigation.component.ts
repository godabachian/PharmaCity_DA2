import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

      role: string|null;
  constructor(private breakpointObserver: BreakpointObserver, private router: Router) {
    this.role = localStorage.getItem('role');
  }

  LogOut(){
    this.router.navigate(['/login']);
    localStorage.setItem('role',"");
    localStorage.setItem('token',"");
    window.location.reload();
  }
}
