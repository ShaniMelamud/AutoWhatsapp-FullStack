import { ActionType } from './../../redux/action-type';
import { Action } from './../../redux/action';
import { BusinessesService } from './../../services/businesses.service';
import { Component, OnInit } from '@angular/core';
import { Unsubscribe } from 'redux';
import { store } from 'src/app/redux/store';
import { Router } from '@angular/router';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

    public business = store.getState().business;
    private unsubscribe: Unsubscribe;

    constructor(private businessService: BusinessesService, private router: Router) { }

    ngOnInit(): void {
        this.unsubscribe = store.subscribe(() => {
            this.business = store.getState().business;

        })
    }

    public async updateBusiness() {
        try {
            const updatedBusiness = await this.businessService.UpdateBusiness(this.business);
            alert('Profile has been updated: ' + this.business.businessId)
            this.router.navigateByUrl('dashboard/profile');
        }
        catch (err) { console.log(err.message) }
    }
}


