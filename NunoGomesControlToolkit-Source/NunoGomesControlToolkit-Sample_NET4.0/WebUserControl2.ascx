<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl2.ascx.cs" Inherits="EnhancedWebApp.WebUserControl2" %>
<asp:TextBox ID="TextBox" runat="server" OnTextChanged="DumpControl" AutoPostBack="true" >2</asp:TextBox>
<asp:Panel ID="Detail" runat="server" CssClass="detail" />