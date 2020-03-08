<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NextGateMusic.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div class="container float-sm-right">
        <div class="form-row">
            <div class="col align-content-center">
                <asp:Label ID="lblInvalidSearch" runat="server" Text="Please enter an artist or album name to search" Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <br />
        <div class="form-row justify-content-center">
            <div class="col-8">
                <asp:TextBox ID="tbSearchText" class="form-control" runat="server" placeholder="Enter an artist or album name" ></asp:TextBox>
            </div>
            <div class="col">
                <asp:Button ID="btnSearch" class="btn btn-secondary" runat="server" Text="Search" OnClick="btnSearch_Click" style="width: 150px"/>
            </div>
        </div>
    </div>

    <br /><br /><br />
    <br /><br />
    
    <div class="container">
        <asp:GridView ID="gvSearchResults" class="table table-striped" runat="server" EmptyDataText="No search results to display"></asp:GridView>
    </div>

</asp:Content>
