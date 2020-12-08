using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tomedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    [Authorize]
    public class MessagesController : BaseController, IDisposable
    {
    
        private readonly MessagesLogic logic;

        public MessagesController(MessagesLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        [Route("{businessId}")]
        public IActionResult GetAllMessages(int businessId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                List<MessageModel> messages = logic.GetAllMessages(businessId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{businessId}/{messageId}")]
        public IActionResult GetSingleMessage(int businessId, int messageId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                MessageModel message = logic.GetSingleMessage(businessId, messageId);
                if (message == null)
                    return NotFound($"id {messageId} not found");
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("{businessId}")]
        public IActionResult AddMessage(int businessId, MessageModel messageModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                MessageModel addedMessage = logic.AddMessage(businessId, messageModel);
                return Created("api/messages/" + addedMessage.MessageId, addedMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{businessId}/{messageId}")]
        public IActionResult UpdateFullMessage(int businessId, int messageId, MessageModel messageModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                messageModel.MessageId = messageId;
                MessageModel updatedMessage = logic.UpdateFullMessage(businessId, messageModel);
                if (updatedMessage == null)
                    return NotFound($"id {messageId} not found");
                return Ok(updatedMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Route("{businessId}/{messageId}")]
        public IActionResult UpdatePartialMessage(int businessId, int messageId, MessageModel messageModel)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                messageModel.MessageId = messageId;
                MessageModel updatedMessage = logic.UpdatePartialMessage(businessId, messageModel);
                if (updatedMessage == null)
                    return NotFound($"id {messageId} not found");
                return Ok(updatedMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{businessId}/{messageId}")]
        public IActionResult DeleteMessage(int businessId, int messageId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                logic.DeleteMessage(businessId, messageId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("mailinglists/{budinessId}/{mailingListId}")]
        public IActionResult GetAllMessagesByMailingListId(int businessId, int mailingListId)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                return Ok(logic.GetAllMessagesByMailingListId(businessId, mailingListId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{businessId}/{text}")]
        public IActionResult GetAllMessagesContainText(int businessId, string text)
        {
            try
            {
                if (!logic.isBusinessAuthorized(businessId, Request))
                    return BadRequest("you are not authorized");

                return Ok(logic.GetAllMessagesContainText(businessId, text));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       
        [HttpPost]
        [Route("scheduled-message-to-mailinglist")]
        public async Task<IActionResult> SendMessageToMailingList2(ScheduledMessageToMailingListModel scheduledMessage)
        {
            try
            {
                if (!logic.isBusinessAuthorized(scheduledMessage.BusinessId, Request))
                    return BadRequest("you are not authorized");

                await logic.SendMessageToSeveralMailingLists(scheduledMessage);
                return Ok("Message was sent");
            }
            catch (Exception ex)
            {
                return (OkObjectResult)StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("send_simple-message")]
        public IActionResult SendSimpleMessage(SimpleMessageModel simpleMessageModel)
        {
            try
            {
                var returnedData = logic.sendSimpleMessage(simpleMessageModel);
                return Ok("Message was sent");
            }
            catch (Exception ex)
            {
                return (OkObjectResult)StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        void IDisposable.Dispose()
        {
            logic.Dispose();
        }
    }
}
