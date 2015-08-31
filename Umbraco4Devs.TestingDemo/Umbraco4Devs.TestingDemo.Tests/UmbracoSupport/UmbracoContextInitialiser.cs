using System.Web;
using Umbraco.Core;
using Umbraco.Web;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    public class UmbracoContextInitialiser
    {
        public static void Init()
        {
            HttpContextBase httpContext = TestHttpContextBuilder.Create("GET", "/");
            UmbracoContext.EnsureContext(httpContext, ApplicationContext.Current, true);
        }
    }
}