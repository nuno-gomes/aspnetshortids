using System;
using System.Reflection;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Configuration.Provider;

namespace NunoGomes.Web.Configuration
{
    /// <summary>
    /// </summary>
    public sealed class NamingConfiguration
    {
        private static NamingProvider _provider = null;
        private static NamingProviderCollection _providers = new NamingProviderCollection();
        private static object _lock = new object();

        /// <summary>
        /// Gets the default provider.
        /// </summary>
        /// <value>The default provider.</value>
        public static NamingProvider Provider
        {
            get
            {
                LoadProviders();
                return _provider;
            }
        }

        /// <summary>
        /// Gets the providers collection.
        /// </summary>
        /// <value>The providers collection.</value>
        public static NamingProviderCollection Providers
        {
            get
            {
                LoadProviders(); 
                return _providers;
            }
        }

        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (_lock)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {

                        // Get a reference to the <ServiceConfig> section
                        string sectionName = "NamingConvention";
                        NamingSection section = (NamingSection)WebConfigurationManager.GetSection(sectionName);
                        if (section == null)
                            throw new ProviderException(string.Format("Unable to load {0} configuration", sectionName));



                        // Load registered providers and point _provider to the default provider
                        _providers = new NamingProviderCollection();
                        ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(NamingProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException("Unable to load default NamingProvider");
                    }
                }
            }
        }
    }
}
