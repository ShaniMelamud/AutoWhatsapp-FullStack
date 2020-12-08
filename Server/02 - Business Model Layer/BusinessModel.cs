using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class BusinessModel: ICloneable
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string CustomerName { get; set; }
        public string ContactsBookFileName { get; set; }
        public string Password { get; set; }
        public string JwtToken { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public BusinessModel() { }

        public BusinessModel(Business business)
        {
            BusinessId = business.BusinessId;
            BusinessName = business.BusinessName;
            BusinessType = business.BusinessType;
            BusinessPhone = business.BusinessPhone;
            BusinessEmail = business.BusinessEmail;
            CustomerName = business.CustomerName;
            ContactsBookFileName = business.ContactsBookFileName;
            Password = business.Password;
            Username = business.Username;
            Role = business.Role;
        }

        public Business ConvertToBusiness()
        {
            return new Business
            {
                BusinessId = BusinessId,
                BusinessName = BusinessName,
                BusinessType = BusinessType,
                BusinessPhone = BusinessPhone,
                BusinessEmail = BusinessEmail,
                CustomerName = CustomerName,
                ContactsBookFileName = ContactsBookFileName,
                Username = Username,
                Password = Password,
                Role = Role

            };
        }

        public object Clone()
        {
            return new BusinessModel
            {
                BusinessId = BusinessId,
                BusinessName = BusinessName,
                BusinessType = BusinessType,
                BusinessPhone = BusinessPhone,
                BusinessEmail = BusinessEmail,
                CustomerName = CustomerName,
                ContactsBookFileName = ContactsBookFileName,
                JwtToken = JwtToken,
                Password = Password,
                Role = Role

            };
        }
    }
}
