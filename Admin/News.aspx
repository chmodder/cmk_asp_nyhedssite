<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Backend.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Admin_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Nyheds Administration</h2>
    <asp:Panel ID="Panel_List" runat="server" Visible="false">
        <table class="table table-striped table-hover">
            <thead>
                <tr>
                    <th colspan="2">
                        <asp:HyperLink ID="HyperLink_Add_Header" runat="server" CssClass="btn btn-success btn-xs"><i class="icon-plus"></i>
                        &nbsp;Opret</asp:HyperLink></th>
                    <th>Id</th>
                    <th>Titel</th>
                    <th>Udgivelses Dato</th>
                    <th>Forfatter</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater_List" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 30px;"><a href="News.aspx?action=edit&amp;category_id=<%# Eval("fk_categories_id") %>&amp;news_id=<%# Eval("news_id") %>" class="btn btn-primary btn-xs"><i class="icon-pencil"></i></a></td>
                            <td style="width: 30px;"><a href="News.aspx?action=delete&amp;category_id=<%# Eval("fk_categories_id") %>&amp;news_id=<%# Eval("news_id") %>" class="btn btn-danger btn-xs"
                                onclick="return confirm('Er du sikker på du vil slette?')"><i class="icon-trash"></i></a></td>
                            <td style="width: 50px;"><%# Eval("news_id") %></td>
                            <td style="width: 300px;"><%# Eval("news_title") %></td>
                            <td><%# Eval("news_postdate") %></td>
                            <td><%# Eval("user_name") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <th colspan="6">
                        <asp:HyperLink ID="HyperLink_Add_Footer" runat="server" CssClass="btn btn-success btn-xs"><i class="icon-plus"></i>
                        &nbsp;Opret</asp:HyperLink></th>

                </tr>
            </tfoot>
        </table>
    </asp:Panel>


    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="col-lg-12">
            <div class="form-group">
                <label for="TextBox_Title">Nyheds Titel</label>
                <asp:TextBox ID="TextBox_Title" runat="server" placeholder="Nyheds Titel" CssClass="form-control" MaxLength="32" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Description">Nyheds Tekst</label>
                <asp:TextBox ID="editor1" runat="server" placeholder="Nyheds Tekst" CssClass="ckeditor form-control" ClientIDMode="Static" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="DropDownList_Category">Kategori</label>
                <asp:DropDownList ID="DropDownList_Category" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <asp:Button ID="Button_Save" runat="server" CssClass="btn btn-success" Text="Gem" OnClick="Button_Save_Click" />
            <asp:HyperLink ID="HyperLink_Cancel" runat="server" CssClass="btn btn-default" onclick="return confirm('Er du sikker på du vil annullere?')" Text="Annuller"></asp:HyperLink>
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

