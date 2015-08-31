using Examine.Config;
using Umbraco.Core;
using Examine;

namespace Umbraco4Devs.TestingDemo.WebApp
{
    public class TestEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var s = ExamineSettings.Instance;
        } 
         
    }
}