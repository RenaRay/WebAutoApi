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

        public ProfileController(
            IUserDataAccess userDataAccess,
            IAvatarDataAccess avatarDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (avatarDataAccess == null)
            {
                throw new ArgumentNullException("avatarDataAccess");
            }
            _userDataAccess = userDataAccess;
            _avatarDataAccess = avatarDataAccess;
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
            user.AvatarId = model.Avatar;
            var cars = model.Cars ?? Enumerable.Empty<CarModel>();
            user.Cars = cars
                .Where(car =>
                    !string.IsNullOrEmpty(car.Plate) ||
                    !string.IsNullOrEmpty(car.Model) ||
                    !string.IsNullOrEmpty(car.Vendor))
                .Select(car => car.ToDataContract())
                .ToList();

            await _userDataAccess.UpdateAsync(user);
            
            return Ok();
        }
    }
}