<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Backend.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Bruger Administration</h2>
    <asp:Panel ID="Panel_List" runat="server" Visible="false">
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th colspan="2"><a href="Users.aspx?action=add" class="btn btn-success btn-xs"><i class="icon-plus"></i>&nbsp;Opret</a></th>
                            <th>Id</th>
                            <th>Navn</th>
                            <th>Email</th>
                            <th>Rolle</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 30px;"><a href="Users.aspx?action=edit&amp;user_id=<%# Eval("user_id") %>" class="btn btn-primary btn-xs"><i class="icon-pencil"></i></a></td>
                    <td style="width: 30px;"><a href="Users.aspx?action=delete&amp;user_id=<%# Eval("user_id") %>" class="btn btn-danger btn-xs"
                        onclick="return confirm('Er du sikker på du vil slette?')"><i class="icon-trash"></i></a></td>
                    <td style="width: 50px;"><%# Eval("user_id") %></td>
                    <td style="width: 200px;"><%# Eval("user_name") %></td>
                    <td><%# Eval("user_email") %></td>
                    <td><%# Eval("role_title") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                <tfoot>
                    <tr>
                        <th colspan="5"><a href="Users.aspx?action=add" class="btn btn-success btn-xs"><i class="icon-plus"></i>&nbsp;Opret</a></th>
                    </tr>
                </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="col-lg-6">

            <div class="form-group">
                <label for="TextBox_Name">Navn</label>
                <asp:TextBox ID="TextBox_Name" runat="server" placeholder="Brugerens navn" CssClass="form-control" MaxLength="32" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Email">Email</label>
                <asp:TextBox ID="TextBox_Email" runat="server" placeholder="Brugernes Email" CssClass="form-control" MaxLength="128" required="required" TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="TextBox_Password">Kodeord</label>
                <asp:TextBox ID="TextBox_Password" runat="server" placeholder="Kodeord" CssClass="form-control" MaxLength="40"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator_Passwords" runat="server" Display="Dynamic" ErrorMessage="<p class='alert alert-danger'>De to kodeord er ikke ens</p>" ControlToValidate="TextBox_Password" ControlToCompare="TextBox_Password_Repeat"></asp:CompareValidator>
            </div>

            <div class="form-group">
                <label for="TextBox_Password_Repeat">Gentag Kodeord</label>
                <asp:TextBox ID="TextBox_Password_Repeat" runat="server" placeholder="Gentag Kodeord" CssClass="form-control" MaxLength="40"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox_Role">Rolle</label>
                <asp:DropDownList ID="DropDownList_Role" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RangeValidator ID="RangeValidator_Role" runat="server" ErrorMessage="<p class='alert alert-danger'>Vælg en rolle</p>" ControlToValidate="DropDownList_Role" MinimumValue="1" MaximumValue="100" Type="Integer"></asp:RangeValidator>
            </div>
            <asp:Button ID="Button_Save" runat="server" CssClass="btn btn-success" Text="Gem" OnClick="Button_Save_Click" />
            <a href="Users.aspx" class="btn btn-default" onclick="return confirm('Er du sikker på du vil annullere?')">Annuller</a>
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

