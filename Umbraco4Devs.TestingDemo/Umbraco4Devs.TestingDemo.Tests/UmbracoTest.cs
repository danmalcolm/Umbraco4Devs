using System.Web;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco4Devs.TestingDemo.Tests.UmbracoSupport;

namespace Umbraco4Devs.TestingDemo.Tests
{
    public abstract class UmbracoTest
    {
        /// <summary>
        /// Static constructor - runs to ensure Umbraco application is initialised
        /// when running tests
        /// </summary>
        static UmbracoTest()
        {
            UmbracoInitialiser.EnsureApplication();
        }

        protected UmbracoTest()
        {
            // Sets up fresh UmbracoContext for each test
            UmbracoContextInitialiser.Init();
        }
    }
}