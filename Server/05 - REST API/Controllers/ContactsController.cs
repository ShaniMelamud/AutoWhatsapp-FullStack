using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nancy.Json;
using Newtonsoft.Json.Linq;

namespace Tomedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    [Authorize]
    public class ContactsController : BaseController, IDisposable
    {
        private readonly ContactsLogic logic;
        public ContactsController(ContactsLogic logic)
        {
            this.logic = logic;
        }

        


        [HttpGet]
        [Route("{businessId}")]
        public IActionResult GetAllContacts(int businessId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                List<ContactModel> contacts = logic.GetAllContacts(businessId);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{businessId}/{contactId}")]
        public IActionResult GetSingleContact(int businessId, int contactId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                ContactModel contact = logic.GetSingleContact(businessId, contactId);
                if (contact == null)
                    return NotFound($"id {contactId} not found");
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("{businessId}")]
        public IActionResult AddContact(int businessId, ContactModel contactModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                ContactModel addedContact = logic.AddContact(businessId, contactModel);
                return Created("api/contacts/" + addedContact.ContactId, addedContact);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{businessId}/{contactId}")]
        public IActionResult UpdateFullContact(int businessId, int contactId, ContactModel contactModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                contactModel.ContactId = contactId;
                ContactModel updatedContact = logic.UpdateFullContact(businessId, contactModel);
                if (updatedContact == null)
                    return NotFound($"contactId {contactId} not found");
                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Route("{businessId}/{contactId}")]
        public IActionResult UpdatePartialContact(int businessId, int contactId, ContactModel contactModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                ContactModel updatedContact = logic.UpdatePartialContact(businessId, contactModel);
                if (updatedContact == null)
                    return NotFound($"contactId {contactId} not found");
                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{businessId}/{contactId}")]
        public IActionResult DeleteContact(int businessId, int contactId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                logic.DeleteContact(businessId, contactId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-xls-file-path/{businessId}")]
        public IActionResult LoadContactsFromXlsxFilePath(int businessId, UploadedFilePathModel uploadedFilePathModel)
        {
            if (!logic.isBusinessAuthorized(businessId, Request))
                return BadRequest("you are not authorized");

            return Ok(logic.LoadContactsFromXlsxFilePath(businessId, uploadedFilePathModel.FilePath));
        }

        [HttpPost]
        [Route("upload-xls-file/{businessId}")]
        public IActionResult LoadContactsFromXlsxFile(int businessId, [FromForm]IFormFile contacts)
        {
            if (!logic.isBusinessAuthorized(businessId, Request))
                return BadRequest("you are not authorized");

            return Ok(logic.LoadContactsFromXlsxFile(businessId, contacts));
        }

        public void Dispose()
        {
            logic.Dispose();
        }
    }
}
