<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="MassivePartNumberCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.MassiveManagement.MassivePartNumberCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <!-- BEGIN breadcrumb -->
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-end">
                <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Massive part number</a></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Massive part number</h1>
        </div>
        <!-- END page-header -->
    </div>
    <!-- BEGIN Module Catalog -->
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div id="file-uploader"></div>
                    <%--<div id="dxModuleGrid"></div>--%>
                </div>
            </div>
        </div>
    </div>
    <!-- END Module Catalog -->
</div>
<script type="module" src="/App/Features/AdvancedSettings/MassiveManagement/MassivePartNumberCatalog.js"></script>
</asp:Content>
