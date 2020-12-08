import { Notyf } from 'notyf';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { store } from '../redux/store';

@Injectable({
    providedIn: 'root'
})
export class LoginGuardService implements CanActivate {

    constructor(private router: Router) { }

    public canActivate(): boolean {
        if (store.getState().business) {
            return true;
        }
        const notify = new Notyf({ duration: 1000, ripple: false });
      //  notify.error("בשביל להכנס לעמוד זה, נדרש להכנס למערכת");
        //alert("Please Login!");

        this.router.navigateByUrl("/login");
        return false;
    }
}
