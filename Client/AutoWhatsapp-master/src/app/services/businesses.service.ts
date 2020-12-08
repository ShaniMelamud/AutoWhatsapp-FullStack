import { BusinessModel } from './../models/business-model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Action } from '../redux/action';
import { ActionType } from '../redux/action-type';
import { store } from '../redux/store';

@Injectable({
    providedIn: 'root'
})
export class BusinessesService {

    constructor(private http: HttpClient) { }

    public loadAllBusinessesAsync(): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http
                .get<BusinessModel[]>(environment.businessesBaseUrl)
                .subscribe(businesses => {
                    const action: Action = { type: ActionType.GetAllBusinesses, payload: businesses };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public addBusiness(business: BusinessModel): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http
                .post<BusinessModel>(environment.businessesBaseUrl, business)
                .subscribe(addedBusiness => {
                    const action: Action = { type: ActionType.AddBusiness, payload: addedBusiness };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public UpdateBusiness(updatedBusiness: BusinessModel): Promise<undefined> {

        return new Promise<undefined>((resolve, reject) => {
            this.http.put<BusinessModel>(environment.businessesBaseUrl + "/" + updatedBusiness.businessId, updatedBusiness)
                .subscribe(business => {
                    const action: Action = { type: ActionType.UpdateBusiness, payload: business };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public DeleteBusiness(businessID: number): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.delete<BusinessModel>(environment.businessesBaseUrl + "/" + businessID)
                .subscribe(business => {
                    const action: Action = { type: ActionType.DeleteBusiness, payload: business.businessId };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }
    
    public ClearAllBusinesses() { }
}
