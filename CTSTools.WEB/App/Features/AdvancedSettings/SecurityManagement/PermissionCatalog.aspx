<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="PermissionCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.PermissionCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Org Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;"></a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Permissions</h1>
            </div>
            <!-- END page-header -->

        </div>

        <div class="row">
            <div class="col-12">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-body">
                                <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#PermissionModal" id="PermissionButton"><i class="fa-solid fa-circle-plus"></i>Permission</a>
                                <div id="dxPermissionGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--        Permission Catalog Start--%>
        <div class="modal fade" id="PermissionModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5">Permission Form</h1>
                        <button type="button" class="btn-close" id="btnClosePermissionModal" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxPermissionNameTextBox"></div>
                                <div class="invalid-feedback" id="PermissionNameValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Module (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxPermissionModuleSelectBox"></div>
                                    <div class="invalid-feedback" id="PermissionModuleValidation"></div>
                                    <a href="/App/Features/AdvancedSettings/SecurityManagement/ModuleCatalog.aspx" >Can't find the Module you're looking for? Click here</a>

                                </div>
                            </div>
                        </div>
                        <%--<div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Module (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxPermissionModuleTextBox"></div>
                                <div class="invalid-feedback" id="PermissionModuleValidation"></div>
                            </div>
                        </div>--%>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxPermissionDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Action (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxPermissionActionSelectBox"></div>
                                    <div class="invalid-feedback" id="PermissionActionValidation"></div>
                                    <a href="#Permission_ActionModal" data-bs-toggle="modal">Can't find the Action you're looking for? Click here</a>

                                </div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxPermissionIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                <div class="modal-footer">
                    <div class="row" id="PermissionActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <%--        Permission Catalog End--%>

    <%-- Permission Action Modal --%>
    <div class="modal fade" id="Permission_ActionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Add new Actions</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-25px">
                        <div class="col-md-12 col-lg-4">
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-9 col-md-12">
                                    <div id="dxActionNameTextBox"></div>
                                    <div class="invalid-feedback" id="ActionNameValidation"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                                <div class="col-xl-9 col-md-12">
                                    <div id="dxActionDescriptionTextArea"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                                <div class="col-xl-9 col-md-auto">
                                    <div class="mt-2 mb-2">
                                        <div type="text" id="dxActionIsActiveCheckBox"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="ActionActionButtons"></div>
                        </div>
                        <div class="col-md-12 col-lg-8">
                            <div class="panel-body">
                                <div type="text" id="dxActionGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <input type="hidden" id="hiddenPermissionID" hidden />
    <input type="hidden" id="hiddenActionID" hidden />

    <script type="module" src="/App/Features/AdvancedSettings/SecurityManagement/PermissionCatalog.js"></script>

</asp:Content>
