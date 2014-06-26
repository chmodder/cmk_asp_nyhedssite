<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <a href='rssfeeds.aspx'><span class="label label-info">RSS</span></a>
    
    <asp:Literal ID="CatLtl" runat="server"></asp:Literal>

    <hr />

    <asp:Repeater ID="Repeater_Category" runat="server">
        <ItemTemplate>
            <section class="news_category">
                <h3><%# Eval("news_title") %></h3>
                <p>
                    <a href="News.aspx?category_id=<%# Eval("category_id") %>&amp;news_id=<%# Eval("news_id") %>"><%# Helpers.EvalTrimmed(Eval("news_content").ToString(), 250) %></a>
                </p>
                <em><%# Eval("user_name") %> - <%# ((DateTime)Eval("news_postdate")).ToString("dd. MMMM yyyy - HH:mm") %></em><hr />
            </section>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Literal ID="PagerLtl" runat="server"></asp:Literal>

    <div class="bottom">
        <ul class="breadcrumb">
            <li><a href="Default.aspx">Forside</a></li>
            <li class="active">
                <asp:Literal ID="Literal_BreadCrumb_CategoryTitle" runat="server"></asp:Literal></li>
        </ul>
    </div>
</asp:Content>

