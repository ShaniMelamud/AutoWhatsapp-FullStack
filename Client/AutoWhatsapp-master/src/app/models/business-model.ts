export class BusinessModel {
    public constructor(
        public businessId?: number,
        public businessName?: string,
        public businessType?: string,
        public businessPhone?: string,
        public businessEmail?: string,
        public customerName?: string,
        public password?: string,
        public jwtToken?: string,
        public userName?: string,
        public role?: string
    ){}
}
