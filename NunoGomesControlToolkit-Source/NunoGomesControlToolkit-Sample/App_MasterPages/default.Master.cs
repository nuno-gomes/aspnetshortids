using System;
using System.Web.UI.WebControls;
using System.Globalization;

namespace WebApp.App_MasterPages
{
    public partial class _default : NunoGomes.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MyLinkButton_Click(object sender, EventArgs e)
        {
            //put a breakpoint here
            LinkButton eventSource = sender as LinkButton;
            if (eventSource != null)
            {
                int i;
                if(int.TryParse(eventSource.CommandArgument, out i))
                {
                    eventSource.Text = string.Format("I've been pressed {0} times. Press me again!!", ++i);
                    eventSource.CommandArgument = Convert.ToString(i, CultureInfo.InvariantCulture);
                }
            }
        }
    }
}