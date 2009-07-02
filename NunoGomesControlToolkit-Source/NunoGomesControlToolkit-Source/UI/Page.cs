using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NunoGomes.Web.Configuration;

namespace NunoGomes.Web.UI
{
    /// <summary>
    /// Summary description for NewPage
    /// </summary>
    public class Page : global::System.Web.UI.Page
    {
        #region Naming Management

        /// <summary>
        /// Creates a new <see cref="T:System.Web.UI.ControlCollection"></see> object to hold the child controls (both literal and server) of the server control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.UI.ControlCollection"></see> object to contain the current server control's child server controls.
        /// </returns>
        protected override ControlCollection CreateControlCollection()
        {
            return NamingConfiguration.Provider.CreateControlCollection(this);
        }

        /// <summary>
        /// Searches the current naming container for a server control with the specified id and an integer, specified in the pathOffset parameter, which aids in the search. You should not override this version of the <see cref="Overload:System.Web.UI.Control.FindControl"></see> method.
        /// </summary>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container.</param>
        /// <returns>
        /// The specified control, or null if the specified control does not exist.
        /// </returns>
        protected override Control FindControl(string id, int pathOffset)
        {
            Control ctrl = base.FindControl(id, pathOffset);
            if (ctrl == null)
            {
                ctrl = NamingConfiguration.Provider.FindControl(this, id, pathOffset);
            }
            return ctrl;
        }

        #endregion Naming Management

    }
}