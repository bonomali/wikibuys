<%@ Page Language="C#" MasterPageFile="~/defaultproduct.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="seoWebApplication.Product" Title="Product Details Page" %>

<%@ Register src="UserControls/ProductRecommendations.ascx" tagname="ProductRecommendations" tagprefix="uc1" %>

<%@ Register src="UserControls/ProductAttributes.ascx" tagname="ProductAttributes" tagprefix="uc2" %>

<%@ Register src="UserControls/ProductAttributesRadio.ascx" tagname="ProductAttributesRadio" tagprefix="uc3" %>

<%@ Register src="UserControls/ProductCustomAttributes.ascx" tagname="ProductCustomAttributes" tagprefix="uc4" %>
<%@ Register Src="~/UserControls/contact.ascx" TagPrefix="uc1" TagName="contact" %>
 
<%@ Register Src="~/UserControls/PicturesModals.ascx" TagPrefix="uc1" TagName="PicturesModals" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <!-- Open Graph data --> 
<meta property="og:type" content="article" />
<meta property="og:title" content="<%=seoTitle + " at " + storeName%>"/>
<meta property="og:image" content="<%= "" + host + imgLogo%>"/>
<meta property="og:site_name" content="<%= "" + storeName %>"/>
<meta property="og:url" content="<%= "" + url %>" />
<meta property="og:description" content="<%= "" + seoDesc %>"/>
<meta property="og:price:amount" content="<%= "" + price %>" />
<meta property="og:price:currency" content="USD" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderSoMed" runat="Server"> 
    <div class="fb-like" data-href="<%= "" + fbUrl %>" data-layout="standard" data-action="like" data-show-faces="true" data-share="true"></div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 

    <section class="small-12 columns" data-id="1" id="explore">
        <header class="text-center">
        <h1><asp:Label CssClass="ProductTitle" ID="titleLabel" runat="server" Text="Label"></asp:Label></h1> 
        </header>
    <section class="row">
            <div style="width:50%;float:left;">
            <div class="fb-like" data-href="<%= "" + url %>" data-layout="standard" data-action="like" data-show-faces="true" data-share="true"></div>
            <asp:Image ID="productImage" runat="server" Height="75%" Width="100%" CssClass="img-thumbnail" />
            </div>
            <div style="width:50%;float:right;">
            <asp:Label ID="descriptionLabel" runat="server" Text="Label"></asp:Label> <br />
                <br />
                <br />
            <asp:CheckBox ID="isActive" runat="server" />       
            <right><asp:Label CssClass="ProductPrice" ID="priceLabel" runat="server" Text=""></asp:Label></right>
            <br />  
            <uc4:ProductCustomAttributes ID="ProductCustomAttributes1" runat="server" />
            <br /> 
            <uc2:ProductAttributes ID="ProductAttributes1" runat="server" /> 
            <br />
            <uc3:ProductAttributesRadio ID="ProductAttributesRadio1" runat="server" /> 
            </div>  
            <uc1:PicturesModals runat="server" id="PicturesModals" /> 
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"></asp:Button>
    </section>
     

    </section>
   
   
</asp:Content>
