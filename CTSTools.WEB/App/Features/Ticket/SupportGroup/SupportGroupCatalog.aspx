<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SupportGroupCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.SupportGroup.SupportGroupCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                <li class="breadcrumb-item active">Support Group Catalog</li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Support Group Catalog</h1>
        </div>
    </div>
    <!-- END page-header -->
    <div class="row">
        <div class="col-md-4">
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title">Support Group Form</h4>
                    <div class="panel-heading-btn">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">English Name(<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxSupportGroupEnglishNameTextBox"></div>
                            <div class="invalid-feedback" id="SupportGroupEnglishNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Spanish Name(<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxSupportGroupSpanishNameTextBox"></div>
                            <div class="invalid-feedback" id="SupportGroupSpanishNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Facility (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxSupportGroupFacilitySelectBox"></div>
                            <div class="invalid-feedback" id="SupportGroupFacilityValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Default Station (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxSupportGroupStationSelectBox"></div>
                            <div class="invalid-feedback" id="SupportGroupStationValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-12">Description</label>
                        <div class="col-xl-8 col-md-12">
                            <div id="dxSupportGroupDescriptionTextArea"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-4 col-md-auto">Is Active?</label>
                        <div class="col-xl-8 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSupportGroupIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="SupportGroupActionButtons"></div>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="panel panel-inverse">
                <div class="panel-heading">
                    <h4 class="panel-title">Support Group Information</h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                    </div>
                </div>
                <div class="panel-body">
                    <div id="dxSupportGroupGrid"></div>
                </div>
            </div>

        </div>
    </div>
</div>
<input type="hidden" id="hiddenSupportGroupID" hidden />
<script type="module" src="/App/Features/Ticket/SupportGroup/SupportGroupCatalog.js"></script>
</asp:Content>
