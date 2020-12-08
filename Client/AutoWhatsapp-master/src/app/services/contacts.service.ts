import { ActionType } from './../redux/action-type';
import { ContactModel } from './../models/contact.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action } from '../redux/action';
import { store } from '../redux/store';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class ContactsService {

    private business = JSON.parse(sessionStorage.getItem("business"))

    constructor(private http: HttpClient) { }

    public async getAllContacts(): Promise<boolean> {
        try {
            const contacts = await this.http
                .get<ContactModel[]>(
                    environment.contactsBaseUrl + "/" +
                    this.business.businessId
                )
                .toPromise();
            store.dispatch({ type: ActionType.GetAllContacts, payload: contacts });
            return true
        }
        catch (err) {
            console.log(err.message)
            return false;
        }
    }

    public addContact(contact: ContactModel): Promise<undefined> {

        return new Promise<undefined>((resolve, reject) => {
            this.http
                .post<ContactModel>(environment.contactsBaseUrl + "/" + this.business.businessId, contact)
                .subscribe(addedContact => {
                    const action: Action = { type: ActionType.AddContact, payload: addedContact };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public UpdateContact(updatedContact: ContactModel): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.put<ContactModel>(
                environment.contactsBaseUrl + "/" + this.business.businessId +
                "/" + updatedContact.contactId, updatedContact)
                .subscribe(contact => {
                    const action: Action = { type: ActionType.UpdateContact, payload: contact };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public DeleteContact(contactID: number): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.delete<ContactModel>(environment.contactsBaseUrl + "/" + contactID)
                .subscribe(() => {
                    const action: Action = { type: ActionType.DeleteContact, payload: contactID };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public LoadContactsViaFile(formData: FormData) {
        this.http.post<any>(environment.xlsxFileUploadBaseUrl + "/" + this.business.businessId, formData).subscribe(response => {
            console.log(response);
        }, error => {
            console.log(error);
        });
    }

    public ClearAllContacts() { }
}
