<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl3.ascx.cs" Inherits="EnhancedWebApp.WebUserControl3" %>
<%@ Register TagPrefix="nc" Namespace="NunoGomes.Web.UI.WebControls" Assembly="NunoGomesControlToolkit" %>
<nc:TextBox ID="TextBox" runat="server" OnTextChanged="DumpControl" AutoPostBack="true" >3</nc:TextBox>
<asp:Panel ID="Detail" runat="server" CssClass="detail" />