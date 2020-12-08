using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;

namespace Tomedia
{
    public class ContactsLogic : BaseLogic
    {
        public ContactsLogic(AutoWhatsappContext db) : base(db) { }

        public List<ContactModel> GetAllContacts(int businessId)
        {
            return DB.Contacts.Where(p=>p.BusinessId==businessId).Select(p => new ContactModel(p)).ToList();
        }

        public ContactModel GetSingleContact(int businessId, int contactId)
        {
            return DB.Contacts.Where(p => p.ContactId == contactId && p.BusinessId == businessId).Select(p => new ContactModel(p)).SingleOrDefault();
        }

        public ContactModel AddContact(int businessId, ContactModel contactModel)
        {
            if (contactModel.BusinessId != businessId)
                return null;
            Contact contact = contactModel.ConvertToContact();
            DB.Contacts.Add(contact);
            DB.SaveChanges();
            contactModel.ContactId = contact.ContactId;
            return contactModel;
        }

        public ContactModel UpdateFullContact(int businessId, ContactModel contactModel)
        {
            Contact contact = DB.Contacts.SingleOrDefault(p => p.ContactId == contactModel.ContactId);
            if (contact == null || contact.BusinessId != businessId)
                return null;
            
            contact.BusinessId = contactModel.BusinessId;
            contact.ContactName = contactModel.ContactName;
            contact.ContactPhone = contactModel.ContactPhone;
            contact.ContactEmail = contactModel.ContactEmail;
            DB.SaveChanges();
            return contactModel;
        }

        public ContactModel UpdatePartialContact(int businessId, ContactModel contactModel)
        {
            Contact contact = DB.Contacts.SingleOrDefault(p => p.ContactId == contactModel.ContactId && p.BusinessId==businessId);
            if (contact == null)
                return null;
            
            if(contact.ContactName!=null)
                contact.ContactName = contactModel.ContactName;
            
            if(contact.ContactPhone!=null)
                contact.ContactPhone = contactModel.ContactPhone;
            
            if(contact.ContactEmail!=null)
            contact.ContactEmail = contactModel.ContactEmail;
            
            DB.SaveChanges();
            return contactModel;
        }

        public void DeleteContact(int businessId, int contactId)
        {
            Contact ContactToDelete = DB.Contacts.SingleOrDefault(p => p.ContactId == contactId && p.BusinessId == businessId);
            if (ContactToDelete == null)
                return;
            DB.Contacts.Remove(ContactToDelete);
            DB.SaveChanges();
        }

        public List<ContactModel> LoadContactsFromXlsxFilePath(int businessId, string inputFilePath)
        {
            ExcelDataHandler excelDataHandler = new ExcelDataHandler();
            //string outputFile = @"C:\Users\user\OneDrive\Programming studies\Tomedia Web Team\AutoWhatsapp\03 - Business Logic Layer\CsvFiles\1.josn";
            return ExcelDataHandler.UsingExcelDataReader(businessId, inputFilePath);
        }

        public List<ContactModel> LoadContactsFromXlsxFile(int businessId, IFormFile contactsFile)
        {
            string extension = Path.GetExtension(contactsFile.FileName);
            string contactsBookFileName = Guid.NewGuid() + extension;
            
            BusinessModel businessToAddContactsBook = new BusinessModel(DB.Businesses.SingleOrDefault(p => p.BusinessId == businessId));
            businessToAddContactsBook.ContactsBookFileName = contactsBookFileName;

            using (FileStream fileStream = File.Create("ExcelFiles/" + contactsBookFileName))
            {
                contactsFile.CopyTo(fileStream);
            }

            ExcelDataHandler excelDataHandler = new ExcelDataHandler();
            List<ContactModel> contacts = ExcelDataHandler.UsingExcelDataReader(businessId, "ExcelFiles/" + contactsBookFileName);
            foreach (var item in contacts)
            {
                if (item.ContactPhone.Contains(":::"))
                {
                    int sliceIndex = item.ContactPhone.IndexOf(":");
                    item.ContactPhone = item.ContactPhone.Substring(0, sliceIndex);
                }
                DB.Contacts.Add(item.ConvertToContact());
            }
            DB.SaveChanges();
            return contacts;
        }
    }
}