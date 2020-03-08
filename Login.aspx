<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NextGateMusic.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="container justify-content-center float-sm-right">
        <div class="form-row">
            <div class="col align-content-center">
                <asp:Label ID="lblInvalidLogin" runat="server" Text="Invalid username or password" Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col">
                <asp:TextBox ID="tbUsername" class="form-control" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox ID="tbPassword" class="form-control" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
            <div class="col">
                <asp:Button ID="btnLogin" class="btn btn-secondary" runat="server" Text="Login" OnClick="btnLogin_Click" style="width: 125px"/>
            </div>
        </div>
    </div>
</asp:Content>
