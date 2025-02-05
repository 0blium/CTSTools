<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="StatusAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.StatusManagement.StatusAdministration" %>

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
                <h1 class="page-header">Status</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#StatusTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                            <span class="d-sm-none">Status</span>
                            <span class="d-sm-block d-none">Status</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#StatusTypeTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Type</span>
                            <span class="d-sm-block d-none">Type</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#StatusRelationTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Status Relation</span>
                            <span class="d-sm-block d-none">Status Relation</span>
                        </a>
                    </li>
                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade active show" id="StatusTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">

                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#StatusModal" id="SupplyTypeButton">Add New</a>

                                        <div id="dxStatusGrid"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="StatusTypeTab" role="tabpanel">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">

                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#StatusTypeModal" id="SupplyTypeButton">Add New</a>

                                        <div id="dxStatusTypeGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="StatusRelationTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">

                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#StatusRelationModal" id="StatusRelationButton">Add New</a>

                                        <div id="dxStatus_StatusTypeGrid"></div>
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

    <!-- Status Modal -->

    <div class="modal fade" id="StatusModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Status Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxStatusNameTextBox"></div>
                            <div class="invalid-feedback" id="StatusNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxStatusDescription"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxStatusIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="StatusActionButtons"></div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="StatusTypeModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Status Type Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxStatusTypeNameTextBox"></div>
                            <div class="invalid-feedback" id="StatusTypeNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxStatusTypeDescription"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">

                                <div type="text" id="dxStatusTypeIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="StatusTypeActionButtons"></div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="StatusRelationModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Status Relation Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Status (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <%--<input type="email" class="form-control mb-5px" id="StatusName" placeholder="Enter Status Name">--%>
                            <div id="dxStatus_StatusTypeStatusSelectBox"></div>
                            <div class="invalid-feedback" id="Status_StatusTypeStatusValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Status Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <%--<textarea class="form-control" id="StatusDescription" rows="3"></textarea>--%>
                            <div id="dxStatus_StatusTypeStatusTypeSelectBox"></div>
                            <div class="invalid-feedback" id="Status_StatusTypeStatusTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxStatus_StatusTypeIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="Status_StatusTypeActionButtons"></div>
                </div>
            </div>
        </div>
    </div>


    <input type="hidden" id="hiddenStatusID" hidden />
    <input type="hidden" id="hiddenStatusTypeID" hidden />
    <input type="hidden" id="hiddenStatus_StatusTypeID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/StatusManagement/StatusAdministration.js"></script>
</asp:Content>
