import { BusinessModel } from './../models/business-model';
import { ActionType } from './../redux/action-type';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageModel } from '../models/message-model';
import { environment } from 'src/environments/environment';
import { Action } from '../redux/action';
import { store } from '../redux/store';
import { ScheduledMessageToMailingList } from '../models/scheduledMessageToMailingList.model';

@Injectable({
    providedIn: 'root'
})
export class MessagesService {

    business: BusinessModel = store.getState().business;

    constructor(private http: HttpClient) { }

    public loadAllmessagesAsync(): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http
                .get<MessageModel[]>(environment.messagesBaseUrl + "/" + this.business.businessId)
                .subscribe(messages => {
                    const action: Action = { type: ActionType.GetAllMessages, payload: messages };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public addMessage(message: MessageModel): Promise<MessageModel> {
        const observable = this.http.post<MessageModel>(environment.messagesBaseUrl + "/" + this.business.businessId, message);
        return observable.toPromise();
    }

    public UpdateMessage(updatedMessage: MessageModel): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.put<MessageModel>(environment.messagesBaseUrl + "/" + updatedMessage.messageId, updatedMessage)
                .subscribe(contact => {
                    const action: Action = { type: ActionType.UpdateContact, payload: contact };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public DeleteMessage(messageId: number): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.delete<MessageModel>(environment.messagesBaseUrl + "/" + messageId)
                .subscribe(message => {
                    const action: Action = { type: ActionType.DeleteMessage, payload: message.messageId };
                    store.dispatch(action);
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }

    public ClearAllmessages() { }

    public sendMessageToMailingLists(scheduledMessageToMailingList: ScheduledMessageToMailingList): Promise<undefined> {
        return new Promise<undefined>((resolve, reject) => {
            this.http.post<ScheduledMessageToMailingList>(environment.messagesBaseUrl + "/scheduled-message-to-mailinglist", scheduledMessageToMailingList)
                .subscribe(() => {
                    resolve();
                }, err => {
                    reject(err);
                });
        });
    }
}
