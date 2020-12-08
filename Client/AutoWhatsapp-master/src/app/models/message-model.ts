export class MessageModel {
    public constructor(
        public messageId?: number,
        public messageContent?: string,
        public businessId?: number,
        public filePath?: string
    ){}

}
