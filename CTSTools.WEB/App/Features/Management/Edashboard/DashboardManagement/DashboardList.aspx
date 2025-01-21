<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DashboardList.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.DashboardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">E-Dashboard</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Dashboard  List</h1>
            </div>
            <!-- END page-header -->

        </div>

        <!-- BEGIN Dashboard Catalog -->
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 mb-3">
                            <a id="NewDashboardBtn" href="#SaveDashboardRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>&nbsp Dashboard</a>
                        </div>
                        <div id="dxDashboardGrid"></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- END Dashboard Catalog -->

        <!-- Modal save record -->
        <div class="modal fade" id="SaveDashboardRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="DashboardModalTitle" class="modal-title fs-5"></h4>
                        <button type="button" id="btnCloseDashboardModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <!-- Name -->
                            <div class="mb-3">
                                <label for="dxDashboardNameTextBox" class="form-label">Name (<span class="text-danger">*</span>)</label>
                                <div id="dxDashboardNameTextBox"></div>
                                <div class="invalid-feedback" id="DashboardNameValidation"></div>
                            </div>

                            <!-- Description -->
                            <div class="mb-3">
                                <label for="dxDashboardDescriptionTextArea" class="form-label">Description</label>
                                <div id="dxDashboardDescriptionTextArea"></div>
                            </div>

                            <!-- Row: Revision & New Select Box -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardRevisionTextBox" class="form-label">Revision (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardRevisionTextBox"></div>
                                    <div class="invalid-feedback" id="DashboardRevisionValidation"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardYearSelectBox" class="form-label">Year (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardYearSelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardYearValidation"></div>
                                </div>
                            </div>

                            <!-- Row: Owner & Level -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardOwnerSelectBox" class="form-label">Owner (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardOwnerSelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardOwnerValidation"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardLevelSelectBox" class="form-label">Level (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardLevelSelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardLevelValidation"></div>

                                </div>
                            </div>

                            <!-- Row: Facility & Department -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardFacilitySelectBox" class="form-label">Facility (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardFacilitySelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardFacilityValidation"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardDepartmentSelectBox" class="form-label">Department (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardDepartmentSelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardDepartmentValidation"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDashboardFacilitySelectBox" class="form-label">Goal Range (<span class="text-danger">*</span>)</label>
                                    <div id="dxDashboardGoalRangeSelectBox"></div>
                                    <div class="invalid-feedback" id="DashboardGoalRangeValidation"></div>
                                </div>
                                
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <!-- Action Buttons -->
                        <div class="row" id="DashboardActionButtons"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDashboardID" hidden />
    <script type="module" src="/App/Features/Management/Edashboard/DashboardManagement/DashboardList.js"></script>
</asp:Content>
