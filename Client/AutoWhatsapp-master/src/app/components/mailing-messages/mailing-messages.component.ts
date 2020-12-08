import { ScheduledMessageToMailingList } from './../../models/scheduledMessageToMailingList.model';
import { MessagesService } from './../../services/messages.service';
import { MessageModel } from './../../models/message-model';
import { BusinessModel } from './../../models/business-model';
import { ContactsService } from './../../services/contacts.service';
import { ContactModel } from './../../models/contact.model';
import { Unsubscribe } from 'redux';
import { MailingListModel } from './../../models/mailing-list-model';
import { AfterViewInit, Component, HostListener, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { store } from 'src/app/redux/store';
import { MailingListsService } from 'src/app/services/mailing-lists.service';

@Component({
    selector: 'app-mailing-messages',
    templateUrl: './mailing-messages.component.html',
    styleUrls: ['./mailing-messages.component.scss']
})
export class MailingMessagesComponent implements OnInit, AfterViewInit {

    mailingLists: MailingListModel[] = [];
    mailingListsToShow: MailingListModel[] = [];
    mailingListIdsToSendMessage: number[] = [];
    newMailingListContacts: ContactModel[] = [];
    newMailingList: MailingListModel;
    currenMailingListToEdit: MailingListModel;
    mailingListToDelete: MailingListModel;

    messages: MessageModel[] = [];
    messagesToShow: MessageModel[] = [];
    newMessage: MessageModel = new MessageModel();
    messageToSend: MessageModel = new MessageModel();
    messageToEdit: MessageModel;
    messageToDelete: MessageModel;

    scheduledMessageToMailingList: ScheduledMessageToMailingList = new ScheduledMessageToMailingList();

    contacts: ContactModel[] = [];
    contactsToShow: ContactModel[] = [];
    contactToEdit: ContactModel = new ContactModel();
    business: BusinessModel = new BusinessModel();

    localTextMessageDialogRef;
    localMailingListDialogRef;
    localChooseMailingListDialogRef;
    localCreateNewMailingListDialogRef;
    localEditMailingListDialogRef;
    localMessagesDialogRef;
    localEditMessageDialogRef;
    localCreateNewMessageDialogRef;
    localGiveNameToMailingListDialogRef;
    localDeleteMessageDialogRef;
    localConfirmDeleteMailingListDialogRef;
    localNoTextInMessageDialogRef;
    localNoDateChosenDialogRef;
    localConfirmSendMessageDialogRef;
    localNoMailingListChosenDialogRef;
    localAddContactsToMailingListDialogRef;
    localConfirmEditMailingListDialogRef;
    localEditContactDialogRef;
    localConfirmDeleteContactDialogRef;
    

    mailingListCheckboxes: boolean[] = [];
    contactCheckboxes: boolean[] = [];
    messagesCheckboxes: boolean[] = [];
    checkboxes: boolean[] = [];

    changeStr;

    datetimeString: string = "";

    shouldCall = false;
    dialogWidth = '50%';
    unsubscribe: Unsubscribe

    constructor(
        private dialog: MatDialog,
        private dialogRef: MatDialogRef<any>,
        private mailingListsService: MailingListsService,
        private contactsService: ContactsService,
        private messagesService: MessagesService
    ) { }

    async ngOnInit() {
        this.unsubscribe = store.subscribe(() => {
            this.mailingLists = store.getState().mailingLists;
            this.contacts = store.getState().contacts;
            this.messages = store.getState().messages;
            this.business = store.getState().business;
        });

        if (store.getState().mailingLists.length > 0) {
            this.mailingLists = store.getState().mailingLists;
        }
        else {
            try {
                await this.mailingListsService.loadAllMailingListsAsync();
                console.log("MailingLists: " + this.mailingLists);
            }
            catch (err) {
                console.log(err.massage);
            }
        }

        if (store.getState().contacts.length > 0) {
            this.contacts = store.getState().contacts;
        }
        else {
            try {
                await this.contactsService.getAllContacts();
                console.log("contacts: " + this.contacts);
            }
            catch (err) {
                console.log(err.massage);
            }
        }

        if (store.getState().messages.length > 0) {
            this.messages = store.getState().messages;
        }
        else {
            try {
                await this.messagesService.loadAllmessagesAsync();
                console.log("messages: " + this.messages);
            }
            catch (err) {
                console.log(err.massage);
            }
        }

        this.scheduledMessageToMailingList.mailingListIds = new Array<number>();
        this.newMailingList = { mailingListName: "" };

        if (window.innerWidth > 993) {
            this.dialogWidth = '50%';
        }
        if (window.innerWidth < 993) {
            this.dialogWidth = '80%';
        }
        if (window.innerWidth < 576) {
            this.dialogWidth = '90%';
        }

        this.contactsToShow = this.contacts;
        this.mailingListsToShow = this.mailingLists;
        this.messagesToShow = this.messages;
        this.messageToSend.messageContent = "";
        this.messageToSend.businessId = store.getState().business.businessId;
    }

    ngAfterViewInit() {
        this.shouldCall = true;
    }


    openChooseMailingListsDialog(templateRef) {
        this.checkboxes=[];
        this.mailingListIdsToSendMessage = [];
        this.mailingListsToShow = this.mailingLists;
        this.localChooseMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openMailingListsDialog(templateRef) {
        this.mailingListsToShow = this.mailingLists;
        this.localMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    async openCreateNewMailingListDialog(templateRef) {
        this.mailingListsToShow = this.mailingLists;
        this.contactsToShow = this.contacts;
        this.checkboxes = [];
        this.localCreateNewMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    async openEditMailingListDialog(templateRef, mailingListId: number) {
        this.currenMailingListToEdit = this.mailingLists.find(p=>p.mailingListId===mailingListId);
        this.contactsToShow = await this.mailingListsService.getContactsByMailingListId(mailingListId);
        this.localEditMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openMessagesDialog(templateRef) {
        this.messagesToShow = this.messages;
        this.localMessagesDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openEditMessageDialog(templateRef, messageId: number) {
        this.messageToEdit = this.messages.find(p => p.messageId === messageId);
        this.localEditMessageDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        })
    }
    openCreateNewMessageDialog(templateRef) {
        this.checkboxes=[];
        this.localCreateNewMessageDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        })
    }
    openGiveNameToMailingListDialog(templateRef) {
        this.localGiveNameToMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        })
    }
    openConfirmDeleteMailingListDialog(templateRef, mailingListId: number) {
        this.mailingListToDelete = this.mailingLists.find(p => p.mailingListId === mailingListId);
        this.localConfirmDeleteMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openDeleteMessageDialog(templateRef, messageId: number) {
        this.messageToDelete = this.messages.find(p => p.messageId === messageId);
        this.localDeleteMessageDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openNoTextInMessageDialog(templateRef) {
        this.localNoTextInMessageDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openlocalNoDateChosenDialogRef(templateRef) {
        this.localNoDateChosenDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openConfirmSendMessageDilog(confirmSendTemplateRef, noMailingLislChosenTemplate) {
        if (this.scheduledMessageToMailingList.mailingListIds.length === 0) {
            this.localNoMailingListChosenDialogRef = this.dialog.open(noMailingLislChosenTemplate)
        }
        else {
            this.onCloseChooseMailingListsDialog();
            console.log(this.scheduledMessageToMailingList.mailingListIds);
            this.localConfirmSendMessageDialogRef = this.dialog.open(confirmSendTemplateRef, {
                width: this.dialogWidth,
            })
        }
    }
    async openLocalAddContactsToMailingListDialog(templateRef){
        this.contactsToShow = this.contacts;
        const mailingListContacts = await this.mailingListsService.getContactsByMailingListId(this.currenMailingListToEdit.mailingListId);
        for(let c of mailingListContacts){
            this.contactsToShow.filter(p=>p.contactId!==c.contactId);
        }
        this.checkboxes=new Array<boolean>(this.contacts.length);
        for(let i = 0;i<this.checkboxes.length;i++){
            const contact = mailingListContacts.find(p=>p.contactId===this.contacts[i].contactId);
            if(contact!==undefined){
                this.checkboxes[i]=true;
            }
            else{
                this.checkboxes[i]=false;
            }
        }
        this.localAddContactsToMailingListDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    openConfirmEditMailingListDialog(templateRef){
        this.localConfirmEditMailingListDialogRef = this.dialog.open(templateRef), {
            width: this.dialogWidth,
        }
    }
    openEditContactDialog(templateRef, contactId: number){
        this.contactToEdit = this.contacts.find(p=>p.contactId===contactId);
        this.localEditContactDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
    async openConfirmDeleteContactDialog(templateRef, contactId){
        this.localConfirmDeleteContactDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
        await this.contactsService.DeleteContact(contactId);
    }
    

    onClose(){
        this.dialog.closeAll();
    }
    onCloseMessageDialog() {
        this.localTextMessageDialogRef.close();
    }
    onCloseChooseMailingListsDialog() {
        this.localChooseMailingListDialogRef.close();
    }
    onCloseMailingListsDialog() {
        this.dialog.closeAll();
    }
    onCloseCreateNewMailingListDialog() {
        this.localCreateNewMailingListDialogRef.close();
    }
    onCloseEditMailingListDialog() {
        this.localEditMailingListDialogRef.close();
    }
    onCloseEditMessageDialog() {
        this.localEditMessageDialogRef.close();
    }
    onCloseGiveNmaeToMailingListDialog() {
        this.localGiveNameToMailingListDialogRef.close();
    }
    onCloseMessagesDialog() {
        this.localMessagesDialogRef.close();
    }
    onCloseDeleteMessageDialog() {
        this.localDeleteMessageDialogRef.close();
    }
    onCloseConfirmDeleteMailingListDialog() {
        this.localConfirmDeleteMailingListDialogRef.close();
    }
    onCloseCreateNewMessageDialog() {
        this.localCreateNewMessageDialogRef.close();
    }
    onCloseNoTextInMessageDialog() {
        this.localNoTextInMessageDialogRef.close();
    }
    onCloseNoDateChosenDialog() {
        this.localNoDateChosenDialogRef.close();
    }
    onCloseNoMailingListChosenDialog() {
        this.localNoMailingListChosenDialogRef.close();
    }
    onCloseConfirmSendMessageDialog() {
        this.localConfirmSendMessageDialogRef.close();
    }
    onCloseAddContactsToMailingListDialog(){
        this.localAddContactsToMailingListDialogRef.close();
    }
    onCloseConfirmEditMailingListDialog(){
        this.localAddContactsToMailingListDialogRef.close;
    }
    onCloseEditContactDialog(){
        this.localEditContactDialogRef.close();
    }



    toggleAllCheckboxes(event, arrayLength: number) {
        this.checkboxes = new Array<boolean>(arrayLength);
        if (event.checked) {
            for (let i = 0; i < this.checkboxes.length; i++) {
                this.checkboxes[i] = true;
            }
        }
        else if (!event.checked) {
            for (let i = 0; i < this.checkboxes.length; i++) {
                this.checkboxes[i] = false;
            }
        }
    }
    setMailingListIdsToArray(){
        for(let i = 0;i<this.checkboxes.length;i++){
            if(this.checkboxes[i]===true){
                this.scheduledMessageToMailingList.mailingListIds.push(this.mailingLists[i].mailingListId)
            }
        }
    }
    setContactsToArray(){
        for(let i = 0;i<this.checkboxes.length;i++){
            if(this.checkboxes[i]===true){
                this.newMailingListContacts.push(this.contacts[i]);
            }
        }
        console.log(this.newMailingListContacts)
    }
    
    // toggleMailingList(event, i: number) {

    //     if (event.checked === true) {
    //         this.mailingListIdsToSendMessage.push(this.mailingLists[i].mailingListId);
    //         console.log(this.mailingListIdsToSendMessage);
    //     }
    //     else if (event.checked === false) {
    //         const mailingListIdToDelete = this.mailingLists[i].mailingListId;
    //         const remove = this.mailingListIdsToSendMessage.findIndex(p => p === mailingListIdToDelete);
    //         this.mailingListIdsToSendMessage.splice(remove, 1);
    //         console.log(this.mailingListIdsToSendMessage);
    //     }
    // }
    // toggleContact(event, i: number) {
    //     if (event.checked === true) {
    //         this.newMailingListContacts.push(this.contacts[i]);
    //     }
    //     else if (event.checked === false) {
    //         const contactIdToDelete = this.contacts.find(p => p.contactId === this.contacts[i].contactId).contactId;
    //         if (contactIdToDelete != null) {
    //             const deleteIndex = this.newMailingListContacts.findIndex(p => p.contactId === contactIdToDelete);
    //             this.newMailingListContacts.splice(deleteIndex, 1);
    //         }
    //     }
    // }


    mailingListsChoosingDone() {
        console.log(this.newMailingListContacts);
    }

    giveNmaeToMailingListDone() {
       console.log(this.newMailingListContacts);
    }

    async createNewMessageDone() {
        await this.messagesService.addMessage(this.newMessage);
    }

    async addMailingListToServer(){
        if(this.newMailingList.mailingListName===""){
            //todo
        }
        this.newMailingList.businessId = this.business.businessId;
        await this.mailingListsService.addMailingList(this.newMailingList);
        this.newMailingList = store.getState().addedMailingList;
        const contactsIds = new Array<number>();
        for(const item of this.newMailingListContacts){
            contactsIds.push(item.contactId);
        }
        await this.mailingListsService.addContactsToMailingList(this.newMailingList.mailingListId, contactsIds);
        this.newMailingList = new MailingListModel();
        this.newMailingListContacts = [];
    }

    async sendMessage(noTextTemplateRef, noDateTemplateRef, chooseMailingListsTemplateRef) {
        if (this.messageToSend.messageContent != "" && this.datetimeString != "") {
            console.log(this.messageToSend);
            // this.messageToSend = await this.messagesService.addMessage(this.messageToSend);
            this.scheduledMessageToMailingList.mailingListIds = [];          
            this.localChooseMailingListDialogRef = this.dialog.open(chooseMailingListsTemplateRef, {
                width: this.dialogWidth,
            })
        }
        if (this.messageToSend.messageContent === "") {
            this.localNoTextInMessageDialogRef = this.dialog.open(noTextTemplateRef, {
                width: this.dialogWidth,
            })
        }
        if (this.datetimeString === "") {
            this.localNoDateChosenDialogRef = this.dialog.open(noDateTemplateRef, {
                width: this.dialogWidth,
            })
        }
    }

    async editMailingList(){
        let contactsIds = new Array<number>();
        for(let c of this.newMailingListContacts){
            contactsIds.push(c.contactId);
        }
        await this.mailingListsService.addContactsToMailingList(this.currenMailingListToEdit.mailingListId, contactsIds);
    }

    public async editContact() {
        try {
            this.contactToEdit.businessId = this.business.businessId
            await this.contactsService.UpdateContact(this.contactToEdit);
            alert("Contact has been added")
            // this.router.navigateByUrl("/dashboard")
        }
        catch (err) { console.log(err.Message) }
    }

    async sendMessageToContacts() {
        this.messageToSend.businessId = this.business.businessId;
        const messageId = await (await this.messagesService.addMessage(this.messageToSend)).messageId
        console.log(messageId);
        this.scheduledMessageToMailingList.messageId = messageId;
        this.scheduledMessageToMailingList.datetime = new Date(this.datetimeString);
        this.scheduledMessageToMailingList.businessId = this.business.businessId;
        console.log(this.scheduledMessageToMailingList);
        const d: Date = new Date(this.datetimeString);
        this.scheduledMessageToMailingList.datetime = d;
        await this.messagesService.sendMessageToMailingLists(this.scheduledMessageToMailingList);
    }

    setDateTimeToNow() {
        const date = new Date();
        date.setTime(date.getTime() + 2 * 60 * 60 * 1000);
        this.datetimeString = date.toISOString().slice(0, 16);
    }

    async deleteMailingList(mailingListsTempalteRef) {
        this.onCloseConfirmDeleteMailingListDialog();
        console.log("redux1: " );
        console.log(store.getState().mailingLists.find(p=>p.mailingListId===this.mailingListToDelete.mailingListId));
        await this.mailingListsService.DeleteMailingList(this.mailingListToDelete.mailingListId);
        this.mailingListsToShow = store.getState().mailingLists;
        
        this.mailingListsToShow = this.mailingLists;

        console.log(this.mailingLists);
        console.log(this.mailingListsToShow);
    }

    async deleteMessage() {
        console.log("")
        await this.messagesService.DeleteMessage(this.messageToDelete.messageId);
    }

    searchMailingLists() {
        this.mailingListsToShow = this.mailingLists.filter(m =>
            m.mailingListName
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
        );
    }

    searchContacts() {
        this.contactsToShow = this.contacts.filter(c =>
            c.contactPhone
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
            ||
            c.contactName
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
        );
    }

    searchMessages() {
        console.log(this.messagesToShow);
        this.messagesToShow = this.messages.filter(m =>
            m.messageContent
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
        );
    }
    
    clearSearchString(){
        this.changeStr = "";
        // this.contactsToShow = [];
        // this.messagesToShow = [];
        // this.mailingListsToShow = [];
    }




    @HostListener('window:resize', ['$event'])
    onResize(event) {
        if (this.shouldCall) this.updateWidthOfDialogue();
    }


    updateWidthOfDialogue() {
        if (window.innerWidth > 993) {
            this.localTextMessageDialogRef.updateSize('50%', '');
            this.localMailingListDialogRef.updateSize('50%', '');
            this.localCreateNewMailingListDialogRef.updateSize('50%', '');
            this.localEditMailingListDialogRef.updateSize('50%', '');
            this.localMessagesDialogRef.updateSize('50%', '');
            this.localEditMessageDialogRef.updateSize('50%', '');
            this.localCreateNewMessageDialogRef.updateSize('50%', '');
            this.localGiveNameToMailingListDialogRef.updateSize('50%', '');
        }
        if (window.innerWidth < 993) {
            this.localTextMessageDialogRef.updateSize('80%', '');
            this.localMailingListDialogRef.updateSize('80%', '');
            this.localCreateNewMailingListDialogRef.updateSize('80%', '');
            this.localEditMailingListDialogRef.updateSize('80%', '');
            this.localMessagesDialogRef.updateSize('80%', '');
            this.localEditMessageDialogRef.updateSize('80%', '');
            this.localCreateNewMessageDialogRef.updateSize('80%', '');
            this.localGiveNameToMailingListDialogRef.updateSize('80%', '');
        }
        if (window.innerWidth < 576) {
            this.localTextMessageDialogRef.updateSize('90%', '');
            this.localMailingListDialogRef.updateSize('90%', '');
            this.localCreateNewMailingListDialogRef.updateSize('90%', '');
            this.localEditMailingListDialogRef.updateSize('90%', '');
            this.localMessagesDialogRef.updateSize('90%', '');
            this.localEditMessageDialogRef.updateSize('90%', '');
            this.localCreateNewMessageDialogRef.updateSize('90%', '');
            this.localGiveNameToMailingListDialogRef.updateSize('90%', '');
        }
    }




    openMessageDialog(templateRef) {
        this.localTextMessageDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
    }
}
