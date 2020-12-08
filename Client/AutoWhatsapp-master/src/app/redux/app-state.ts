import { ContactModel } from '../models/contact.model';
import { BusinessModel } from '../models/business-model';
import { MailingListModel } from '../models/mailing-list-model';
import { MessageModel } from '../models/message-model';

export class AppState {
    //מערכים של מידע
    public business: BusinessModel = null;
    public contacts: ContactModel[] = [];
    public businesses: BusinessModel[] = [];
    public mailingLists: MailingListModel[] = [];
    public messages: MessageModel[] = [];
    public addedMailingList: MailingListModel = null;

    public constructor() {
        this.business = JSON.parse(sessionStorage.getItem("business"));
    }
}