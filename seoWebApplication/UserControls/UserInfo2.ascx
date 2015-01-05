<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo2.ascx.cs" Inherits="seoWebApplication.UserControls.UserInfo2" %>
 <%if (!Page.User.Identity.IsAuthenticated)
   { %>
 <a href="/Account/Login/" id="mk-header-login-button" class="mk-login-link mk-toggle-trigger"><i class="fa fa-user"></i>Login</a>
 or
<asp:HyperLink runat="server" ID="registerLink"
NavigateUrl="~/Account/Register/" Text="Register"
ToolTip="Go to the registration page"/> 
<%}
  else
  { %> 

<asp:LoginName ID="LoginName2" runat="server" FormatString="Hello, <b>{0}</b>!" />
 
<asp:HyperLink runat="server" ID="HpLogout" NavigateUrl="~/Account/Logout/"
Text="Log Out"
ToolTip="Log Out" />
<asp:HyperLink runat="server" ID="detailsLink" NavigateUrl="~/Account/Manage/"
Text="My Account"
ToolTip="Edit your personal details" />
<%} %>