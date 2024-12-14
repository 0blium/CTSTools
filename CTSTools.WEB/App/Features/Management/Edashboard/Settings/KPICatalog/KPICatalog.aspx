<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="KPICatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.Settings.KPICatalog.KPICatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">E-Dashboard</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">KPI Catalog</h1>
            </div>
            <!-- END page-header -->

        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 mb-3">
                                <a id="NewKPICategoryBtn" href="#SaveKPICategoryRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>KPI</a>
                            </div>
                            <div class="col-lg-12">
                                <div id="dxMetricGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveKPICategoryRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog modal-xl ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="KPICategoryModalTitle" class="modal-title"></h4>
                    <button type="button" id="btnCloseKPICategoryModal" class="btn-close fs-5" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">KPI Name (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricNameTextBox"></div>
                                <div class="invalid-feedback" id="MetricNameValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label for="exampleFormControlTextarea1" class="form-label">KPI Description</label>
                                <div id="dxMetricDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">Goal  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricGoalNumberBox"></div>
                                <div class="invalid-feedback" id="MetricGoalValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Owner (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricOwnerSelectBox"></div>
                                <div class="invalid-feedback" id="MetricOwnerValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Unit Of Measure  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricUnitOfMeasureSelectBox"></div>
                                <div class="invalid-feedback" id="MetricUnitOfMeasureValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="mb-3">
                                <label class="form-label">Value Type  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricValueTypeSelectBox"></div>
                                <div class="invalid-feedback" id="MetricValueTypeValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Facility  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricFacilitySelectBox"></div>
                                <div class="invalid-feedback" id="MetricFacilityValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Owner Department  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricOwnerDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="MetricOwnerDepartmentValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="mb-3">
                                <label class="form-label">Comparison</label>
                                <div id="dxMetricEquivalenceSelectBox"></div>
                                <div class="invalid-feedback" id="MetricEquivalenceValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Responsible  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricResponsibleSelectBox"></div>
                                <div class="invalid-feedback" id="MetricResponsibleValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="mb-3">
                                <label class="form-label">Goal Range  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricGoalRangeSelectBox"></div>
                                <div class="invalid-feedback" id="MetricGoalRangeValidation"></div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Responsible Department  (<span class="text-danger">*</span>)</label>
                                <div id="dxMetricResponsibleDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="MetricResponsibleDepartmentValidation"></div>
                            </div>
                            <div class="mb-5">
                                <div type="text" id="dxMetricIsActiveCheckBox"></div>
                            </div>

                        </div>
                        <div class="col-md-12 mb-3">
                            <a data-bs-toggle="collapse" data-bs-target="#collapseUser" class="h6 text-color-link">Can't you find the value to select?</a>
                            <div id="collapseUser" class="accordion-collapse collapse border border-1 mt-1 mb-1" data-bs-parent="#accordion">
                                <div class="accordion-body accordion-height bg-light p-1  text-dark">
                                    <div class="col-md-12 px-2">
                                        <label class="col-md-12 mt-1">If the value to select doesn´t exits on the list above, create a new value to select clicking on the next link</label>
                                        <div>
                                            <a class="h6 mt-1 text-color-link" target="_blank" href="/App/Features/Management/Edashboard/Settings/KPISettings/KPISettings.aspx">Go to the KPI Settings Hub</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row" id="MetricActionButtons"></div>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" id="hiddenMetricID" value="0" />
        <input type="hidden" id="hiddenStatusID" value="0" />
        <script type="module" src="/App/Features/Management/Edashboard/Settings/KPICatalog/KPICatalog.js"></script>
</asp:Content>
