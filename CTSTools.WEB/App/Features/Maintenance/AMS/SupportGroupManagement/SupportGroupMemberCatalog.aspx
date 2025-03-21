<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SupportGroupMemberCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.SupportGroupMember.SupportGroupMemberCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Support Group</a></li>
                <li class="breadcrumb-item active">Support Group Member Catalog</li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Support Group Member Catalog</h1>
        </div>
    </div>
    <!-- END page-header -->
    <div class="row">
        <div class="col-md-4">
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title">Support Group Member Form</h4>
                    <div class="panel-heading-btn">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupportGroupMemberSupportGroupSelectBox"></div>
                            <div class="invalid-feedback" id="SupportGroupMemberSupportGroupValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">User (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupportGroupMemberUserSelectBox"></div>
                            <div class="invalid-feedback" id="SupportGroupMemberUserValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Role (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupportGroupMemberRoleSelectBox"></div>
                            <div class="invalid-feedback" id="SupportGroupMemberRoleValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSupportGroupMemberIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="SupportGroupMemberActionButtons"></div>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title">Support Group Member Information</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                    </div>
                </div>
                <div class="panel-body">
                    <div id="dxSupportGroupMemberGrid"></div>
                </div>
            </div>

        </div>
    </div>
</div>
<input type="hidden" id="hiddenSupportGroupMemberID" hidden />
<script type="module" src="/App/Features/Maintenance/AMS/SupportGroupManagement/SupportGroupMemberCatalog.js?v=1"></script>
</asp:Content>
