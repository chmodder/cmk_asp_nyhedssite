<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1><asp:Literal ID="Literal_NewsTitle" runat="server"></asp:Literal></h1>
    <asp:Literal ID="Literal_NewsContent" runat="server"></asp:Literal><br /><br />
    <em class="text-muted">Skrevet af: <asp:Label ID="Label_UserName" runat="server"></asp:Label>, den: <asp:Label ID="Label_NewsPostdate" runat="server"></asp:Label></em>

    <div class="bottom">
        <ul class="breadcrumb">
            <li><a href="Default.aspx">Forside</a></li>
            <li><asp:HyperLink ID="HyperLink_BreadCrumb_Category" runat="server"></asp:HyperLink></li>
            <li class="active"><asp:Literal ID="Literal_BreadCrumb_NewsTitle" runat="server"></asp:Literal></li>
        </ul>
    </div>
</asp:Content>

