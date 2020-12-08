import { HttpClient } from '@angular/common/http';
import { ActionType } from './../../redux/action-type';
import { Action } from './../../redux/action';
import { ContactsService } from './../../services/contacts.service';
import { AfterViewInit, Component, HostListener, OnDestroy, OnInit, ViewChild, ElementRef, AfterContentInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ContactModel } from 'src/app/models/contact.model';
import { Unsubscribe } from 'redux';
import { Router } from '@angular/router';
import { store } from 'src/app/redux/store';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as _ from "lodash";

@Component({
    selector: 'app-contacts',
    templateUrl: './contacts.component.html',
    styleUrls: ['./contacts.component.scss'],
})
export class ContactsComponent implements OnInit, AfterContentInit, OnDestroy {

    public changeStr: string = "";
    public contactsToShow: ContactModel[];
    public contacts: ContactModel[];
    public contact: ContactModel;
    public newContact = new ContactModel();
    public contactName: string;
    public contactPhone: string;

    @ViewChild('UploadFileInput', { static: false }) uploadFileInput: ElementRef;
    fileUploadForm: FormGroup;
    fileInputLabel: string;

    

    public business = store.getState().business;

    private unsubscribe: Unsubscribe;

    localDialogRef;
    localDialogRefTwo;
    shouldCall = false;
    dialogWidth = '50%';
    dialogWidthTwo = '70%';

    dialogNo;

    constructor(
        private dialog: MatDialog,
        private dialogRef: MatDialogRef<any>,
        private contactsService: ContactsService,
        private router: Router,
        private formBuilder: FormBuilder,
        private http: HttpClient
    ) { }
    ngOnDestroy(): void {
        this.unsubscribe();
    }


    async ngOnInit() {

        this.fileUploadForm = this.formBuilder.group({
            myfile: ['']
        });

        this.unsubscribe = store.subscribe(() => {
            this.contacts = store.getState().contacts;
            this.contactsToShow = [...this.contacts]
        });

        if (store.getState().contacts.length > 0) {
            this.contacts = store.getState().contacts;
            this.contactsToShow = [...this.contacts]
        }
        else {
            setTimeout(async () => {
                try {
                    await this.contactsService.getAllContacts();

                }
                catch (err) {
                    alert("error: " + err.massage);
                }
            }, 100);
        }

        let wWidth = window.innerWidth;
        if (wWidth > 993) {
            this.dialogWidth = '50%';
            this.dialogWidthTwo = '70%';
        }
        if (wWidth > 1170 && wWidth < 1300) {
            this.dialogWidthTwo = '55%';
        }
        if (wWidth > 1300) {
            this.dialogWidthTwo = '45%';
        }
        if (wWidth >= 993 && wWidth <= 1170) {
            this.dialogWidthTwo = '70%';
        }
        if (wWidth < 993) {
            this.dialogWidth = '80%';
            this.dialogWidthTwo = '80%';
        }
        if (wWidth < 576) {
            this.dialogWidth = '90%';
            this.dialogWidthTwo = '90%';
        }
    }

    ngAfterContentInit() {
        this.shouldCall = true;
    }

    openDialog(templateRef) {
        this.localDialogRef = this.dialog.open(templateRef, {
            width: this.dialogWidth,
        });
        this.dialogNo = 'one';
    }

    openDialogTwo(templateRef) {
        this.localDialogRefTwo = this.dialog.open(templateRef, {
            width: this.dialogWidthTwo,
        });
        this.dialogNo = 'two';
    }

    onClose() {
        this.dialog.closeAll()
    }

    @HostListener('window:resize', ['$event'])
    onResize(event) {
        if (this.shouldCall) this.updateWidthOfDialogue(this.dialogNo);
    }

    updateWidthOfDialogue(dialogueNumber) {
        let wWidth = window.innerWidth;
        switch (dialogueNumber) {
            case 'one':
                if (wWidth > 993) {
                    this.localDialogRef.updateSize('50%', '');
                }
                if (wWidth < 993) {
                    this.localDialogRef.updateSize('80%', '');
                }
                if (wWidth < 576) {
                    this.localDialogRef.updateSize('90%', '');
                }
                break;
            case 'two':
                if (wWidth > 993) {
                    this.localDialogRefTwo.updateSize('70%', '');
                }
                if (wWidth > 1170 && wWidth < 1300) {
                    this.localDialogRefTwo.updateSize('55%', '');
                }
                if (wWidth > 1300) {
                    this.localDialogRefTwo.updateSize('45%', '');
                }
                if (wWidth >= 993 && wWidth <= 1170) {
                    this.localDialogRefTwo.updateSize('70%', '');
                }
                if (wWidth < 993) {
                    this.localDialogRefTwo.updateSize('80%', '');
                }
                if (wWidth < 576) {
                    this.localDialogRefTwo.updateSize('90%', '');
                }
                break;
            case 'three':
                break;
        }
    }

    public onFileSelect(event) {
        let af = ['application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'application/vnd.ms-excel']
        if (event.target.files.length > 0) {
            const file = event.target.files[0];

            if (!_.includes(af, file.type)) {
                alert('Only EXCEL Docs Allowed!');
            } else {
                this.fileInputLabel = file.name;
                this.fileUploadForm.get('myfile').setValue(file);
            }
        }
    }
    public async onFormSubmit() {

        if (!this.fileUploadForm.get('myfile').value) {
            alert('Please fill valid details!');
            return false;
        }

        const formData = new FormData();
        formData.append('contacts', this.fileUploadForm.get('myfile').value);

        // formData.append('agentId', '007');


        const success = await this.contactsService.LoadContactsViaFile(formData)

        if (success != null) {
            // Reset the file input
            this.uploadFileInput.nativeElement.value = "";
            this.fileInputLabel = undefined;
        }
        return;
    }



    search() {
        this.contactsToShow = this.contacts.filter(c =>
            c.contactPhone
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
            ||
            c.contactName
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
            ||
            c.contactEmail
                .toLocaleLowerCase()
                .includes(this.changeStr.toLocaleLowerCase())
        );
    }
    public getSingleContactByID(id: number) {
        this.contact = this.contacts.find(c => c.contactId === id);
    }
    public async AddContact() {
        try {

            this.newContact.businessId = this.business.businessId
            let addedContact: ContactModel = this.newContact;
            await this.contactsService.addContact(this.newContact);
            alert("Contact has been added")
            // this.router.navigateByUrl("/dashboard")
        }
        catch (err) { console.log(err.Message) }
    }
    public async deleteContact(contactID: number) {
        try {

            await this.contactsService.DeleteContact(contactID);
            this.contactsToShow = [...this.contacts]
        }
        catch (err) { console.log(err.Message) }
    }
    public async updateContact() {
        try {
            if (this.contact.contactName != null && this.contact.contactPhone != null) {
                await this.contactsService.UpdateContact(this.contact);
                alert("Contact has been updated.")
                this.router.navigateByUrl('dashboard/contacts');
            }
        }
        catch (err) { console.log(err) }
    }
    public getExampleFile() {
        alert("File Example")
    }
}
