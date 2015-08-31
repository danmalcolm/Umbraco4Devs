using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco4Devs.TestingDemo.WebApp.Services.Blogs
{
    public class CreateBlogPostRequest
    {
        public string Title { get; set; }

        public string MainContent { get; set; }
    }

    public class CreateBlogPostRequestHandler
    {

        public void Handle(CreateBlogPostRequest request)
        {
            
        }
    }
}