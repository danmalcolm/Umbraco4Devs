using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Umbraco.Core;
using umbraco.interfaces;
using Umbraco.Web.PublishedCache;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    public class TestingBootManager : CoreBootManager
    {
        private readonly string _baseDirectory;

        public TestingBootManager(UmbracoApplicationBase umbracoApplication, string baseDirectory)
            : base(umbracoApplication)
        {
            _baseDirectory = baseDirectory;

            base.InitializeApplicationRootPath(_baseDirectory);

            // this is only here to ensure references to the assemblies needed for
            // the DataTypesResolver otherwise they won't be loaded into the AppDomain.
            var interfacesAssemblyName = typeof(IDataType).Assembly.FullName;
        }

        protected override void InitializeResolvers()
        {
            base.InitializeResolvers();

            InitPublishedContentCachesResolver();
        }

        private void InitPublishedContentCachesResolver()
        {
            // Use reflection to implement the following (from WebBootManager)

            // PublishedCachesResolver.Current = new PublishedCachesResolver(new PublishedCaches(
            //   new PublishedCache.XmlPublishedCache.PublishedContentCache(),
            //   new PublishedCache.XmlPublishedCache.PublishedMediaCache(ApplicationContext)));

            try
            {
                // Create PublishedCaches
                var publishedContentCacheType = Type.GetType("Umbraco.Web.PublishedCache.XmlPublishedCache.PublishedContentCache, umbraco");
                Debug.Assert(publishedContentCacheType != null, "publishedContentCacheType != null");
                var publishedContentCache = (IPublishedContentCache) Activator.CreateInstance(publishedContentCacheType);
                var publishedMediaType = Type.GetType("Umbraco.Web.PublishedCache.XmlPublishedCache.PublishedMediaCache, umbraco");
                Debug.Assert(publishedMediaType != null, "publishedMediaType != null");
                var publishedMediaCache = (IPublishedMediaCache) Activator.CreateInstance(publishedMediaType, new[] {ApplicationContext.Current});
                var publishedCachesType = Type.GetType("Umbraco.Web.PublishedCache.PublishedCaches, umbraco");
                Debug.Assert(publishedCachesType != null, "publishedCachesType != null");
                var ctor = publishedCachesType.GetConstructors().Single();
                var publishedCaches = ctor.Invoke(new object[] {publishedContentCache, publishedMediaCache});
            
                // Create PublishedCachesResolver and initialise PublishedCachesResolver.Current
                var resolverType = Type.GetType("Umbraco.Web.PublishedCache.PublishedCachesResolver, umbraco");
                Debug.Assert(resolverType != null, "resolverType != null");
                var resolver = Activator.CreateInstance(resolverType, BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new[] {publishedCaches}, CultureInfo.CurrentCulture);
                var currentProp = resolverType.GetProperty("Current", BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public);
                currentProp.SetValue(null, resolver);
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured while initialising PublishedCachesResolver.Current. This relies on reflection to create instances of internal types. Check the inner exception and / or debug this method for further details", exception);
            }
        }
    }
}