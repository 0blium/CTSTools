<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="UserCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.UserCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item">Advanced Settings</li>
                    <li class="breadcrumb-item">Org. Management</li>
                    <li class="breadcrumb-item"></li>

                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">User Management</h1>
            </div>
            <!-- END page-header -->
        </div>
        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#UserTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                            <span class="d-sm-none">Users</span>
                            <span class="d-sm-block d-none">Users</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#UserPermissionsTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Permissions</span>
                            <span class="d-sm-block d-none">Permissions</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#UserRolesTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Roles</span>
                            <span class="d-sm-block d-none">Roles</span>
                        </a>
                    </li>
                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade active show" id="UserTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#UserModal" id="UserButton"><i class="fa-solid fa-circle-plus"></i> User</a>
                                        <div id="dxUserGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="UserPermissionsTab">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#AddNewPermissionUserModal" id="User_PermissionButton"><i class="fa-solid fa-circle-plus"></i> Permission</a>
                                        <div id="dxUser_PermissionGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="UserRolesTab">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#AddNewRoleUserModal" id="User_RoleButton"><i class="fa-solid fa-circle-plus"></i> Role</a>
                                        <div id="dxUser_RoleGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- END User Management -->
    </div>
    <!-- Modal -->

    <div class="modal fade" id="UserModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="userModalLabel">User Form</h1>
                    <button type="button" id="UserModalCloseButtton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <%--Code here--%>
                    <div class="row">
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Login (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserLoginTextBox"></div>
                                <div class="invalid-feedback" id="UserLoginValidation"></div>
                                <div id="dxUserSendWelcomeEmailCheckBox"></div>
                            </div>
                        </div>
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserNameTextBox"></div>
                                <div class="invalid-feedback" id="UserNameValidation"></div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Email (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserEmailTextBox"></div>
                                <div class="invalid-feedback" id="UserEmailValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Position (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserPositionTextBox"></div>
                                <div class="invalid-feedback" id="UserPositionValidation"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Facility (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserFacilitySelectBox"></div>
                                <div class="invalid-feedback" id="UserFacilityValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Department (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxUserDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="UserDepartmentValidation"></div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-15px">
                            <label class="form-label col-form-label col-md-12">Role</label>
                            <div class="col-md-12">
                                <div id="dxUserRoleTagBox"></div>
                                <div class="invalid-feedback" id="UserRolesValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6 mb-15px">
                            <div class="col-md-12">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxUserIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <div class="row" id="UserActionButtons"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="AddNewPermissionUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Add new permissions to user</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="User_PermissionModalCloseButton" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-3">User (<span class="text-danger">*</span>)</label>
                        <div class="col-md-9">
                            <div id="dxUser_PermissionUserSelectBox"></div>
                            <div class="invalid-feedback" id="User_PermissionUserValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-3">Permissions (<span class="text-danger">*</span>)</label>
                        <div class="col-md-9">
                            <div id="dxUser_PermissionPermissionTagBox"></div>
                            <div class="invalid-feedback" id="User_PermissionPermissionValidation"></div>
                        </div>
                    </div>
                    <div class="mb-15px">
                        <div class="col-md-9">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxUser_PermissionIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="User_PermissionActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="AddNewRoleUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="rolesModalLabel">Add new roles to user</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-3">User (<span class="text-danger">*</span>)</label>
                        <div class="col-md-9">
                            <div id="dxUser_RoleUserSelectBox"></div>
                            <div class="invalid-feedback" id="User_RoleUserValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-3">Roles (<span class="text-danger">*</span>)</label>
                        <div class="col-md-9">
                            <div id="dxUser_RoleRoleSelectBox"></div>
                            <div class="invalid-feedback" id="User_RoleRoleValidation"></div>
                        </div>
                    </div>
                    <div class="mb-15px">
                        <div class="col-md-9">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxUser_RoleIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="User_RoleActionButtons"></div>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" id="hiddenUserID" hidden />
    <input type="hidden" id="hiddenUser_PermissionID" hidden />
    <input type="hidden" id="hiddenUser_RoleID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/UserManagement/UserCatalog.js"></script>
</asp:Content>
