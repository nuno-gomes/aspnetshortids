using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EnhancedWebApp
{
    public partial class WebUserControl2 : NunoGomes.Web.UI.UserControl
    {
        public void DumpControl(object e, EventArgs args)
        {
            this.Detail.Controls.Add(new LiteralControl(string.Format(@"
this.ID={0}<br/>
this.UniqueID={1}<br/>
this.ClientID={2}<br/>
this.Text={3}<br/>
this.Page.FindControl(UniqueID).UniqueID = {4}<br/>
this.NamingContainer.FindControl(ID).UniqueID = {5}",
                                               ((Control)e).ID,
                                               ((Control)e).UniqueID,
                                               ((Control)e).ClientID,
                                               ((TextBox)e).Text,
                                               ((TextBox)e).Page.FindControl(((Control)e).UniqueID).UniqueID,
                                               ((TextBox)e).NamingContainer.FindControl(((Control)e).ID).UniqueID
                                               )));
        }
    }
}