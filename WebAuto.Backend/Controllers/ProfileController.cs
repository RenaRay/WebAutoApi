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
        private readonly IMessageDataAccess _messageDataAccess;

        public ProfileController(
            IUserDataAccess userDataAccess,
            IAvatarDataAccess avatarDataAccess,
            IMessageDataAccess messageDataAccess)
        {
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            if (avatarDataAccess == null)
            {
                throw new ArgumentNullException("avatarDataAccess");
            }
            if (messageDataAccess == null)
            {
                throw new ArgumentNullException("messageDataAccess");
            }
            _userDataAccess = userDataAccess;
            _avatarDataAccess = avatarDataAccess;
            _messageDataAccess = messageDataAccess;
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
        [Route("")]
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
            user.BirthDate = model.BirthDate;
            user.AvatarId = model.Avatar;
            user.FirstLicenseDate = model.FirstLicenseDate;
            user.Gender = model.Gender;
            user.HairColor = model.HairColor;
            user.MaritalStatus = model.MaritalStatus;
            user.Occupation = model.Occupation;
            var cars = model.Cars ?? Enumerable.Empty<CarModel>();
            user.Cars = cars
                .Where(car =>
                    !string.IsNullOrEmpty(car.Plate) ||
                    !string.IsNullOrEmpty(car.Model) ||
                    !string.IsNullOrEmpty(car.Vendor))
                .Select(car => car.ToDataContract())
                .ToList();

            await _userDataAccess.UpdateAsync(user);

            //1. получить список номеров автомобилей
            //2. найти сообщения, не привязанные к пользователю и
            //привязать все сообщения к текущему пользователю
            await _messageDataAccess.AddMessagesToUser(user.Id);
            
            return Ok();
        }
    }
}