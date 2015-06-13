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
    //этот класс берет из хэдера языковые настройки(название культуры) и 
    //устанавливает культуру по умолчанию void SetThreadCulture(string culture)
    //дальше работает сам .net framework
    //засчет того, что сообщения об ошибках валидации хранятся в ресурсных файлах,
    //в итоге для пользователей с англ. культурой сообщения отображаются на анг. языке
    //для пользователей с рус. культурой - на русском

    //до обработки запроса кодом нашего бэкенда запрос проходит через обработку с помощью middleware
    //middleware используют для добавления сквозного функционала, т.е. кода, который будет выполняться для всех методов бэкенда
    //например: логирование, аутентификация, обработка ошибок
    //у нас используется для настройки языка по умолчанию
    //если в приложении используется несколько обработчиков Middleware, то они выполняются последовательно друг за другом
    //в нашем коде: await this.Next.Invoke(context); - это указатель на следующий обработчик в цепочке middleware
    //мы передаем обработку запроса следующему middleware в списке

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
            //определение языка, на котором пользователю надо показывать сообщения об ошибках
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