<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Backend.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Dette er backend forsiden...</h2>

    <h3>Oversigt over hvad du skal lave:</h3>
    <ul>
        <li>
            <h4><a href="Editors.aspx">Redaktør</a> administrationen skal laves</h4>
            <ul>
                <li>En <a href="Editors.aspx">redaktør</a> skal kunne tilføjes som <a href="Editors.aspx">redaktøren</a> til en eller flere <a href="Categories.aspx">kategorier</a>.
                </li>
                <li>En <a href="Editors.aspx">redaktør</a> skal kunne fjernes fra valgte <a href="Categories.aspx">kategorier</a>.
                </li>
            </ul>
        </li>
        <li>
            <h4>Når en <a href="Editors.aspx">redaktør</a> logger på</h4>
            <ul>
                <li>
                    <strong>Nyheds Kategori Menuen</strong> skal afgrænses, til kun at vise de <a href="Categories.aspx">kategorier</a>&nbsp;<a href="Editors.aspx">redaktøren</a> har adgang til.
                </li>
                <li>På <a href="News.aspx?CategoryId=1">nyheds</a> admin siden, skal der tjekkes på om <a href="Editors.aspx">redaktøren</a> har rettighed, til at administrere <a href="News.aspx?CategoryId=1">nyhederne</a> i den pågældende <a href="Categories.aspx">kategori</a>.
                </li>
            </ul>
        </li>

        <li>
            <h4>Nyheder</h4>
            <ul>
                <li>Indsæt en RichTextBox editor på <a href="News.aspx?CategoryId=1">nyheds</a> administrationen
                </li>
            </ul>
        </li>
        <li>
            <h4>Brugere</h4>
            <ul>
                <li>
                    <a href="Users.aspx">Brugernes</a> kodeord skal sikres i databasen med en <strong>HASH</strong> og <strong>SALT</strong>
                </li>
            </ul>
        </li>
        <li>
            <h4>RSS Feed Opgaver</h4>
            <ul>
                <li>Når en <a href="News.aspx?CategoryId=1">nyhed</a> oprettes, rettes eller slettes, skal <a href="Categories.aspx">kategoriens</a> RSS feed opdaters
                </li>
                <li>Når en <a href="Categories.aspx">kategori</a> oprettes, rettes eller slettes, så skal den's RSS feed opdateres.
                </li>
            </ul>
        </li>
        <li>
            <h4>Frontend specifikke opgaver:</h4>
            <ul>
                <li>På frontend, skal der oprettes <strong>Paging</strong> under hver <a href="Categories.aspx">kategori</a>, så der vises 5 nyheder pr side, og man kan bladre rundt imellem siderne i <a href="Categories.aspx">kategorien</a>.
                </li>
                <li>Der skal oprettes et link til <a href="Categories.aspx">kategoriens</a> RSS feed, under hver <a href="Categories.aspx">kategori</a> (skal selvfølgelig først lave RSS feed delen!)
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>

