using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;

namespace NunoGomes.Web.Configuration
{
    public abstract class NamingProvider : ProviderBase
    {
        #region Private Fields
        private bool _keepOriginalIDs = false;
        private StringCollection _exceptionlist = null;
        #endregion Private Fields

        #region ProviderBase Implementation

        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException("config");

            if (!string.IsNullOrEmpty(config["keeporiginalids"]))
            {
                Boolean.TryParse(config["keeporiginalids"], out _keepOriginalIDs);
            }
            config.Remove("keeporiginalids");

            if (!string.IsNullOrEmpty(config["exceptionlist"]))
            {
                string[] a = config["exceptionlist"].Split(new char[] { ';', ',' });
                _exceptionlist = new StringCollection();
                _exceptionlist.AddRange(a);
            }
            config.Remove("exceptionlist");

            // Call the base class's Initialize method
            base.Initialize(name, config);

            /*
             * ----------------------
             * Throw an exception if unrecognized attributes remain
             */
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        #endregion

        #region Specific Public Properties
        ///// <summary>
        ///// Gets a value indicating whether to keep original Ids.
        ///// </summary>
        ///// <value><c>true</c> to keep original Ids; otherwise, <c>false</c>.</value>
        public bool KeepOriginalIDs
        {
            get
            {
                return _keepOriginalIDs;
            }
        }

        /// <summary>
        /// Gets a list of pages that will allways render the original Ids.
        /// </summary>
        /// <value>The list of pages that will allways render the original Ids.</value>
        public StringCollection ExceptionList
        {
            get { return _exceptionlist; }
        }

        #endregion Specific Public Properties

        #region Public Methods

        /// <summary>
        /// Generates the Control ID.
        /// </summary>
        /// <param name="name">The controls name.</param>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public abstract string SetControlID(string name, System.Web.UI.Control control);

        /// <summary>
        /// Creates a controls collection.
        /// </summary>
        /// <param name="control">The owner control.</param>
        /// <returns></returns>
        public ControlCollection CreateControlCollection(System.Web.UI.Control control)
        {
            return new NamingContainerControlCollection(control);
        }

        /// <summary>
        /// Gets the control name given the control's id.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="id">The controls id</param>
        /// <returns></returns>
        public string GetControlID(System.Web.UI.Control control, string id)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (control.NamingContainer != null)
            {
                NamingContainerControlCollection namingContainerCollection = control.NamingContainer.Controls as NamingContainerControlCollection;
                if (namingContainerCollection != null)
                {
                    return namingContainerCollection.GetName(id);
                }
            }
            return id;
        }

        /// <summary>
        /// Finds a control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="id">The id.</param>
        /// <param name="pathOffset">The path offset.</param>
        /// <returns></returns>
        public Control FindControl(System.Web.UI.Control control, string id, int pathOffset)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            NamingContainerControlCollection controlsCollection = control.Controls as NamingContainerControlCollection;
            if (controlsCollection == null)
            {
                return null;
                //throw new ArgumentException("Invalid controls collection");
            }

            return controlsCollection.FindControl(id, pathOffset);
        }

        #endregion Public Methods
    }
}
