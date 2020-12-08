using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tomedia.Controllers;

namespace Tomedia
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    [Authorize]
    public class BusinessesController : BaseController, IDisposable
    {
        private readonly BusinessesLogic logic;
        private readonly JwtHelper jwtHelper;

        public BusinessesController(JwtHelper jwtHelper, BusinessesLogic logic)
        {
            this.logic = logic;
            this.jwtHelper = jwtHelper;
        }

        [HttpGet]
        public IActionResult GetAllBusinesses()
        {
            try
            {
                List<BusinessModel> businesses = logic.GetAllBusinesses();
                return Ok(businesses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleBusiness(int id)
        {
            try
            {
                BusinessModel business = logic.GetSingleBusiness(id);
                if (business == null)
                    return NotFound($"id {id} not found");
                return Ok(business);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBusiness(BusinessModel businessModel)
        {
            try
            {
                if(logic.IsUserNameExists(businessModel.Username))
                    return BadRequest("Customer name already taken");

                if (logic.IsEmailExists(businessModel.BusinessEmail))
                    return BadRequest("This email is associated with another account");

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

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateFullBusiness(int id, BusinessModel businessModel)
        {
            try
            {
                businessModel.BusinessId = id;
                BusinessModel updatedBusiness = logic.UpdateFullBusiness(businessModel);
                if (updatedBusiness == null)
                    return NotFound($"id {id} not found");
                return Ok(updatedBusiness);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdatePartialBusiness(int id, BusinessModel businessModel)
        {
            try
            {
                businessModel.BusinessId = id;
                BusinessModel updatedBusiness = logic.UpdatePartialBusiness(businessModel);
                if (updatedBusiness == null)
                    return NotFound($"id {id} not found");
                return Ok(updatedBusiness);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteBusiness(int id)
        {
            try
            {
                logic.DeleteBusiness(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        ////////////////////////////////////////////////////////////////////////////////
        //[HttpGet]
        //[Route("collection")]
        //public IActionResult GetCollection()
        //{
        //    NameValueCollection nameValueCollection = new NameValueCollection();
        //    nameValueCollection.Add("message", "435213452");
        //    nameValueCollection.Add("message", "65262");
        //    nameValueCollection.Add("message", "56265262");
        //    nameValueCollection.Add("message", "66626234");
        //    nameValueCollection.Add("message", "43521353245252452");
        //    nameValueCollection.Add("message", "846876");
        //    nameValueCollection.Add("message", "7654743");
        //    nameValueCollection.Add("message", "65433");
        //    nameValueCollection.Add("message", "4352458747895613452");
        //    nameValueCollection.Add("message", "4352262613452");

        //    return Ok( nameValueCollection );
        //}
        ////////////////////////////////////////////////////////////////////////////////


        public void Dispose()
        {
            logic.Dispose();
        }
    }
}
