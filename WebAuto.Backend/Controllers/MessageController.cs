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
        private readonly IMessageDataAccess _messageDataAccess;

        public MessageController(
            IUserDataAccess userDataAccess,
            IMessageDataAccess messageDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (messageDataAccess == null)
            {
                throw new ArgumentNullException("messageDataAccess");
            }
            _userDataAccess = userDataAccess;
            _messageDataAccess = messageDataAccess;
        }

        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Send(SendMessageModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userDataAccess.FindByLoginAsync(User.Identity.Name);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var toUser = await _userDataAccess.FindByPlateExactAsync(model.ToPlate);

            var message =
                new Message
                {
                    FromUserId = currentUser.Id,
                    Icons = new List<int>(),
                    IsLiked = false,
                    IsRead = false,
                    Sent = DateTime.UtcNow,
                    Text = model.Text,
                    ToPlate = model.ToPlate,
                    ToUserId = toUser != null ? toUser.Id : (int?)null
                };
            await _messageDataAccess.CreateAsync(message);

            return Ok();
        }

        [Authorize]
        [Route("unread")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnreadCount()
        {
            var currentUserLogin = User.Identity.Name;
            var user = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (user == null)
            {
                return NotFound();
            }

            var count = await _messageDataAccess.GetUnreadCount(user.Id);

            return Ok(new { count });
        }

        [Authorize]
        [Route("inbox")]
        [HttpGet]
        public async Task<IHttpActionResult> Inbox()
        {
            var currentUserLogin = User.Identity.Name;
            var user = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (user == null)
            {
                return NotFound();
            }

            var messages = await _messageDataAccess.GetInboxMessages(user.Id);
            var messageModels = messages
                .Select(m => new InboxMessageModel
                {
                    Sent = m.Sent,
                    Text = m.Text
                })
                .ToList();

            return Ok(messageModels);
        }

        [Authorize]
        [Route("read")]
        [HttpPost]
        public async Task<IHttpActionResult> Read()
        {
            var currentUserLogin = User.Identity.Name;
            var user = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (user == null)
            {
                return NotFound();
            }

            await _messageDataAccess.ReadInboxMessages(user.Id);

            return Ok();
        }
    }
}