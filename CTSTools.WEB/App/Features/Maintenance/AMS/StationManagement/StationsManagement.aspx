<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="StationsManagement.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.StationManagement.StationsManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">AMS</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Stations</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="AddNewStationButton" href="#AddNewStationModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i> Station</a>
                        </div>
                        <div id="dxStationGrid"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Add new item Modal -->
    <div class="modal fade" id="AddNewStationModal" tabindex="-1" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 id="StationModalTitle" class="modal-title fs-5"></h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="row mb-15px">
                            <div class="col-md-6">
                                <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxStationNameTextBox"></div>
                                    <div class="invalid-feedback" id="StationNameValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label col-form-label col-md-12">Serial (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxStationSerialTextBox"></div>
                                    <div class="invalid-feedback" id="StationSerialValidation"></div>
                                </div>
                            </div>
                        </div>
                        <div class="mb-15px">
                            <label class="form-label col-form-label col-md-12">Station Type (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxStationStationTypeSelectBox"></div>
                                <div class="invalid-feedback" id="StationStationTypeValidation"></div>
                            </div>
                        </div>
                        <div class="mb-15px">
                            <label class="form-label col-form-label col-md-12">Description</label>
                            <div class="col-md-12">
                                <div id="dxStationDescriptionTextArea"></div>
                                <div class="invalid-feedback" id="StationDescriptionValidation"></div>
                            </div>
                        </div>
                        <div class="mb-15px">
                            <label class="form-label col-form-label col-md-12">Facility (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxStationFacilitySelectBox"></div>
                                <div class="invalid-feedback" id="StationFacilityValidation"></div>
                            </div>
                        </div>
                        <div class="mb-15px">
                            <label class="form-label col-form-label col-md-12">Department (<span class="text-danger">*</span>)</label>
                            <div class="col-md-12">
                                <div id="dxStationDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="StationDepartmentValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-md-3">Is Active?</label>
                            <div class="col-md-9">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxStationIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div id="StationActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenStationID" hidden />
    <script type="module" src="/App/Features/Maintenance/AMS/StationManagement/StationsManagement.js"></script>
</asp:Content>
