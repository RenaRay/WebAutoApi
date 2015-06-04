using System.Web.Http;
using WebAuto.Backend.Models;
using WebAuto.Backend.Properties;

namespace WebAuto.Backend.Controllers
{
    [RoutePrefix("config")]
    public class ConfigurationController : ApiController
    {
        [HttpGet]
        [Route("")]
        public ConfigurationModel Get()
        {
            return
                new ConfigurationModel
                {
                    FeedLength = Settings.Default.FeedLength
                };
        }
    }
}