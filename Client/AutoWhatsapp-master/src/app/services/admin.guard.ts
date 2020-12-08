import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Notyf } from 'notyf';
import { store } from '../redux/store';

@Injectable({
    providedIn: 'root'
})
export class AdminGuard implements CanActivate {
    
public constructor(private router: Router){}

    canActivate(): boolean {
        if (store.getState().business?.role === "Admin") {
            return true;
        }
        const notify = new Notyf({ duration: 4000, ripple: false });
      //  notify.error("בשביל להכנס לעמוד זה, נדרש להכנס למערכת");
        alert("You are not Admin!");

        //this.router.navigateByUrl("/login");
        return false;
    }

}
