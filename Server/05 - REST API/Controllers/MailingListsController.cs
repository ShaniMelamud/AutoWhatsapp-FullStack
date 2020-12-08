using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Tomedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    [Authorize]
    public class MailingListsController : BaseController, IDisposable
    {
        private readonly MailingListsLogic logic;

        public MailingListsController(MailingListsLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        [Route("{businessId}")]
        public IActionResult GetAllMailingLists(int businessId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                return Ok(logic.GetAllMailingLists(businessId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{businessId}/{mailingListId}")]
        public IActionResult GetMailingListContactsById(int businessId, int mailingListId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                return Ok(logic.GetContactsByMailingList(businessId, mailingListId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddMailingList(MailingListModel mailingListModel)
        {
            try
            {
                return Ok(logic.AddNewMailingList(mailingListModel));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPost]
        [Route("{businessId}/{mailingListId}")]
        public IActionResult AddContactsToMailingList(int businessId, int mailingListId, List<int> cnontacts)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                return Ok(logic.AddContactsToMailingList(businessId, mailingListId, cnontacts));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{businessId}/{mailingListId}")]
        public IActionResult deleteMailingList(int businessId, int mailingListId)
        {
            if (!logic.isBusinessAuthorized(businessId, Request))
                return BadRequest("you are not authorized");

            logic.DeleteMailingList(businessId, mailingListId);
            return NoContent();
        }

        public void Dispose()
        {
            logic.Dispose();
        }
    }
}
