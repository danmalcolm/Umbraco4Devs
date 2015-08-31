using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Umbraco.Core;
using Umbraco.Web;

namespace Umbraco4Devs.TestingDemo.Tests.UmbracoSupport
{
    /// <summary>
    /// Umbraco application suitable for running Umbraco outside of context
    /// of a web application
    /// </summary>
    public class TestingUmbracoApplication : UmbracoApplicationBase
    {

        private readonly string _baseDirectory;
        private static TestingUmbracoApplication _application;
        private static bool _started;
        private static readonly object AppLock = new object();

        public TestingUmbracoApplication(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        protected override IBootManager GetBootManager()
        {
            return new TestingBootManager(this,  _baseDirectory);
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Start()
        {
            lock (AppLock)
            {
                if (_started)
                    throw new InvalidOperationException("Application has already started.");
                Application_Start(this, EventArgs.Empty);
                _started = true;
            }
        }
    }
}