using System.Web;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    public class TestingSiteMapProvider : StaticSiteMapProvider
    {
        private SiteMapNode root;

        public TestingSiteMapProvider()
        {
            root = new SiteMapNode(this, "-1", "/", "root", "umbraco root", null, null, null, null);
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return root;
        }

        public override SiteMapNode BuildSiteMap()
        {
            return root;
        }
    }
}