<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/OriginalIDs.aspx.cs" Inherits="OriginalIDs"  %>
<%@ Register TagPrefix="uc" Src="WebUserControl1.ascx" TagName="SampleWebUserControl1" %>
<%@ Register TagPrefix="uc" Src="WebUserControl2.ascx" TagName="SampleWebUserControl2" %>
<%@ Register TagPrefix="uc" Src="WebUserControl3.ascx" TagName="SampleWebUserControl3" %>
<%@ Register TagPrefix="uc" Src="WebUserControl4.ascx" TagName="SampleWebUserControl4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
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
    <form id="form1" runat="server">
        <h2>
            Change TextBox content to get detailed control info.</h2>
        <div>
            ASPNET User Control with ASPNET TextBox Control:
            <uc:SampleWebUserControl1 ID="WebUserControl1" runat="server" />
            <br />
            New User Control with ASPNET TextBox Control:
            <uc:SampleWebUserControl2 ID="WebUserControl2" runat="server" />
            <br />
            New User Control with New TextBox Control (with specified ID property):
            <uc:SampleWebUserControl3 ID="WebUserControl3" runat="server" />
            <br />
            New User Control with New TextBox Control (without specified ID property):
            <uc:SampleWebUserControl4 ID="WebUserControl4" runat="server" />
        </div>
    </form>
</body>
</html>