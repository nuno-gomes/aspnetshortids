using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace NunoGomes.Web.Configuration
{
    public class ShortIDsProvider : NamingProvider
    {
        #region Private Fields
        private const string ID_PREFIX = "T";
        private static object KeepLongIDsAttributeValueKey = new object();
        #endregion Private Fields

        #region Public Methods
        private bool KeepOriginalID(System.Web.UI.Control control)
        {
            bool keepOriginalIDs = this.KeepOriginalIDs;

            #region KeepLongIDs Attribute Value Management
            if (!keepOriginalIDs)
            {
                if (HttpContext.Current.Items.Contains(KeepLongIDsAttributeValueKey))
                {
                    keepOriginalIDs = (bool)HttpContext.Current.Items[KeepLongIDsAttributeValueKey];
                }
                else
                {
                    string path = System.Web.HttpContext.Current.Request.Path.Replace(System.Web.HttpContext.Current.Request.ApplicationPath, string.Empty);

                    if (this.ExceptionList != null && this.ExceptionList.Contains(path))
                    {
                        keepOriginalIDs = true; ;
                    }
                    else
                    {
                        if (control.Page != null)
                        {
                            object[] atts = control.Page.GetType().GetCustomAttributes(true);

                            foreach (Attribute att in atts)
                            {
                                if (att is KeepOriginalIDsAttribute)
                                {
                                    keepOriginalIDs = ((KeepOriginalIDsAttribute)att).Enabled;
                                    break;
                                }
                            }
                        }
                    }
                    HttpContext.Current.Items[KeepLongIDsAttributeValueKey] = keepOriginalIDs;
                }
            }
            #endregion KeepLongIDs Attribute Value Management

            return keepOriginalIDs;
        }
        #endregion Public Methods

        #region NamingProvider Implementation

        /// <summary>
        /// Generates the Control ID.
        /// </summary>
        /// <param name="name">The controls name.</param>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public override string SetControlID(string name, System.Web.UI.Control control)
        {
            if (this.KeepOriginalID(control))
            {
                return name;
            }
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (control.NamingContainer == null)
            {
                return name;
            }
            NamingContainerControlCollection controlsCollection = control.NamingContainer.Controls as NamingContainerControlCollection;
            if (controlsCollection == null)
            {
                return name;
            }

            string shortid = null; ;
            if (!controlsCollection.ContainsName(name))
            {
                shortid = string.Format("{0}{1}", ID_PREFIX, controlsCollection.GetUniqueControlSufix());

                if (string.IsNullOrEmpty(name))
                {
                    name = shortid;
                }
                controlsCollection.RegisterControl(shortid, name, control);
            }
            else
            {
                shortid = control.ID;
            }
            return shortid;
        }

        #endregion NamingProvider Implementation

    }
}
