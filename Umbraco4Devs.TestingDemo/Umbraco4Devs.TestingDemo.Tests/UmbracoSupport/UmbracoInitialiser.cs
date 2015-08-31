using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Examine;
using Examine.Providers;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    public static class UmbracoInitialiser
    {
        private static TestingUmbracoApplication application;

        public static TestingUmbracoApplication EnsureApplication()
        {
            if (application == null)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                application = new TestingUmbracoApplication(baseDirectory);
                application.Start();
            }
            return application;
        }

//        private static void RebuildExamineIndexes()
//        {
//            var providers = ExamineManager.Instance.IndexProviderCollection.Cast<BaseIndexProvider>().ToList();
//            int indexed = 0;
//            var startTime = DateTime.Now;
//            var maxTime = Debugger.IsAttached ? TimeSpan.FromMinutes(5) : TimeSpan.FromSeconds(10);
//            foreach (var provider2 in providers)
//            {
//                provider2.RebuildIndex();
//                provider2.NodesIndexed += (sender, args) => Interlocked.Increment(ref indexed);
//            }
//            while (indexed < providers.Count)
//            {
//                Thread.Sleep(100);
//                if (DateTime.Now > startTime + maxTime)
//                    throw new Exception("Examine not initialised within time allowed");
//            }
//        }
    }
}