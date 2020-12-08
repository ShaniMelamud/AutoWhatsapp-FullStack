import { Action } from './action';
import { ActionType } from './action-type';
import { AppState } from './app-state';
import { Notyf } from 'notyf';

const notify = new Notyf({ duration: 4000, ripple: false });

export function reducer(currentState: AppState, action: Action): AppState {

    let newState = { ...currentState };

    switch (action.type) {

        case ActionType.Register:
        case ActionType.Login: {
            newState.business = action.payload;
            sessionStorage.setItem("business", JSON.stringify(newState.business));
            break;
        }
        case ActionType.Logout: {
            newState.business = null;
            newState.contacts = [];
            newState.businesses = [];
            sessionStorage.removeItem("business");
            break;
        }
        case ActionType.GetAllContacts: {
            newState.contacts = action.payload;
            break;
        }
        case ActionType.AddContact: {
            newState.contacts.push(action.payload);
            break;
        }
        case ActionType.UpdateContact: {
            const index = newState.contacts.findIndex(c => c.contactId === action.payload.contactID);
            newState.contacts[index] = action.payload;
            break;
        }
        case ActionType.DeleteContact: {
            const index = newState.contacts.findIndex(c => c.contactId === action.payload);
            newState.contacts.splice(index, 1);
            break;
        }
        case ActionType.GetAllBusinesses: {
            newState.businesses = action.payload;
            break;
        }
        case ActionType.AddBusiness: {
            newState.businesses.push(action.payload);
            break;
        }
        case ActionType.UpdateBusiness: {
            newState.business = action.payload;
            sessionStorage.setItem("business", JSON.stringify(newState.business));
            break;
        }
        case ActionType.DeleteBusiness: {
            const index = newState.businesses.findIndex(b => b.businessId === action.payload.businessId);
            newState.businesses.splice(index, 1);
            break;
        }
        case ActionType.ClearAllContact: {
            newState.contacts = [];
            break;
        }
        case ActionType.GetAllMailingLists: {
            newState.mailingLists = action.payload;
            break;
        }
        case ActionType.AddMailingList: {
            newState.mailingLists.push(action.payload);
            newState.addedMailingList = action.payload;
            break;
        }
        case ActionType.UpdateMailingList: {
            const index = newState.mailingLists.findIndex(c => c.mailingListId === action.payload.mailingListId);
            newState.mailingLists[index] = action.payload;
            break;
        }
        case ActionType.DeleteMailigList: {
            const index = newState.mailingLists.findIndex(c => c.mailingListId === action.payload.MailingListId);
            newState.mailingLists.splice(index, 1);
            break;
        }
        case ActionType.ClearALlMailingLists: {
            newState.mailingLists = [];
            break;
        }

        case ActionType.GetAllMessages: {
            newState.messages = action.payload;
            break;
        }
        case ActionType.AddMessage: {
            newState.messages.push(action.payload);
            break;
        }
        case ActionType.UpdateMessage: {
            const index = newState.messages.findIndex(c => c.messageId === action.payload.messageId);
            newState.messages[index] = action.payload;
            break;
        }
        case ActionType.DeleteMessage: {
            const index = newState.messages.findIndex(c => c.messageId === action.payload.messageId);
            newState.messages.splice(index, 1);
            break;
        }
        case ActionType.ClearAllMessages: {
            newState.messages = [];
            break;
        }
    }
    return newState;
}