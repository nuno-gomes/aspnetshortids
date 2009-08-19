using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI;
using System.Configuration;
using System.Web.Compilation;

namespace NunoGomes.Web.UI
{
    /// <summary>
    /// This class provides methods to create dynamically mapped controls, using the PageSection TagMappings and Prefix info.
    /// </summary>
    public class DynamicControlBuilder
    {
        #region Private Fields
        private static Dictionary<Type, Type> m_tagMappings = null;
        private static Dictionary<string, TagPrefixInfo> m_prefixes = null;
        private static object m_syncobj = new object();
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Static constructor
        /// </summary>
        static DynamicControlBuilder()
        {
            if (m_tagMappings == null)
            {
                lock (m_syncobj)
                {
                    if (m_tagMappings == null)
                    {
                        Dictionary<Type, Type> tagmapping = new Dictionary<Type, Type>();
                        PagesSection section = (PagesSection)WebConfigurationManager.GetSection("system.web/pages");
                        foreach (TagMapInfo info in section.TagMapping)
                        {
                            Type type = BuildManager.GetType(info.TagType, true);
                            Type c = BuildManager.GetType(info.MappedTagType, true);
                            if (!type.IsAssignableFrom(c))
                            {
                                throw new ConfigurationErrorsException("Mapped type must inherit");
                            }
                            tagmapping[type] = c;
                        }

                        Dictionary<string, TagPrefixInfo> prefixes = new Dictionary<string, TagPrefixInfo>(StringComparer.OrdinalIgnoreCase);
                        foreach (TagPrefixInfo info in section.Controls)
                        {
                            prefixes[info.TagPrefix] = info;
                        }
                        m_prefixes = prefixes;
                        m_tagMappings = tagmapping;
                    }
                }
            }
        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Gets the mapped <see cref="System.Web.UI.Control"/> type.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Web.UI.Control"/> type to be mapped</typeparam>
        /// <returns>A <see cref="System.Type"/> object.</returns>
        public static Type GetMappedType<T>() where T : global::System.Web.UI.Control
        {
            return GetMappedType(typeof(T), null);
        }

        /// <summary>
        /// Gets the mapped <see cref="System.Web.UI.Control"/> type.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Web.UI.Control"/> type to be mapped</typeparam>
        /// <param name="prefix">The namespace prefix.</param>
        /// <returns>A <see cref="System.Type"/> object.</returns>
        public static Type GetMappedType<T>(string prefix) where T : global::System.Web.UI.Control
        {
            return GetMappedType(typeof(T), prefix);
        }

        /// <summary>
        /// Gets the mapped <see cref="System.Web.UI.Control"/> type.
        /// </summary>
        /// <param name="type">The <see cref="System.Web.UI.Control"/> type to be mapped</param>
        /// <returns>A <see cref="System.Type"/> object.</returns>
        public static Type GetMappedType(Type type)
        {
            return GetMappedType(type, null);
        }

        /// <summary>
        /// Gets the mapped <see cref="System.Web.UI.Control"/> type.
        /// </summary>
        /// <param name="type">The <see cref="System.Web.UI.Control"/> type to be mapped</param>
        /// <param name="prefix">The namespace prefix.</param>
        /// <returns>A <see cref="System.Type"/> object.</returns>
        public static Type GetMappedType(Type type, string prefix)
        {
            if (!typeof(global::System.Web.UI.Control).IsAssignableFrom(type))
            {
                throw new ArgumentOutOfRangeException("type", "Must inherit from Control.");
            }
            Type mappedtype;
            if (!string.IsNullOrEmpty(prefix))
            {
                TagPrefixInfo prefixinfo;
                if (!m_prefixes.TryGetValue(prefix, out prefixinfo))
                {
                    throw new ArgumentException("prefix", "No prefix found.");
                }
                else
                {
                    type = BuildManager.GetType(string.Format("{0}.{1}, {2}", prefixinfo.Namespace, type.UnderlyingSystemType.Name, prefixinfo.Assembly), false);
                    if (type == null)
                    {
                        throw new ArgumentException("type", "Control not found within specified prefix.");
                    }
                }
            }
            if (m_tagMappings.TryGetValue(type.UnderlyingSystemType, out mappedtype))
            {
                return mappedtype;
            }
            return type;
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Web.UI.Control"/> type to be mapped</typeparam>
        /// <returns>A <paramref name="T"/> object.</returns>
        public static T CreateControl<T>() where T : global::System.Web.UI.Control, new()
        {
            return (T)CreateControl(typeof(T), null);
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Web.UI.Control"/> type to be mapped</typeparam>
        /// <param name="prefix">The namespace prefix.</param>
        /// <returns>A <paramref name="T"/> object.</returns>
        public static T CreateControl<T>(string prefix) where T : global::System.Web.UI.Control, new()
        {
            return (T)CreateControl(typeof(T), prefix);
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Web.UI.Control"/> type to be mapped</typeparam>
        /// <param name="prefix">The namespace prefix.</param>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <returns>A <paramref name="T"/> object.</returns>
        public static T CreateControl<T>(string prefix, params object[] args) where T : global::System.Web.UI.Control
        {
            return (T)CreateControl(typeof(T), prefix, args);
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Web.UI.Control"/> type to be mapped</param>
        /// <returns>A <see cref="System.Web.UI.Control"/> object.</returns>
        public static global::System.Web.UI.Control CreateControl(Type type)
        {
            return CreateControl(type, null);
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Web.UI.Control"/> type to be mapped</param>
        /// <param name="prefix">The namespace prefix.</param>
        /// <returns>A <see cref="System.Web.UI.Control"/> object.</returns>
        public static global::System.Web.UI.Control CreateControl(Type type, string prefix)
        {
            return CreateControl(type, prefix, null);
        }

        /// <summary>
        /// Creates a dynamic mapped <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Web.UI.Control"/> type to be mapped</param>
        /// <param name="prefix">The namespace prefix.</param>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <returns>A <see cref="System.Web.UI.Control"/> object.</returns>
        public static global::System.Web.UI.Control CreateControl(Type type, string prefix, params object[] args)
        {
            Type mappedType = GetMappedType(type, prefix); ;
            return (global::System.Web.UI.Control)Activator.CreateInstance(mappedType, args);
        }
        #endregion Public Methods
    }
}