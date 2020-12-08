export class ScheduledMessageToMailingList {
    public constructor(
        public datetime?: Date,
        public mailingListIds?: number[],
        public messageId?: number,
        public businessId?: number
    ){}
}