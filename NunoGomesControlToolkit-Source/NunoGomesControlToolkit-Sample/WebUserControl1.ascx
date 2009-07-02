<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="EnhancedWebApp.WebUserControl1" %>
<asp:TextBox ID="TextBox" runat="server" OnTextChanged="DumpControl" AutoPostBack="true" >1</asp:TextBox>
<asp:Panel ID="Detail" runat="server" CssClass="detail"/>