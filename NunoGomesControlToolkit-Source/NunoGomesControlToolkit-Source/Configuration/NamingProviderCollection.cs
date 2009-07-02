using System;
using System.Configuration.Provider;

namespace NunoGomes.Web.Configuration
{
    public sealed class NamingProviderCollection : ProviderCollection
    {
        public new NamingProvider this[string name]
        {
            get { return (NamingProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is NamingProvider))
                throw new ArgumentException("Invalid provider type", "provider");

            base.Add(provider);
        }
    }
}
