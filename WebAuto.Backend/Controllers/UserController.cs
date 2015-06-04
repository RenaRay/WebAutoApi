using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuto.Backend.Extensions;
using WebAuto.Backend.Models;
using WebAuto.Backend.Properties;
using WebAuto.Backend.Security;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IHashAlgorithm _hashAlgorithm;

        public UserController(
            IUserDataAccess userDataAccess,
            IHashAlgorithm hashAlgorithm)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }
            _userDataAccess = userDataAccess;
            _hashAlgorithm = hashAlgorithm;
        }

        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _userDataAccess.FindByLoginAsync(model.Login);
            if (existing != null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    string.Format(Resources.ValidationLoginIsAlreadyInUse, model.Login));
                return BadRequest(ModelState);
            }

            var userCreateContract =
                new User
                {
                    Login = model.Login,
                    PasswordHash = _hashAlgorithm.Hash(model.Password)
                };
            await _userDataAccess.CreateAsync(userCreateContract);

            return Ok();
        }

        [Authorize]
        [Route("find")]
        [HttpGet]
        public async Task<IHttpActionResult> Find(string plate, bool matchExact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<User> users;
            if (matchExact)
            {
                users = await _userDataAccess.FindByPlateExactAsync(plate);
            }
            else
            {
                users = await _userDataAccess.FindByPlatePartAsync(plate, Settings.Default.UserFindByPlateLimit);
            }
            var currentUserLogin = User.Identity.Name;
            List<ConversationMemberModel> conversationMembers = users
                .Select(u => new ConversationMemberModel
                {
                    User = new UserModel
                    {
                        Login = u.Login,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Avatar = u.Avatar
                    },
                    Car = u.Cars.FirstOrDefault(c => c.Plate.Contains(plate)).ToModel()
                })
                .ToList();

            return Ok(conversationMembers);
        }
    }
}