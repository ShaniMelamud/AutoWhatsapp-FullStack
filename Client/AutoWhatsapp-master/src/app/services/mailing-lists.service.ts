import { BusinessModel } from './../models/business-model';
import { MailingListModel } from './../models/mailing-list-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action } from '../redux/action';
import { ActionType } from '../redux/action-type';
import { store } from '../redux/store';
import { environment } from 'src/environments/environment';
import { ContactModel } from '../models/contact.model';
import { rejects } from 'assert';

@Injectable({
    providedIn: 'root'
})
export class MailingListsService {

    constructor(private http: HttpClient) { }
    business: BusinessModel = store.getState().business;

    public loadAllMailingListsAsync(): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http
                .get<MailingListModel[]>(environment.mailingListsBaseUrl + "/" + this.business.businessId)
                .subscribe(mailingLists => {
                    const action: Action = { type: ActionType.GetAllMailingLists, payload: mailingLists };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public addMailingList(MailingList: MailingListModel): Promise<MailingListModel> {
        return new Promise<MailingListModel>((resolve, reject) => {
            this.http
                .post<MailingListModel>(environment.mailingListsBaseUrl, MailingList)
                .subscribe(addedMailingList => {
                    const action: Action = { type: ActionType.AddMailingList, payload: addedMailingList };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public UpdateMailingList(updatedMailingList: MailingListModel): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.put<MailingListModel>(environment.mailingListsBaseUrl + "/" + updatedMailingList.mailingListId, updatedMailingList)
                .subscribe(mailingList => {
                    const action: Action = { type: ActionType.UpdateMailingList, payload: mailingList };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public DeleteMailingList(mailingListId: number): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.delete<MailingListModel>(environment.mailingListsBaseUrl + "/" + this.business.businessId + "/" + mailingListId)
                .subscribe(MailingList => {
                    const action: Action = { type: ActionType.DeleteMailigList, payload: mailingListId };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public addContactsToMailingList(mailingListId: number, contactsIds: number[]){
        const observable = this.http.post<ContactModel[]>(environment.mailingListsBaseUrl + "/" + this.business.businessId + "/" + mailingListId, contactsIds);
        return observable.toPromise();
    }

    public getContactsByMailingListId(mailingListId: number): Promise<ContactModel[]>{
        const observable = this.http.get<ContactModel[]>(environment.mailingListsBaseUrl + "/" + this.business.businessId + "/" + mailingListId);
        return observable.toPromise();
      }

    public ClearAllMailingLists() { }
}
