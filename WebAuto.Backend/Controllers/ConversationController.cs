using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuto.Backend.Extensions;
using WebAuto.Backend.Models;
using WebAuto.Backend.Properties;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Controllers
{
    [RoutePrefix("conversation")]
    public class ConversationController : ApiController
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IConversationDataAccess _conversationDataAccess;

        public ConversationController(
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
        public async Task<IHttpActionResult> New(NewConversationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserLogin = User.Identity.Name;
            var currentUser = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (currentUser == null)
            {
                return Unauthorized();
            }
            if (!currentUser.Cars.Any(c => c.Plate == model.FromPlate))
            {
                return BadRequest();
            }
            var toUser = string.IsNullOrEmpty(model.ToUser)
                ? (await _userDataAccess.FindByPlateExactAsync(model.ToPlate)).FirstOrDefault()
                : await _userDataAccess.FindByLoginAsync(model.ToUser);
            if (toUser != null && !toUser.Cars.Any(c => c.Plate == model.ToPlate))
            {
                return BadRequest();
            }

            var from =
                new ConversationMember
                    {
                        User = currentUser.Id,
                        Plate = model.FromPlate.ToUpper()
                    };
            var to =
                new ConversationMember
                    {
                        User = toUser != null ? toUser.Id : null,
                        Plate = model.ToPlate.ToUpper()
                    };
            var members =
                new List<ConversationMember>
                {
                    from,
                    to
                };

            var conversation = await _conversationDataAccess.FindByMembersAsync(members);
            if (conversation == null)
            {
                conversation =
                    new Conversation
                    {
                        Members = members,
                        Messages = new List<ConversationMessage>()
                    };
                await _conversationDataAccess.CreateAsync(conversation);
            }

            var conversationMessage =
                new ConversationMessage
                {
                    Author = from.User,
                    Posted = DateTime.UtcNow,
                    Text = model.Message
                };
            await _conversationDataAccess.PostMessageAsync(
                    conversation.Id,
                    conversationMessage);
            return Ok(new { conversation.Id });
        }

        [Authorize]
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var currentUserLogin = User.Identity.Name;
            var currentUser = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var conversations = await _conversationDataAccess.FindByUserAsync(currentUser.Id);

            var userIds = conversations
                .SelectMany(c => c.Members)
                .Select(m => m.User)
                .Where(u => !string.IsNullOrEmpty(u))
                .Distinct()
                .ToArray();
            var users = await _userDataAccess.FindByIdsAsync(userIds);
            var usersByIds = users.ToDictionary(u => u.Id);

            var models = conversations
                .Select(c => new ConversationListItemModel
                {
                    Id = c.Id,
                    Members = c.Members
                        .Select(m => GetConversationMemberModel(m, usersByIds))
                        .ToList(),
                    UnreadCount = c.Members.FirstOrDefault(m => m.User == currentUser.Id).UnreadCount
                });

            return Ok(models);
        }

        private ConversationMemberModel GetConversationMemberModel(ConversationMember conversationMember, IDictionary<string, DataAccess.User> usersByIds)
        {
            User user = null;
            if (!string.IsNullOrEmpty(conversationMember.User))
            {
                usersByIds.TryGetValue(conversationMember.User, out user);
            }
            var memberCar = user != null ? user.Cars.FirstOrDefault(car => car.Plate == conversationMember.Plate) : null;
            return
                new ConversationMemberModel
                {
                    User = user != null
                        ? new UserModel
                            {
                                Avatar = user.Avatar,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Login = user.Login
                            }
                        : new UserModel
                            {
                                Login = string.Empty
                            },
                    Car = new CarModel
                    {
                        Plate = conversationMember.Plate,
                        Vendor = memberCar != null ? memberCar.Vendor : null,
                        Model = memberCar != null ? memberCar.Model : null
                    }
                };
        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("id was expected");
            }

            var conversation = await _conversationDataAccess.FindByIdAsync(id);
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

            var userIds = conversation.Members
                .Select(m => m.User)
                .Where(u => !string.IsNullOrEmpty(u))
                .ToArray();
            var users = await _userDataAccess.FindByIdsAsync(userIds);
            var usersByIds = users.ToDictionary(u => u.Id);

            return Ok(
                new ConversationModel
                {
                    Members = conversation.Members
                        .Select(m => GetConversationMemberModel(m, usersByIds))
                        .ToList(),
                    Messages = conversation.Messages
                        .Select(m =>
                            new ConversationMessageModel
                            {
                                Author = usersByIds[m.Author].Login,
                                Posted = m.Posted,
                                Text = m.Text
                            })
                        .ToList(),
                    UnreadCount = conversation.Members.First(m => m.User == currentUser.Id).UnreadCount
                });
        }
    }
}