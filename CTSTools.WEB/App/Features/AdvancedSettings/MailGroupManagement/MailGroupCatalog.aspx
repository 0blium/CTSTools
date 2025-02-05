<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="MailGroupCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.MailGroupManagement.MailGroupCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
                    <li class="breadcrumb-item active"></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Mail Groups</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#MailGroupTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                            <span class="d-sm-none">Mail Group</span>
                            <span class="d-sm-block d-none">Mail Group</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#MailGroupMemberTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Mail Group Member</span>
                            <span class="d-sm-block d-none">Mail Group Member</span>
                        </a>
                    </li>
                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade active show" id="MailGroupTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#MailGroupModal" id="MailGroupButton">Add New</a>
                                        <div id="dxMailGroupGrid"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="MailGroupMemberTab" role="tabpanel">
                        <div class="row">                            
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#MailGroupMemberModal" id="MailGroupMemberButton">Add New</a>

                                        <div id="dxMailGroupMemberGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!-- END Status Catalog -->
    </div>

    <div class="modal fade" id="MailGroupModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Mail Group Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxMailGroupNameTextBox"></div>
                            <div class="invalid-feedback" id="MailGroupNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxMailGroupDescriptionTextArea"></div>
                            <div class="invalid-feedback" id="MailGroupDescriptionValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxMailGroupIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="MailGroupActionButtons"></div>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="MailGroupMemberModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Mail Group Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">User (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxMailGroupMemberUserSelectBox"></div>
                            <div class="invalid-feedback" id="MailGroupMemberUserValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Mail Group (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxMailGroupMemberMailGroupSelectBox"></div>
                            <div class="invalid-feedback" id="MailGroupMemberMailGroupValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxMailGroupMemberIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="MailGroupMemberActionButtons"></div>

                </div>
            </div>
        </div>
    </div>



    <input type="hidden" id="hiddenMailGroupID" hidden />
    <input type="hidden" id="hiddenMailGroupMemberID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/MailGroupManagement/MailGroupCatalog.js"></script>
</asp:Content>
