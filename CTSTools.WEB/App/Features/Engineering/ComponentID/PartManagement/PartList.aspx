<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="PartList.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.PartManagement.PartList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
      <div class="container-fluid">
          <div class="row">
              <ol class="breadcrumb float-xl-start">
                  <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                  <li class="breadcrumb-item"><a href="javascript:;">Component ID</a></li>
                  <li class="breadcrumb-item active"></li>
              </ol>
          </div>
          <!-- END breadcrumb -->
          <!-- BEGIN page-header -->
          <div class="row">
              <h1 class="page-header">Part List</h1>
          </div>
      </div>
      <div class="row">
          <div class="col-xl-12">
              <div class="panel panel-inverse">

                  <div class="panel-body">
                      <div class="row ">
                          <div class="col-12">                                                                
                              <div id="dxPartGrid"></div>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
      </div>
  </div>
  <script type="module" src="/App/Features/Engineering/ComponentID/PartManagement/PartList.js"></script>
</asp:Content>
