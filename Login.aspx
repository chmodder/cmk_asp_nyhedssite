<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Frontend.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-5 form-horizontal">
            <div class="form-group">
                <label for="TextBox_Email" class="col-lg-2 control-label">Email</label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TextBox_Email" runat="server" CssClass="form-control" TextMode="Email" required autofocus Text="bb@cmk-dynamisk-web.dk" ValidationGroup="Login"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="TextBox_Password" class="col-lg-2 control-label">Kodeord</label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TextBox_Password" runat="server" TextMode="Password" CssClass="form-control" placeholder="1234" required ValidationGroup="Login"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-offset-2 col-lg-10">
                    <asp:Button ID="Button_Login" runat="server" CssClass="btn btn-default" Text="Login" OnClick="Button_Login_Click" ValidationGroup="Login" />
                    <asp:Label ID="Label_Errors" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
                 </div>
            </div>
        </div>
    </div>

    <div class="row bottom">
        <ul class="breadcrumb">
            <li>
                <a href="Default.aspx">Forside</a>
            </li>
            <li class="active">
                Login
            </li>
        </ul>
    </div>
</asp:Content>

