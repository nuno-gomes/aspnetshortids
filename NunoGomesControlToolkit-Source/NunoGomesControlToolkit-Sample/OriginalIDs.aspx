<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/OriginalIDs.aspx.cs" Inherits="OriginalIDs"  MasterPageFile="~/App_MasterPages/default.Master" %>
<%@ Register TagPrefix="uc" Src="WebUserControl1.ascx" TagName="SampleWebUserControl1" %>
<%@ Register TagPrefix="uc" Src="WebUserControl2.ascx" TagName="SampleWebUserControl2" %>
<%@ Register TagPrefix="uc" Src="WebUserControl3.ascx" TagName="SampleWebUserControl3" %>
<%@ Register TagPrefix="uc" Src="WebUserControl4.ascx" TagName="SampleWebUserControl4" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="A">
        <h2>This page was decored with KeepOriginalIDs and this way all controls will preserve their original IDs.</h2>
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
</asp:Content>