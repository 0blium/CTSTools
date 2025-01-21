<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ModuleCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Module.ModuleCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;"></a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Modules</h1>
            </div>
            <!-- END page-header -->
        </div>
        <!-- BEGIN Module Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="NewModuleBtn" href="#SaveModuleRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Module</a>
                        </div>
                        <div id="dxModuleGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END Module Catalog -->
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveModuleRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="ModuleModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseModuleModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxModuleNameTextBox"></div>
                            <div class="invalid-feedback" id="ModuleNameValidation"></div>
                        </div>
                    </div>
                    
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Description</label>
                        <div class="col-md-12">
                            <div id="dxModuleDescription"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-auto">Is Active?</label>
                        <div class="col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxModuleIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="ModuleActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenModuleID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/SecurityManagement/ModuleCatalog.js"></script>
</asp:Content>
