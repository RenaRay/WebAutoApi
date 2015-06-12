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

[assembly: OwinStartup(typeof(WebAuto.Backend.Startup))]
namespace WebAuto.Backend
{
    public class Startup
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