<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Backend.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Admin_Categories" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Kategori Administration</h2>
    <asp:Panel ID="Panel_List" runat="server" Visible="false">
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th colspan="2"><a href="Categories.aspx?action=add" class="btn btn-success btn-xs"><i class="icon-plus"></i>&nbsp;Opret</a></th>
                            <th>Id</th>
                            <th>Titel</th>
                            <th>Beskrivelse</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 30px;"><a href="Categories.aspx?action=edit&amp;category_id=<%# Eval("category_id") %>" class="btn btn-primary btn-xs"><i class="icon-pencil"></i></a></td>
                    <td style="width: 30px;"><a href="Categories.aspx?action=delete&amp;category_id=<%# Eval("category_id") %>" class="btn btn-danger btn-xs"
                        onclick="return confirm('Er du sikker på du vil slette?')"><i class="icon-trash"></i></a></td>
                    <td style="width: 50px;"><%# Eval("category_id") %></td>
                    <td style="width: 200px;"><%# Eval("category_title") %></td>
                    <td><%# Eval("category_description") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                <tfoot>
                    <tr>
                        <th colspan="5"><a href="Categories.aspx?action=add" class="btn btn-success btn-xs"><i class="icon-plus"></i>&nbsp;Opret</a></th>
                    </tr>
                </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>


    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="col-lg-6">
            <div class="form-group">
                <label for="TextBox_Title">Kategori Titel</label>
                <asp:TextBox ID="TextBox_Title" runat="server" placeholder="Kategori Titel" CssClass="form-control" MaxLength="32" required="required"></asp:TextBox>
            </div>
            <div class="form-group ">
                <label for="TextBox_Description">Beskrivelse</label>
                <asp:TextBox ID="TextBox_Description" runat="server" placeholder="Beskrivelse" CssClass="form-control" MaxLength="200"></asp:TextBox>
            </div>
            <asp:Button ID="Button_Save" runat="server" CssClass="btn btn-success" Text="Gem" OnClick="Button_Save_Click" />
            <a href="Categories.aspx" class="btn btn-default" onclick="return confirm('Er du sikker på du vil annullere?')">Annuller</a>
        </div>
    </asp:Panel>

    <div class="clearfix"></div>

    <asp:Panel ID="Panel_Error" Visible="false" runat="server">
        <div class="alert alert-danger alert-dismissable">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <p>
                <asp:Label ID="Label_Error" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </asp:Panel>
</asp:Content>

