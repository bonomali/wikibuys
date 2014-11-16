<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogIn.ascx.cs" Inherits="seoWebApplication.UserControls.LogIn" %>
 
<span class="form-section">
<label for="log">Email</label> 
<asp:TextBox ID="Email" runat="server" CssClass="text-input" TextMode="Email"></asp:TextBox>
</span>
<span class="form-section">
<label for="pwd">Password</label>
<asp:TextBox ID="Password" runat="server" CssClass="text-input" TextMode="Password"></asp:TextBox> 
</span>
<label class="mk-login-remember">
    <asp:CheckBox ID="RememberMe" runat="server" /> Remember Me </label>
<asp:Button ID="btnLogin" runat="server" Text="LOG IN" CssClass="shop-flat-btn shop-skin-btn" OnClick="btnLogin_Click" />
<div class="register-login-links">
<a href="#" class="mk-forget-password">Forget?</a>
</div>
