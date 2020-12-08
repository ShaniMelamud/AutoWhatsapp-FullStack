using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tomedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper jwtHelper;
        private readonly BusinessesLogic logic;

        public AuthController(JwtHelper jwtHelper, BusinessesLogic logic)
        {
            this.jwtHelper = jwtHelper;
            this.logic = logic;
        }

        [HttpPost]
        public IActionResult Register(BusinessModel businessModel)
        {
            try
            {
                if (logic.IsUserNameExists(businessModel.Username))
                    return BadRequest("Customer name already taken");

                BusinessModel addedBusiness = logic.AddBusiness(businessModel);

                businessModel.JwtToken = jwtHelper.GetJwtToken(businessModel.Username, businessModel.Role, Convert.ToString(businessModel.BusinessId));

                businessModel = (BusinessModel)businessModel.Clone();
                businessModel.Password = null;

                return Created("api/businesses/" + addedBusiness.BusinessId, addedBusiness);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(CredentialsModel credentials)
        {
            try
            {
                BusinessModel businessModel = logic.GetBusinessByCredentials(credentials);

                if (businessModel == null)
                    return Unauthorized("Incorrect username or password");

                businessModel.JwtToken = jwtHelper.GetJwtToken(businessModel.Username, businessModel.Role, Convert.ToString(businessModel.BusinessId));

                businessModel = (BusinessModel)businessModel.Clone();
                businessModel.Password = null;

                return Ok(businessModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
