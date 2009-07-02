using System;
using System.Collections.Generic;
using System.Text;

namespace NunoGomes.Web.Configuration
{
    /// <summary>
    /// Attribute that should be applied to Pages where the original Ids must be rendered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class KeepOriginalIDsAttribute : Attribute
    {
        private bool _enabled = true;

        public KeepOriginalIDsAttribute() { }
        public KeepOriginalIDsAttribute(bool enabled)
        {
            _enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="KeepOriginalIDsAttribute"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get { return _enabled; }
        }
    }
}
