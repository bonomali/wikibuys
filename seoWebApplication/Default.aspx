<%@ Page Language="C#" MasterPageFile="~/default2.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="seoWebApplication._Default" Title="WikiBuys.com" %>
 
<%@ Register src="UserControls/ProductsList.ascx" tagname="ProductsList" tagprefix="uc1" %>
<%@ Register src="UserControls/Pager.ascx" tagname="Pager" tagprefix="uc2" %>  
<%@ Register src="UserControls/AccordianRestaurantHours.ascx" tagname="AccordianRestaurantHours" tagprefix="uc4" %>
<%@ Register src="UserControls/RestaurantMap.ascx" tagname="RestaurantMap" tagprefix="uc3" %>
<%@ Register Src="~/UserControls/RestaurantReviews.ascx" TagPrefix="uc1" TagName="RestaurantReviews" %>
<%@ Register Src="~/UserControls/RestaurantReviewStars.ascx" TagPrefix="uc1" TagName="RestaurantReviewStars" %>
<%@ Register Src="~/UserControls/ProductsList.ascx" TagPrefix="uc2" TagName="ProductsList" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> 
       <!-- Open Graph data --> 
<meta property="og:type" content="article" />
<meta property="og:title" content="<%=seoTitle + " at " + storeName%>"/>
<meta property="og:image" content="<%= "" + host + imgLogo%>"/>
<meta property="og:site_name" content="<%= "" + storeName %>"/>
<meta property="og:url" content="<%= "" + url %>" />
<meta property="og:description" content="<%= "" + seoDesc %>"/> 
<meta property="og:price:currency" content="USD" />
<meta name="google-site-verification" content="FhvokD0LxjumaUY73P_DvMermMZp6v0fJDy0xTIPIlk" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="paperShadow"> 
<asp:Label ID="catalogTitleLabel" CssClass="CatalogTitle" runat="server" />
</div>


		 <asp:Repeater ID="list" runat="server" OnItemCreated="R1_ItemCreated">   
             <itemtemplate>   
                 <asp:Literal ID="lblDivStart" runat="server"></asp:Literal>
                      <div class="large-4 columns">
			                <div class="work-item" data-project-id="project-1">
				                <div class="work-img-holder">
					                <p align="center"><a href="<%# Link.ToProduct(Eval("product_id").ToString()) %>">
                                        <%# Eval("name").ToString().Length > 30 ? HttpUtility.HtmlEncode(Eval("name").ToString().Trim().Substring(0,30)) : HttpUtility.HtmlEncode(Eval("name").ToString().Trim())%>
                                       
                                        </a>
					                </p>
                                    <a href="<%# Link.ToProduct(Eval("product_id").ToString()) %>">
                                    <img width="225" border="0"
                                    src="<%# Link.ToProductImage(Eval("thumbnail").ToString()) %>"
                                    alt='<%# HttpUtility.HtmlEncode(Eval("name").ToString())%>' class="product-image" />
                                    </a>
                                    <span class="label label-info price"><%# Eval("price", "{0:c}") %></span> 
                                    <div class="caption"> 
                                    <div class="description-block">
                                    <%# HttpUtility.HtmlEncode(Eval("description").ToString().Trim().Substring(0,60) + "(...)") %>         
                                    </div> 
			                        </div>
                                    <a href='<%# Link.ToProduct(Eval("product_id").ToString()) %>' class="button [tiny small large radius round]">Details</a> 
				                </div>
			                </div>
	                  </div> 
                 <asp:Literal ID="lblDivEnd" runat="server"></asp:Literal>
            </itemtemplate>
         </asp:Repeater> 
	 
<asp:Label ID="catalogDescriptionLabel" CssClass="CatalogDescription" runat="server" /> 
     
</asp:Content>