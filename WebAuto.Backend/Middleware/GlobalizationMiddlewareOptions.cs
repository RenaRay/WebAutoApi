using System;
using System.Collections.Generic;

namespace WebAuto.Backend.Middleware
{
    public class GlobalizationMiddlewareOptions
    {
        public IDictionary<string, string> LanguageToCultureMappings { get; private set; }

        public GlobalizationMiddlewareOptions()
        {
            LanguageToCultureMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public GlobalizationMiddlewareOptions(string supportedCultures, string languageToCultureMappings)
            : this()
        {
            if (string.IsNullOrEmpty(supportedCultures))
            {
                throw new ArgumentNullException("supportedCultures");
            }
            ParseSupportedCultures(supportedCultures);
            ParseMappings(languageToCultureMappings);
        }

        private void ParseSupportedCultures(string supportedCultures)
        {
            foreach (var culture in supportedCultures.Split(';'))
            {
                LanguageToCultureMappings[culture] = culture;
            }
        }

        private void ParseMappings(string languageToCultureMappings)
        {
            if (string.IsNullOrEmpty(languageToCultureMappings))
            {
                return;
            }
            foreach (var mapping in languageToCultureMappings.Split(';'))
            {
                var pair = mapping.Split('=');
                var language = pair[0];
                var culture = pair[1];
                LanguageToCultureMappings[language] = culture;
            }
        }
    }
}