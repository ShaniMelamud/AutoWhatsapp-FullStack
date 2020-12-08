using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomedia
{
    public class BusinessesLogic: BaseLogic
    {

        public BusinessesLogic(AutoWhatsappContext db):base(db) { }

        public bool IsUserNameExists(string userName)
        {
            return DB.Businesses.Any(p => p.Username == userName);
        }

        public bool IsEmailExists(string email)
        {
            return DB.Businesses.Any(p => p.BusinessEmail == email);
        }

        public List<BusinessModel> GetAllBusinesses()
        {
            return DB.Businesses.Select(p => new BusinessModel(p)).ToList();
        } 

        public BusinessModel GetSingleBusiness(int id)
        {
            return DB.Businesses.Where(p => p.BusinessId == id).Select(p => new BusinessModel(p)).SingleOrDefault();
        }

        public BusinessModel AddBusiness(BusinessModel businessModel)
        {
            Business business = businessModel.ConvertToBusiness();
            DB.Businesses.Add(business);
            DB.SaveChanges();
            businessModel.BusinessId = business.BusinessId;
            return businessModel;
        }

        public BusinessModel UpdateFullBusiness(BusinessModel businessModel)
        {
            Business business = DB.Businesses.SingleOrDefault(p => p.BusinessId == businessModel.BusinessId);
            if (business == null)
                return null;
            business.BusinessName = businessModel.BusinessName;
            business.BusinessType = businessModel.BusinessType;
            business.BusinessPhone = businessModel.BusinessPhone;
            business.BusinessEmail = businessModel.BusinessEmail;
            business.CustomerName = businessModel.CustomerName;
            business.Role = business.Role;
            business.Username = DB.Businesses.SingleOrDefault(p=>p.BusinessId == businessModel.BusinessId).Username;
            business.Password = DB.Businesses.SingleOrDefault(p=>p.BusinessId == businessModel.BusinessId).Password;
            DB.SaveChanges();
            return businessModel;
        }

        public BusinessModel UpdatePartialBusiness(BusinessModel businessModel)
        {
            Business business = DB.Businesses.SingleOrDefault(p => p.BusinessId == businessModel.BusinessId);
            if (business == null)
                return null;

            if (business.BusinessName != null)
                business.BusinessName = businessModel.BusinessName;

            if (business.BusinessType != null)
                business.BusinessType = businessModel.BusinessType;

            if (business.BusinessPhone != null)
                business.BusinessPhone = businessModel.BusinessPhone;

            if (business.BusinessEmail != null)
                business.BusinessEmail = businessModel.BusinessEmail;

            if (business.CustomerName != null)
                business.CustomerName = businessModel.CustomerName;

            if (business.Username != null)
                business.Username = businessModel.Username;

            if (business.Password != null)
                business.Password = businessModel.Password;

            if (business.Role != null)
                business.Role = businessModel.Role;

            DB.SaveChanges();
            return businessModel;
        }

        public void DeleteBusiness(int id)
        {
            Business BusinessToDelete = DB.Businesses.SingleOrDefault(p => p.BusinessId == id);
            if (BusinessToDelete == null)
                return;
            DB.Businesses.Remove(BusinessToDelete);
            DB.SaveChanges();
        }

        public BusinessModel GetBusinessByCredentials(CredentialsModel credentials)
        {
            return new BusinessModel(DB.Businesses.SingleOrDefault(p => p.Username == credentials.Username && p.Password == credentials.Password));
        }
    }
}
