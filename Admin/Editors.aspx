<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Backend.master" AutoEventWireup="true" CodeFile="Editors.aspx.cs" Inherits="Admin_Editors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Redaktør Administration</h2>
    <div class="row">
        <div class="col-xs-5">

            <div class="row">
                <div class="col-xs-5">
                    <asp:DropDownList ID="CatDdl" runat="server" DataSourceID="CatSqlDs" CssClass="form-control" DataTextField="category_title" DataValueField="category_id">
                    </asp:DropDownList>




                    <asp:SqlDataSource runat="server" ID="CatSqlDs" ConnectionString='<%$ ConnectionStrings:cmk_asp_nyhedssite %>' SelectCommand="
                SELECT [category_id], [category_title] FROM [categories]"></asp:SqlDataSource>
                </div>

                <div class="col-xs-7">
                    <asp:LinkButton ID="CatSelectBtn" CssClass="btn btn-primary" runat="server" OnClick="CatSelectBtn_Click">Vælg</asp:LinkButton>
                </div>
            </div>

        </div>
        <div class="col-xs-7">
        </div>
    </div>

    <div class="row">
        <div class="col-xs-5">
            <h2>Ikke Redaktører</h2>
        </div>

        <div class="col-xs-2">
        </div>

        <div class="col-xs-5">
            <h2>Redaktører</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-5">
            <asp:ListBox ID="ListBox_NotEditors" runat="server" class="form-control" DataTextField="user_name"
                DataValueField="user_id" SelectionMode="Multiple"></asp:ListBox>
        </div>

        <div class="col-xs-2">

            <asp:LinkButton ID="MoveBtn" CssClass="btn btn-success btn-block" runat="server" OnClick="MoveBtn_Click"><span class="glyphicon glyphicon-transfer"></span></asp:LinkButton>
            <br />
            <asp:LinkButton ID="DeselectBtn" CssClass="btn btn-block btn-default" runat="server" OnClick="DeselectBtn_Click"><span class="glyphicon glyphicon-repeat"></span></asp:LinkButton>

        </div>


        <div class="col-xs-5">

            <asp:ListBox ID="ListBox_Editors" runat="server" class="form-control" DataTextField="user_name"
                DataValueField="user_id" SelectionMode="Multiple"></asp:ListBox>
        </div>
    </div>







</asp:Content>

