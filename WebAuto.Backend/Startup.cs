using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Unity.WebApi;
using WebAuto.Backend.Middleware;


[assembly: OwinStartup(typeof(WebAuto.Backend.Startup))] //атрибут, с пмощиью которого класс инициализируется один раз, при старте приложения
namespace WebAuto.Backend
{
    public class Startup

    //это стандартный для asp.net web api класс, в котором описаны все настройки бэкенда
    //1. ConfigureContainer - настройка контейнера(Unity Container)
    //2. ConfigureMiddleware - настройка обработчиков middleware(локализация)
    //3. ConfigureOAuth - настройка аутентификации(OAuth)
    //4. ConfigureRoutes - настройка маршрутов(routing) по умолчанию
    //5. ConfigureFormatters - настройка форматирования json

    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();

            IUnityContainer container = ConfigureContainer(config);
            ConfigureMiddleware(app, container);
            ConfigureOAuth(app, container);
            ConfigureRoutes(config);
            ConfigureFormatters(config);

            app.UseWebApi(config);
        }

        private static UnityContainer ConfigureContainer(HttpConfiguration config)
        {
            //зависимости регистрируются в web.config
            //в asp.net web api есть поддержка разрешения зависимостей с помощью dependencyResolver'а
            //мы просто подсказываем asp.net web api, что для разрешения зависимостей нужно использовать unity
            var container = new UnityContainer();
            container.LoadConfiguration("default");
            config.DependencyResolver = new UnityDependencyResolver(container);
            return container;
        }

        private void ConfigureMiddleware(IAppBuilder app, IUnityContainer container)
        {
            var globalizationMiddlewareOptions = container.Resolve<GlobalizationMiddlewareOptions>();
            app.Use<GlobalizationMiddleware>(globalizationMiddlewareOptions);
        }

        public void ConfigureOAuth(IAppBuilder app, IUnityContainer container)
        {
            var authorizationServerOptions = container.Resolve<OAuthAuthorizationServerOptions>();
            app.UseOAuthAuthorizationServer(authorizationServerOptions);

            var bearerAuthenticationOptions = container.Resolve<OAuthBearerAuthenticationOptions>();
            app.UseOAuthBearerAuthentication(bearerAuthenticationOptions);
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureFormatters(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}