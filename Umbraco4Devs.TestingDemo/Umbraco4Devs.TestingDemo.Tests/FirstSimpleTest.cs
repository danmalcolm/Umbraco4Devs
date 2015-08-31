using System.Linq;
using Examine;
using Examine.Config;
using Umbraco.Core;
using Umbraco.Web;
using Xunit;

namespace Umbraco4Devs.TestingDemo.Tests
{
    public class FirstSimpleTest : UmbracoTest
    {
        [Fact]
        public void can_retrieve_content_via_contentservice_api()
        {
            var contentService = ApplicationContext.Current.Services.ContentService;

            var content = contentService.GetRootContent().ToList();
            Assert.NotEmpty(content);
            var home = content.FirstOrDefault(x => x.ContentType.Alias == "HomePage");
            Assert.NotNull(home);
        }

        [Fact]
        public void can_retrieve_content_via_UmbracoHelper()
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var content = umbracoHelper.TypedContentAtRoot();
            Assert.NotEmpty(content);
            var home = content.FirstOrDefault(x => x.ContentType.Alias == "HomePage");
            Assert.NotNull(home);
        }

        [Fact]
        public void can_retrieve_content_via_Examine_search()
        {
            // For some reason, this didn't work while there was a single root HomePage
            // node
            var provider = ExamineManager.Instance.DefaultSearchProvider;
            var criteria = provider.CreateSearchCriteria();
            criteria.Field("nodeName", "Home");
            var results = provider.Search(criteria);
            Assert.Equal(1, results.TotalItemCount);
        }
        
        [Fact]
        public void can_retrieve_examine_settings()
        {
            // Set up application
            var s = ExamineSettings.Instance;
        }
    }
}
