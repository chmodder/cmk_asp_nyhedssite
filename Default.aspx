<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater ID="Repeater_Frontpage" runat="server">
        <ItemTemplate>
            <section class="news_category">
                <h3><%# Eval("news_title") %></h3>
                <p><a href="News.aspx?category_id=<%# Eval("category_id") %>&amp;news_id=<%# Eval("news_id") %>"><%# Helpers.EvalTrimmed(Eval("news_content").ToString(), 250) %></a></p>
                <em>af: <%# Eval("user_name") %>, i kategorien: <%# Eval("category_title") %>, den. <%# ((DateTime)Eval("news_postdate")).ToString("dd. MMMM yyyy - HH:mm") %></em><hr />
            </section>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

