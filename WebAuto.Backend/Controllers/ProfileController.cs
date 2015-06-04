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
    [RoutePrefix("profile")]
    public class ProfileController : ApiController
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IAvatarDataAccess _avatarDataAccess;
        private readonly IConversationDataAccess _conversationDataAccess;

        public ProfileController(
            IUserDataAccess userDataAccess,
            IAvatarDataAccess avatarDataAccess,
            IConversationDataAccess conversationDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (avatarDataAccess == null)
            {
                throw new ArgumentNullException("avatarDataAccess");
            }
            if (conversationDataAccess == null)
            {
                throw new ArgumentNullException("conversationDataAccess");
            }
            _userDataAccess = userDataAccess;
            _avatarDataAccess = avatarDataAccess;
            _conversationDataAccess = conversationDataAccess;
        }
        
        //http://localhost/api/profile/test
        [Authorize]
        [Route("{login}")]
        public async Task<IHttpActionResult> Get(string login)
        {
            var user = await _userDataAccess.FindByLoginAsync(login);
            if (user == null)
            {
                return NotFound();
            }
            var currentUserLogin = User.Identity.Name;
            UserProfileModel profile = user.ToUserProfileModel(currentUserLogin);
            return Ok(profile);
        }

        [Authorize]
        public async Task<IHttpActionResult> Post(UserProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserLogin = User.Identity.Name;
            var user = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (user == null)
            {
                return NotFound();
            }
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.ContactsVisibleTo = model.ContactsVisibleTo;
            user.Avatar = model.Avatar;
            var cars = model.Cars ?? Enumerable.Empty<CarModel>();
            user.Cars = cars.Select(car => car.ToDataContract()).ToList();

            await _userDataAccess.UpdateAsync(user);

            var plates = user.Cars
                .Select(c => c.Plate);
            foreach(var plate in plates)
            {
                await _conversationDataAccess.UpdateConversationsWithEmptyUser(user.Id, plate);
            }

            return Ok();
        }
    }
}