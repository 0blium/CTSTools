<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="RoleAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.RoleAdministration" %>

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
                <h1 class="page-header">Role Management</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#RoleTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                            <span class="d-sm-none">Role</span>
                            <span class="d-sm-block d-none">Role</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#RoleTypeTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Role Type</span>
                            <span class="d-sm-block d-none">Role Type</span>
                        </a>
                    </li>

                    <li class="nav-item" role="presentation">
                        <a href="#RolePermissionTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Role's Permissions</span>
                            <span class="d-sm-block d-none">Role's Permissions</span>
                        </a>
                    </li>
                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade active show" id="RoleTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#RoleModal" id="RoleButton"><i class="fa-solid fa-circle-plus"></i>Role</a>
                                        <div id="dxRoleGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="RoleTypeTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#RoleTypeModal" id="RoleTypeButton"><i class="fa-solid fa-circle-plus"></i>Role Type</a>
                                        <div id="dxRoleTypeGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="RoleRelationTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#RoleRelationModal" id="RoleRelationButton"><i class="fa-solid fa-circle-plus"></i>Relation</a>
                                        <div id="dxRoleRelationGrid"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="RolePermissionTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#RolePermissionModal" id="RolePermissionButton"><i class="fa-solid fa-circle-plus"></i>Permission</a>

                                        <div id="dxRole_PermissionGrid"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END Status Catalog -->

        <%--        Role Catalog Start--%>
        <div class="modal fade" id="RoleModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Role Form</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRoleNameTextBox"></div>
                                <div class="invalid-feedback" id="RoleNameValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRoleDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Type (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRoleTypeSelectBox"></div>
                                <div class="invalid-feedback" id="RoleTypeValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxRoleIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row" id="RoleActionButtons"></div>

                    </div>
                </div>
            </div>
        </div>
        <%--        Role Catalog End--%>

        <%--        Role Type Catalog Start--%>
        <div class="modal fade" id="RoleTypeModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Role Type Form</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRoleTypeNameTextBox"></div>
                                <div class="invalid-feedback" id="RoleTypeNameValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRoleTypeDescription"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxRoleTypeIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row" id="RoleTypeActionButtons"></div>

                    </div>
                </div>
            </div>
        </div>
        <%--        Role Type Catalog End--%>

        <%--        Role Permissions Catalog Start--%>
        <div class="modal fade" id="RolePermissionModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Role Relation Form</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Role (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRole_PermissionRoleSelectBox"></div>
                                <div class="invalid-feedback" id="Role_PermissionRoleValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Permission (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxRole_PermissionPermissionTagBox"></div>
                                <div class="invalid-feedback" id="Role_PermissionPermissionValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxRole_PermissionIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row" id="Role_PermissionActionButtons"></div>

                    </div>
                </div>
            </div>
        </div>

        <%--        Role Type Catalog End--%>

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
                                <div id="dxPermissionModuleTextBox"></div>
                                <div class="invalid-feedback" id="PermissionModuleValidation"></div>
                            </div>
                        </div>
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
    </div>
    <input type="hidden" id="hiddenRoleID" hidden />
    <input type="hidden" id="hiddenRoleTypeID" hidden />
    <input type="hidden" id="hiddenRole_PermissionID" hidden />
    <input type="hidden" id="hiddenPermissionID" hidden />
    <input type="hidden" id="hiddenActionID" hidden />

    <script type="module" src="/App/Features/AdvancedSettings/SecurityManagement/RoleAdministration.js"></script>
</asp:Content>
