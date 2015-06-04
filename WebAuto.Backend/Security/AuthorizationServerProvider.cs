using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebAuto.Backend.Properties;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IHashAlgorithm _hashAlgorithm;

        public AuthorizationServerProvider(
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

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var passwordHash = _hashAlgorithm.Hash(context.Password);
            User user = await _userDataAccess.FindByLoginAndPasswordHashAsync(context.UserName, passwordHash);
            if (user == null)
            {
                context.SetError("invalid_grant", Resources.AuthenticationIncorrectLoginOrPassword);
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

            context.Validated(identity);
        }
    }
}