using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Tomedia
{
    public abstract class BaseLogic : IDisposable
    {
        protected readonly AutoWhatsappContext DB;

        public BaseLogic(AutoWhatsappContext db)
        {
            DB = db;
        }

        public BusinessModel GetBusinessByUsername(string username)
        {
            return new BusinessModel(DB.Businesses.SingleOrDefault(p => p.Username == username));
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        public bool isBusinessAuthorized(int businessId, HttpRequest request)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = request.Headers["authorization"].ToString().Substring(7);

            //Read Token for Getting the User Details
            var parsedJwt = tokenHandler.ReadToken(token);
            string str = parsedJwt.ToString();
            return (str.Contains("\"businessId\":\"" + businessId));
        }
    }
}
