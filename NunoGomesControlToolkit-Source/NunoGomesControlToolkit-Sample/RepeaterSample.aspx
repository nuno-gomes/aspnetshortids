<%@ Page Language="C#" AutoEventWireup="true" Inherits="NunoGomes.Web.UI.Page" MasterPageFile="~/App_MasterPages/default.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="A">
<script runat="server">
    void Page_Load(object sender, EventArgs args)
    {
        rptSample.DataBind();
    }
</script>
        <h2>
            Repeater Sample.</h2>
        <h3>
            In this sample you can see a Repeater in action.
            <br/>Notice that, because of DynamicControlBuilder used in Repeater.CreateItem, the RepeaterItem control instance type is the one declared in Tag-Mapping.</h3>
        <label for="<%=ddlSample.ClientID %>" >Simple DataSource</label>
        <asp:DropDownList ID="ddlSample" runat="server">
            <asp:ListItem Value="1" Text="Text 1" ></asp:ListItem>
            <asp:ListItem Value="2" Text="Text 2" ></asp:ListItem>
            <asp:ListItem Value="3" Text="Text 3" ></asp:ListItem>
            <asp:ListItem Value="4" Text="Text 4" ></asp:ListItem>
        </asp:DropDownList>
        <br/>
        <label for="<%=rptSample.ClientID %>" >The Repeater</label>
        <asp:Repeater ID="rptSample" runat="server" DataSource='<%# ddlSample.Items %>' >
            <ItemTemplate>
                <asp:TextBox ID="txtRepeater" runat="server" Text='<%# Eval("Text") %>' />
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>
