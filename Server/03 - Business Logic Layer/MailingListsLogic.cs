using System.Collections.Generic;
using System.Linq;

namespace Tomedia
{
    public class MailingListsLogic: BaseLogic
    {
        public MailingListsLogic(AutoWhatsappContext db) : base(db) { }

        public List<MailingListModel> GetAllMailingLists(int businessId)
        {
            return DB.MailingLists.Where(p=>p.BusinessId==businessId).Select(p => new MailingListModel(p)).ToList();
        }

        public MailingListModel AddNewMailingList(MailingListModel mailingListModel)
        {
            MailingList mailingList= mailingListModel.ConvertToMailingList();
            DB.MailingLists.Add(mailingList);
            DB.SaveChanges();
            mailingListModel.MailingListId = mailingList.MailingListId;

            return mailingListModel;
        }

        public void DeleteMailingList(int businessId, int mailingListId)
        {
            MailingList MailingListToDelete = DB.MailingLists.SingleOrDefault(p => p.MailingListId == mailingListId && p.BusinessId == businessId);
            if (MailingListToDelete == null)
                return;
            DB.MailingLists.Remove(MailingListToDelete);
            DB.SaveChanges();
        }

        public List<ContactModel> GetContactsByMailingList(int businessId, int mailingListId)
        {

            List<ContactModel> contacts = new List<ContactModel>();

            List<MailingListsContactModel> mailingListsContactModels = DB.MailingListsContacts
                .Where(p=>p.MailingListId==mailingListId)
                .Select(p => new MailingListsContactModel(p))
                .ToList();
            
            foreach (var item in mailingListsContactModels)
            {
                ContactModel contactToAdd = new ContactModel(DB.Contacts.SingleOrDefault(p=>p.ContactId==item.ContactId));

                if (contactToAdd.BusinessId == businessId)
                    contacts.Add(contactToAdd);
            }
            return contacts;
        }

        public List<ContactModel> AddContactsToMailingList(int businessId, int mailingListId, List<int> contactIds)
        {
            List<ContactModel> contacts = DB.Contacts.Where(p=>contactIds.Contains(p.ContactId)).Select(p=>new ContactModel(p)).ToList();
            foreach (var item in contacts)
            {
                if (item.BusinessId == businessId)
                {
                    DB.MailingListsContacts.Add(new MailingListsContact
                    {
                        MailingListId = mailingListId,
                        ContactId = item.ContactId
                    });
                }
            }
            //foreach (var item in contactIds)
            //{
            //    DB.MailingListsContacts.Add(new MailingListsContact
            //    {
            //        MailingListId = mailingListId,
            //        ContactId = item
            //    });
            //}
            DB.SaveChanges();

            return this.GetContactsByMailingList(businessId, mailingListId);
        }




    }
}
