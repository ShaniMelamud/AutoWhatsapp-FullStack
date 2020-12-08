using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tomedia
{
    public class ExcelDataHandler
    {
        public static List<ContactModel> UsingExcelDataReader(int businessId, string inputFile)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var fileName = inputFile;
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each ROW
                    {

                        ContactModel contactModel = new ContactModel();
                        contactModel.BusinessId = businessId;
                        if (string.IsNullOrEmpty(reader.GetString(0)))
                            contactModel.ContactName = "";
                        else
                            contactModel.ContactName = reader.GetValue(0).ToString();

                        if (string.IsNullOrEmpty(reader.GetString(1)))
                            contactModel.ContactPhone = "";
                        else
                            contactModel.ContactPhone = reader.GetValue(1).ToString();

                        if (string.IsNullOrEmpty(reader.GetString(2)))
                            contactModel.ContactEmail = "";
                        else
                            contactModel.ContactEmail = reader.GetValue(2).ToString();
                        //{
                        //    ContactName = reader.GetValue(0).ToString(),
                        //    ContactEmail = reader.GetValue(1).ToString(),
                        //    ContactPhone = reader.GetValue(2).ToString()
                        //};
                        contacts.Add(contactModel);
                    }
                }
            }
            return contacts;
        }

        public static List<ContactModel> UsingExcelDataReader2(IFormFile uploadedFile)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            var fileName = uploadedFile.FileName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each ROW
                    {

                        ContactModel contactModel = new ContactModel();
                        if (string.IsNullOrEmpty(reader.GetString(0)))
                            contactModel.ContactName = "";
                        else
                            contactModel.ContactName = reader.GetValue(0).ToString();

                        if (string.IsNullOrEmpty(reader.GetString(1)))
                            contactModel.ContactPhone = "";
                        else
                            contactModel.ContactPhone = reader.GetValue(1).ToString();

                        if (string.IsNullOrEmpty(reader.GetString(2)))
                            contactModel.ContactEmail = "";
                        else
                            contactModel.ContactEmail = reader.GetValue(2).ToString();
                        //{
                        //    ContactName = reader.GetValue(0).ToString(),
                        //    ContactEmail = reader.GetValue(1).ToString(),
                        //    ContactPhone = reader.GetValue(2).ToString()
                        //};
                        contacts.Add(contactModel);
                    }
                }
            }
            return contacts;
        }
    }
}
