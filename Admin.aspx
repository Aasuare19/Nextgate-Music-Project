<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="NextGateMusic.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container" style="max-width: 1250px;">

        
        <asp:HiddenField ID="HiddenField1" runat="server" Value="text"/>
        <input type="hidden" id="hidden" value="old" />

        <nav>
          <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <a class="nav-item nav-link active" id="navhometab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true" onclick="setTab('#nav-home')">Artists</a>
            <a class="nav-item nav-link" id="navprofiletab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false" onclick="setTab('#nav-profile')">Albums</a>
            <!--<a class="nav-item nav-link" id="navcontacttab" data-toggle="tab" href="#nav-contact" role="tab" aria-controls="nav-contact" aria-selected="false" runat="server">Users</a>-->
          </div>
        </nav>

        <div class="tab-content" id="nav-tabContent">
          <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="navhometab">
              <br /><br />
              <div class="container" style="max-width: 1000px;">
                  <asp:Label ID="lblInvalidSinger" runat="server" Text="" Visible="true"></asp:Label>
                  <div class="row">
                    <div class="col">
                        <asp:Label runat="server" Text="First Name"></asp:Label>
                      <input id="tbFirstName" runat="server" type="text" class="form-control">
                    </div>
                    <div class="col">
                        <asp:Label runat="server" Text="Last Name"></asp:Label>
                      <input id="tblLastName" runat="server" type="text" class="form-control">
                    </div>
                  </div>
                  <br />
                  <div class="row">
                    <div class="col">
                        <asp:Label runat="server" Text="Date of Birth"></asp:Label>
                      <input id="dtDob" type="date" class="form-control" runat="server">
                    </div>
                    <div class="col">
                        <asp:Label runat="server" Text="Sex"></asp:Label>
                        <div class="radio">
                            <div class="row d-flex justify-content-between">
                                <div class="col float-sm-right">
                                    <input class="form-check-input" name="sex" type="radio" id="gridCheck1" value="male">
                              <label class="form-check-label" for="gridCheck" style="float-sm-right;">
                                Male
                              </label>
                                </div>
                                <div class="col">
                                     <input class="form-check-input" name="sex" type="radio" id="gridCheck" value="female">
                              <label class="form-check-label" for="gridCheck">
                                Female
                              </label>
                                </div>  
                            </div>  
                        </div>
                    </div>
                  </div>
                  <br />
                  <div class="row d-flex justify-content-end">
                      <asp:Button id="btnAddArtist" runat="server" class="btn btn-secondary" Text="Add Artist" OnClick="btnAddArtist_Click"/>
                  </div>
              </div>
          </div>

          <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="navprofiletab">
              <br /><br />
            <div class="container" style="max-width: 1000px;">
                <asp:Label ID="lblInvalid" runat="server" Text="" Visible="true"></asp:Label>
                  <div class="row">
                      <div class="col-5">
                          <style>
                              .list-group{
                                max-height: 300px;
                                margin-bottom: 10px;
                                overflow:scroll;
                                -webkit-overflow-scrolling: touch;
                            }
                          </style>
                          <asp:Label runat="server" Text="Select an artist:"></asp:Label>
                        <div class="list-group">
                            <asp:Repeater ID="rtrSingerName" runat="server" OnItemCommand="rtrSingerName_ItemCommand">
                                <ItemTemplate>
                                    <asp:Button runat="server" class="list-group-item list-group-item-action" Text='<%# Eval("name") %>' CommandArgument='<%# Eval("name") %>' OnClick="Unnamed_Click"/>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                      <div class="col mr-5">
                          <div class="row">
                              <asp:Label runat="server" Text="Artist:" style="width:90px;"></asp:Label>
                              <asp:Label id="lblSingerName" runat="server" Text="" font-bold="true"></asp:Label>
                          </div>
                          <br />
                          <div class="row">
                              <asp:TextBox id="tbAlbumName" runat="server" class="form-control" placeholder="Album Name"></asp:TextBox>
                          </div>
                          <br />
                          <div class="row">
                              <asp:TextBox id="tbReleaseYear" runat="server" class="form-control" placeholder="Release Year" maxlength="4"></asp:TextBox>
                          </div>
                          <br />
                          <div class="row">
                              <asp:TextBox id="tbRecordCompany" runat="server" class="form-control" placeholder="Record Company"></asp:TextBox>
                          </div>
                          <br />
                          <div class="row d-flex justify-content-end">
                            <asp:Button id="btnAddAlbum" runat="server" class="btn btn-secondary" Text="Add Album" OnClick="btnAddAlbum_Click"/>
                        </div>
                      </div>
                  </div>
                

            </div>

          </div>

          <!--<div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="navcontacttab">
              ...

         </div>-->

    </div>
    </div>

    <script>

        function setTab(linkname) {

            //console.log(linkname);
            //var hidden = document.getElementById("hidden");
            //hidden.setAttribute("value", linkname);

            //var name = hidden.getAttribute("value");
            //console.log(name);

        }

        function uknown(linkname) {
            //$('[href="' + linkname + '"]').tab('show');
        }

        window.onload = function load() {

            //console.log("test");

            //var hidden = document.getElementById("hidden");
            //var linkname = hidden.getAttribute("value");

            //console.log(linkname);

            //$('[href="' + linkname + '"]').tab('show');

            //console.log(tabname);

            //var tab = document.getElementById(tabname);
            //var value = tab.getAttribute("aria-selected");
            //console.log(value);
            //tab.setAttribute("aria-selected", "true");
        }

    </script>
</asp:Content>
