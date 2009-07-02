<%@ Page Language="C#" AutoEventWireup="true" Inherits="NunoGomes.Web.UI.Page" %>
<script runat="server">
    void Page_Load(object sender, EventArgs args)
    {
        rptSample.DataBind();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Repeater Sample</title>
    <style type="text/css">
        BODY, P, TD { font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10pt }
        H2,H3,H4,H5 { color: #ff9900; font-weight: bold; }
        H2 { font-size: 13pt; }
        H3 { font-size: 12pt; }
        H4 { font-size: 10pt; color: black; }
        .detail { BACKGROUND-COLOR: #FBEDBB; FONT-FAMILY: "Courier New", Courier, mono; WHITE-SPACE: pre; }
        CODE { COLOR: #990000; FONT-FAMILY: "Courier New", Courier, mono; }
    </style>
</head>
<body>
    <html:HtmlForm id="form1" runat="server">
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
    </html:HtmlForm>
</body>
</html>
