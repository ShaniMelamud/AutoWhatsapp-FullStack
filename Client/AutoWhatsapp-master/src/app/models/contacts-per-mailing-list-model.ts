import { ContactModel } from './contact.model';
import { MailingListModel } from './mailing-list-model';
export class ContactsPerMailingListModel {
    public constructor(
        public mailingListModel?: MailingListModel,
        public contactModel?: ContactModel[]
    ){}

}
