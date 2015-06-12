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
                    PasswordHash = _hashAlgorithm.Hash(model.Password),
                    Cars = new List<Car>()
                };
            await _userDataAccess.CreateAsync(userCreateContract);

            return Ok();
        }
    }
}