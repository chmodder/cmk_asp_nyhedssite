<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="rssfeeds.aspx.cs" Inherits="rssfeeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="rss_category">
        <%--    <ul>
        <asp:Repeater ID="FeedListRpt" runat="server" DataSourceID="RssCatSqlDs">
            <ItemTemplate>
                <section class="rss_category">
                    <li><a href='<%# "App_Data/Rss/" + Eval("category_title") + ".xml"%>'><span class="label label-info"><%#Eval("category_title") %></span></a></li>
                </section>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource runat="server" ID="RssCatSqlDs" ConnectionString='<%$ ConnectionStrings:cmk_asp_nyhedssite %>' SelectCommand="SELECT [category_title] FROM [categories] ORDER BY [category_title]"></asp:SqlDataSource>
    </ul>--%>

        <ul>
            <asp:Literal ID="RssListLtl" runat="server"></asp:Literal>
        </ul>
    </section>
</asp:Content>

