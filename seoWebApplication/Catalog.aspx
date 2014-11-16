<%@ Page Language="C#" MasterPageFile="~/defaultproduct.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="seoWebApplication.Catalog" Title="MyDinner2Go: Online Ordering" %>

<%@ Register Src="UserControls/ProductsList.ascx" TagName="ProductsList" TagPrefix="uc1" %> 
<%@ Register Src="UserControls/DeptCategoriesList.ascx" TagName="DeptCategoriesList" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
          <div class="row">
		<div class="large-12 columns text-center">
			<div class="page-header pad-bottom">
				<h2><asp:Label ID="catalogTitleLabel" CssClass="CatalogTitle" runat="server" /></h2>
				<div class="hr large-1 small-3"> <asp:Label ID="catalogDescriptionLabel" CssClass="CatalogDescription"
                        runat="server" /></div>
			</div>
		</div>
	</div> 
     
          <asp:Repeater ID="list" runat="server" OnItemCreated="R1_ItemCreated">   
             <itemtemplate>   
                 <asp:Literal ID="lblDivStart" runat="server"></asp:Literal>
                      <div class="large-4 columns">
			                <div class="work-item" data-project-id="project-1">
				                <div class="work-img-holder">
					                <p align="center"><a href="<%# Link.ToProduct(Eval("product_id").ToString()) %>">
                                        <%# HttpUtility.HtmlEncode(Eval("name").ToString()) %>
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
                                    <%# HttpUtility.HtmlEncode(Eval("description").ToString()) %>         
                                    </div> 
			                        </div>
                                    <a href='<%# Link.ToProduct(Eval("product_id").ToString()) %>' class="btn btn-block">Details</a> 
				                </div>
			                </div>
	                  </div> 
                 <asp:Literal ID="lblDivEnd" runat="server"></asp:Literal>
            </itemtemplate>
         </asp:Repeater>
    
          <div class="row">
		<div class="large-12 columns text-center">
			<div class="page-header pad-bottom">
			  <uc1:Pager runat="server" id="Pager2" />
			</div>
		</div>
	</div>
            
</asp:Content>
 