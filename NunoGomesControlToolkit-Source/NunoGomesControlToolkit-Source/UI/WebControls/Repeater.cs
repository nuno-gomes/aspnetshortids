using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using NunoGomes.Web.Configuration;

namespace NunoGomes.Web.UI.WebControls
{
    public class Repeater : global::System.Web.UI.WebControls.Repeater
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
        /// Gets or sets the programmatic identifier assigned to the server control.
        /// </summary>
        /// <value></value>
        /// <returns>The programmatic identifier assigned to the control.</returns>
        public override string ID
        {
            get { return NamingConfiguration.Provider.GetControlID(this, base.ID); }
            set { base.ID = NamingConfiguration.Provider.SetControlID(value, this); }
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

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.EnsureID();
            this.ID = base.ID;
            base.OnInit(e);
        }
        #endregion Naming Management

        #region Item Dynamic tag mapping 

        private string m_itemTagPrefix = null;

        /// <summary>
        /// Gets or sets the tag prefix to be used for Item creation.
        /// </summary>
        /// <value>The item tag prefix.</value>
        public string ItemTagPrefix
        {
            get { return m_itemTagPrefix; }
            set { m_itemTagPrefix = value; }
        }

        /// <summary>
        /// Creates a <see cref="T:System.Web.UI.WebControls.RepeaterItem"/> object with the specified item type and location within the <see cref="T:System.Web.UI.WebControls.Repeater"/> control.
        /// </summary>
        /// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.Repeater"/> control to place the created item.</param>
        /// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType"/> that represents the specified type of the <see cref="T:System.Web.UI.WebControls.Repeater"/> item to create.</param>
        /// <returns>
        /// The new <see cref="T:System.Web.UI.WebControls.RepeaterItem"/> object.
        /// </returns>
        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            return DynamicControlBuilder.CreateControl<global::System.Web.UI.WebControls.RepeaterItem>(this.ItemTagPrefix, new object[] { itemIndex, itemType });
        }

        #endregion Item Dynamic tag mapping
    }
}
