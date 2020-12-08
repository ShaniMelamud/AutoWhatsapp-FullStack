import { CredentialModel } from './../models/credential.model';
import { ActionType } from './../redux/action-type';
import { BusinessModel } from './../models/business-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { store } from '../redux/store';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient) { }

    public isUserNameExist(userName: string) : boolean{
        return;
    }

    public async register(business: BusinessModel): Promise<boolean> {
        try {
            const registeredBusiness = await this.http.post<BusinessModel>(environment.authBaseUrl, business).toPromise();
            store.dispatch({ type: ActionType.Register, payload: registeredBusiness });
            return true;
        }
        catch (httpErrorResponse) {
            store.dispatch({ type: ActionType.GotError, payload: httpErrorResponse });
            return false;
        }
    }
    
    public async login(credentials: CredentialModel): Promise<boolean> {
        try {
            const loggedInBusiness = await this.http.post<BusinessModel>(environment.authBaseUrl + "/login", credentials).toPromise();
            store.dispatch({ type: ActionType.Login, payload: loggedInBusiness });
            return true;
        }
        catch (httpErrorResponse) {
            store.dispatch({ type: ActionType.GotError, payload: httpErrorResponse });
            return false;
        }
    }

    public logout(): void {
        store.dispatch({ type: ActionType.Logout });
    }
}
