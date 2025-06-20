<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ModuleCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Module.ModuleCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
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
        <ul class="nav nav-pills mb-2" role="tablist">
            <li class="nav-item" role="presentation">
                <a href="#module-list-tab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                    <span class="d-sm-none">List</span>
                    <span class="d-sm-block d-none">List</span>
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a href="#module-setup-tab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                    <span class="d-sm-none">Set up</span>
                    <span class="d-sm-block d-none">Setup</span>
                </a>
            </li>
        </ul>
        <!-- BEGIN Module Catalog -->
        <div class="tab-content  rounded-0 m-0">
            <!-- BEGIN tab-pane -->
            <div class="tab-pane fade active show" id="module-list-tab" role="tabpanel">
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
            </div>
            <!-- END tab-pane -->
            <!-- BEGIN tab-pane -->
            <div class="tab-pane fade " id="module-setup-tab" role="tabpanel">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-body">
                                <div class="col-md-12 ">
                                    <a id="NewModuleSetUpBtn" href="#SaveModuleSetupModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Setup</a>
                                </div>
                                <div id="dxModuleSetupGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END tab-pane -->
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
    <!-- Modal save setup -->
    <div class="modal fade" id="SaveModuleSetupModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="ModuleSetUpModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseModuleSetupModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxModuleSetupNameTextBox"></div>
                            <%--<div class="invalid-feedback" id="ModuleNameValidation"></div>--%>
                        </div>
                    </div>

                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3  col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxModuleSetupDescription"></div>
                        </div>
                    </div>

                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3  col-md-12">Actions <span id="ActionRequire" hidden>(<span class="text-danger">*</span>)</span></label>
                        <div class="col-xl-9 col-md-12">                            
                                <div type="text" id="dxModuleSetupActionsTagBox"></div>
                            <%--<div class="mt-2 mb-2 d-flex justify-content-between">
                                <div id="dxModulePermissionReadCheckBox"></div>
                                <div id="dxModulePermissionCreateCheckBox"></div>
                                <div id="dxModulePermissionUpdateCheckBox"></div>
                                <div id="dxModulePermissionDeleteCheckBox"></div>
                            </div>--%>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Role <span id="RolesRequire" hidden>(<span class="text-danger">*</span>)</span></label>
                        <div class="col-xl-9 col-md-12">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxModuleSetupRoleTagBox"></div>
                                <a href="/App/Features/AdvancedSettings/SecurityManagement/RoleAdministration.aspx">Can't find the Role you're looking for? Click here</a>

                            </div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Is Active?</label>
                        <div class="col-xl-9 col-md-12">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxModuleSetupIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="ModuleSetupActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenModuleID" hidden />
    <input type="hidden" id="hiddenModuleSetUpID" hidden />
    <input type="hidden" id="hiddenPermissionID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/SecurityManagement/ModuleCatalog.js"></script>
</asp:Content>
