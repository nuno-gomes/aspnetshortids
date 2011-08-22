<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl4.ascx.cs" Inherits="EnhancedWebApp.WebUserControl4" %>
<%@ Register TagPrefix="nc" Namespace="NunoGomes.Web.UI.WebControls" Assembly="NunoGomesControlToolkit" %>
<nc:TextBox runat="server" OnTextChanged="DumpControl" AutoPostBack="true" >4</nc:TextBox>
<asp:Panel ID="Detail" runat="server" CssClass="detail" />