using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Moq;
using Umbraco.Web.Media.EmbedProviders.Settings;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    public static class TestHttpContextBuilder
    {
        public static HttpContextBase Create(string httpMethod, string path, NameValueCollection headers = null, NameValueCollection form = null)
        {
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");

            var uri = new Uri("http://www.tempuri.org" + path, UriKind.Absolute);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.Items).Returns(new Dictionary<string,object>());
            httpContext.Setup(c => c.Request.ApplicationPath).Returns("/");
            httpContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~" + uri.LocalPath);
            httpContext.Setup(c => c.Request.Url).Returns(uri);
            httpContext.Setup(c => c.Request.PathInfo).Returns("");
            httpContext.Setup(c => c.Request.ServerVariables).Returns(new NameValueCollection());
            httpContext.Setup(c => c.Request.Cookies).Returns(new HttpCookieCollection());
            var queryString = HttpUtility.ParseQueryString(uri.Query);
            httpContext.Setup(x => x.Request.QueryString).Returns(queryString);
            headers = headers ?? new NameValueCollection();
            httpContext.Setup(x => x.Request.Headers).Returns(headers);
            httpContext.Setup(x => x.Request.HttpMethod).Returns(httpMethod);
            form = form ?? new NameValueCollection();
            httpContext.Setup(x => x.Request.Form).Returns(form);
            httpContext.Setup(x => x.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns((string x) => x);
            httpContext.Setup(x => x.Request[It.IsAny<string>()]).Returns((string key) =>
            {
                if (queryString.AllKeys.Contains(key))
                    return queryString[key];
                if (form.AllKeys.Contains(key))
                    return form[key];
               return null;
            });
            return httpContext.Object;
        } 
    }
}