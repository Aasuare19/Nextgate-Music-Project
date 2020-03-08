<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="NextGateMusic.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="width: 750px;">
        <div class="form-row">
            <div class="col align-content-center">
                <asp:Label ID="lblInvalidSignup" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-group">
            <asp:TextBox id ="tbUsername" class="form-control" runat="server" placeholder="Enter a username"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:TextBox id ="tbPassword" class="form-control" runat="server" placeholder="Enter a password" textmode="password"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:TextBox id ="tbPassword2" class="form-control" runat="server" placeholder="Re-enter password" textmode="password"></asp:TextBox>
        </div>

        <br />
        <div class="d-flex justify-content-end">
            <asp:Button ID="btnSignup" class="btn btn-secondary" runat="server" Text="Sign Up" style="width: 100px;" OnClick="btnSignup_Click"/>
        </div>
     </div>

</asp:Content>
