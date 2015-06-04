using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAuto.Backend.Middleware
{
    public class GlobalizationMiddleware : OwinMiddleware
    {
        private readonly GlobalizationMiddlewareOptions _options;

        public GlobalizationMiddleware(OwinMiddleware next, GlobalizationMiddlewareOptions options)
            : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            _options = options;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var culture = GetCultureFromRequestHeaders(context);
            SetThreadCulture(culture);
            await this.Next.Invoke(context);
        }


        private string GetCultureFromRequestHeaders(IOwinContext context)
        {
            string[] acceptLanguageHeaders;
            if (!context.Request.Headers.TryGetValue("Accept-Language", out acceptLanguageHeaders))
            {
                return null;
            }
            var languages = acceptLanguageHeaders
                .SelectMany(header => header.Split(',', ';'));
            return languages
                .Select(GetCultureFromLanguage)
                .FirstOrDefault(culture => !string.IsNullOrEmpty(culture));
        }

        private string GetCultureFromLanguage(string language)
        {
            string culture;
            _options.LanguageToCultureMappings.TryGetValue(language, out culture);
            return culture;
        }

        private void SetThreadCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                return;
            }
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}