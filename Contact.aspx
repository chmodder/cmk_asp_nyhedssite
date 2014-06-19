<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>Skriv en besked til os, og vi vil vende tilbage snarest muligt. </p>
    <div class="row">
        <fieldset class=" col-lg-4">
            <div class="form-group">
                <label for="TextBox_Name">Dit Navn</label>
                <asp:TextBox ID="TextBox_Name" runat="server" placeholder="Skriv dit navn" autofocus required CssClass="form-control" ValidationGroup="contact"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Email">Din Email Adresse</label>
                <asp:TextBox ID="TextBox_Email" runat="server" TextMode="Email" placeholder="Skriv din email" CssClass="form-control" ValidationGroup="contact" pattern="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Topic">Emne</label>
                <asp:TextBox ID="TextBox_Topic" runat="server" CssClass="form-control" placeholder="Skriv emne" ValidationGroup="contact" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Message">Besked</label>
                <asp:TextBox ID="TextBox_Message" TextMode="MultiLine" runat="server" CssClass="form-control" ValidationGroup="contact" placeholder="Skriv din besked" required></asp:TextBox>

            </div>
            <div class="form-group">
                <asp:Button ID="Button_Contact" CssClass="btn btn-default" Text="Send" runat="server" ValidationGroup="contact" OnClick="Button_Contact_Click" />
            </div>
        </fieldset>
    </div>

    <div class="bottom">
        <ul class="breadcrumb">
            <li>
                <a href="Default.aspx">Forside</a>
            </li>
            <li class="active">
                Kontakt
            </li>
        </ul>
    </div>
</asp:Content>

