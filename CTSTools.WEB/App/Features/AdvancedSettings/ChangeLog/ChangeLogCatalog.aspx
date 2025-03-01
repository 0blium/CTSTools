<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ChangeLogCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.ChangeLog.ChangeLogCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <!-- BEGIN breadcrumb -->
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                <li class="breadcrumb-item active">Change Log History</li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Change Log History</h1>
        </div>
        <!-- END page-header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body">
                    <div>
                        <a id="UpdateChangelogbtn" class="btn btn-success"><i class="fas fa-refresh me-2"></i>Update change log records</a>
                    </div>
                    <div id="dxChangelogDataGrid"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="module" src="ChangeLog.js?v=1"></script>
</asp:Content>
