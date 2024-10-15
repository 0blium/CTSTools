<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="LocationsCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.LocationManagement.LocationsCatalog" %>

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
                <h1 class="page-header">Location Management</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Status Catalog -->
        <div class="row">
            <div class="col-12">
                <ul class="nav nav-pills mb-2" role="tablist">
                    <li class="nav-item" role="presentation">
                        <a href="#FacilityTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                            <span class="d-sm-none">Facility</span>
                            <span class="d-sm-block d-none">Facility</span>
                        </a>
                    </li>
                    <li class="nav-item" role="presentation">
                        <a href="#DepartmentTab" data-bs-toggle="tab" class="nav-link" aria-selected="false" tabindex="-1" role="tab">
                            <span class="d-sm-none">Department</span>
                            <span class="d-sm-block d-none">Department</span>
                        </a>
                    </li>
                </ul>
                <div class="tab-content rounded-0 m-0">
                    <div class="tab-pane fade active show" id="FacilityTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#FacilityModal"><i class="fa-solid fa-circle-plus"></i>  Facility</a>
                                        <div id="dxFacilityGrid"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="DepartmentTab" role="tabpanel">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-inverse">
                                    <div class="panel-body">
                                        <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#DepartmentModal"><i class="fa-solid fa-circle-plus"></i>  Department</a>
                                        <div id="dxDepartmentGrid"></div>
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
    <div class="modal fade" id="FacilityModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Facility Form</h1>
                    <button type="button" id="FacilityModalCloseButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxFacilityNameTextBox"></div>
                            <div class="invalid-feedback" id="FacilityNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxFacilityDescriptionTextArea"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxFacilityIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="FacilityActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="DepartmentModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Department Form</h1>
                    <button type="button" id="DepartmentModalCloseButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDepartmentNameTextBox"></div>
                            <div class="invalid-feedback" id="DepartmentNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDepartmentDescriptionTextArea"></div>
                            <div class="invalid-feedback" id="DepartmentDescriptionValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Facility (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDepartmentFacilitySelectBox"></div>
                            <div class="invalid-feedback" id="DepartmentFacilityValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Responsibles (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDepartmentResponsiblesTagBox"></div>
                            <div class="invalid-feedback" id="DepartmentResponsiblesValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxDepartmentIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="DepartmentActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenFacilityID" hidden />
    <input type="hidden" id="hiddenDepartmentID" hidden />
    <script type="module" src="/App/Features/AdvancedSettings/LocationManagement/LocationsCatalog.js"></script>
</asp:Content>
