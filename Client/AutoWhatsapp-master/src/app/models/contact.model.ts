export class ContactModel {
    public constructor(
        public contactId?: number,
        public businessId?: number,
        public contactName?: string,
        public contactPhone?: string,
        public contactEmail?: string
    ) { }
}