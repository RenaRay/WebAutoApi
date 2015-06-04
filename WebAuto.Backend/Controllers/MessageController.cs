using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAuto.Backend.Models;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Controllers
{
    [RoutePrefix("messages")]
    public class MessageController : ApiController
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IConversationDataAccess _conversationDataAccess;

        public MessageController(
            IUserDataAccess userDataAccess,
            IConversationDataAccess conversationDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (conversationDataAccess == null)
            {
                throw new ArgumentNullException("conversationDataAccess");
            }
            _userDataAccess = userDataAccess;
            _conversationDataAccess = conversationDataAccess;
        }

        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(PostMessageModel model)
        {
            var conversation = await _conversationDataAccess.FindByIdAsync(model.ConversationId);
            if (conversation == null)
            {
                return NotFound();
            }

            var currentUserLogin = User.Identity.Name;
            var currentUser = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (!conversation.Members.Any(m => m.User == currentUser.Id))
            {
                return Unauthorized();
            }

            var conversationMessage =
                new ConversationMessage
                {
                    Author = currentUser.Id,
                    Posted = DateTime.UtcNow,
                    Text = model.Message
                };

            await _conversationDataAccess.PostMessageAsync(
                model.ConversationId, conversationMessage);
            return Ok();
        }

        [Authorize]
        [Route("unread")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnreadCount()
        {
            var currentUserLogin = User.Identity.Name;
            var currentUser = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var count = await _conversationDataAccess.GetTotalUnreadCountAsync(currentUser.Id);

            return Ok(new {count});
        }

        [Authorize]
        [Route("read")]
        [HttpPost]
        public async Task<IHttpActionResult> Read(ReadMessagesModel model)
        {
            var currentUserLogin = User.Identity.Name;
            var currentUser = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            await _conversationDataAccess.ReadMessages(model.ConversationId, currentUser.Id, model.MessageCount);

            return Ok();
        }
    }
}