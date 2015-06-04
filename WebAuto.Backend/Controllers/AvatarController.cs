using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Controllers
{
    [RoutePrefix("avatar")]
    public class AvatarController : ApiController
    {
        private readonly IAvatarDataAccess _avatarDataAccess;
        private readonly IUserDataAccess _userDataAccess;

        public AvatarController(
            IAvatarDataAccess avatarDataAccess,
            IUserDataAccess userDataAccess)
        {
            if (avatarDataAccess == null)
            {
                throw new ArgumentNullException("avatarDataAccess");
            }
            if (userDataAccess == null)
            {
                throw new ArgumentNullException("userDataAccess");
            }
            _avatarDataAccess = avatarDataAccess;
            _userDataAccess = userDataAccess;
        }

        [Authorize]
        public async Task<IHttpActionResult> Post(HttpRequestMessage request)
        {
            var currentUserLogin = User.Identity.Name;
            var user = await _userDataAccess.FindByLoginAsync(currentUserLogin);
            if (user == null)
            {
                return NotFound();
            }

            //TODO: google for HttpContent.Dispose()
            MultipartMemoryStreamProvider provider = await request.Content.ReadAsMultipartAsync();
            if (provider.Contents.Count != 1)
            {
                return BadRequest("Only one part was expected in multipart request");
            }
            HttpContent content = provider.Contents.First();

            var avatar =
                new Avatar
                {
                    ContentType = content.Headers.ContentType.MediaType
                };
            avatar.Content = await content.ReadAsByteArrayAsync();
            await _avatarDataAccess.CreateAsync(avatar);

            user.Avatar = avatar.Id;
            await _userDataAccess.UpdateAsync(user);

            return Ok(new { avatar.Id });
        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var result = new HttpResponseMessage();
            if (string.IsNullOrEmpty(id))
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Content = new StringContent("id was expected");
                return result;
            }
            Avatar avatar = await _avatarDataAccess.FindByIdAsync(id);
            if (avatar == null)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                return result;
            }

            result.StatusCode = HttpStatusCode.OK;
            var stream = new MemoryStream(avatar.Content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(avatar.ContentType);
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }
    }
}