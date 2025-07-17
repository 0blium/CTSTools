<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="KPICatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.KPI.KPICatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Management</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">E-Dashboard</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Dashboards</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;"></a></li>

                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">KPI List</h1>
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
                                <a class="btn btn-success mb-2 float-end" id="UploadMassiveKPIModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelKPIModal" hidden><i class="fa-solid fa-file-import"></i> Excel</a>
                            </div>
                            <div class="col-lg-12">
                                <div id="dxKPIGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveKPICategoryRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="KPICategoryModalTitle" class="modal-title"></h4>
                    <button type="button" id="btnCloseKPICategoryModal" class="btn-close fs-5" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">Name (<span class="text-danger">*</span>)</label>
                                <div id="dxKPINameTextBox"></div>
                                <div class="invalid-feedback" id="KPINameValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="mb-3">
                                <label for="exampleFormControlTextarea1" class="form-label">Description</label>
                                <div id="dxKPIDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Comparison</label>
                                <div id="dxKPIEquivalenceSelectBox"></div>
                                <div class="invalid-feedback" id="KPIEquivalenceValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">Goal  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIGoalNumberBox"></div>
                                <div class="invalid-feedback" id="KPIGoalValidation"></div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Unit Of Measure  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIUnitOfMeasureSelectBox"></div>
                                <div class="invalid-feedback" id="KPIUnitOfMeasureValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Value Type  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIValueTypeSelectBox"></div>
                                <div class="invalid-feedback" id="KPIValueTypeValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Facility  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIFacilitySelectBox"></div>
                                <div class="invalid-feedback" id="KPIFacilityValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Owner Department  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIOwnerDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="KPIOwnerDepartmentValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Owner (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIOwnerSelectBox"></div>
                                <div class="invalid-feedback" id="KPIOwnerValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Responsible  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIResponsibleSelectBox"></div>
                                <div class="invalid-feedback" id="KPIResponsibleValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Responsible Department  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPIResponsibleDepartmentSelectBox"></div>
                                <div class="invalid-feedback" id="KPIResponsibleDepartmentValidation"></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label class="form-label">Category  (<span class="text-danger">*</span>)</label>
                                <div id="dxKPICategorySelectBox"></div>
                                <div class="invalid-feedback" id="KPICategoryValidation"></div>
                            </div>
                        </div>
                        <div class="mb-5">
                            <div type="text" id="dxKPIIsActiveCheckBox"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="KPIActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal update excel -->
    <div class="modal fade" id="UploadExcelKPIModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Upload file excel</h1>
                    <button type="button" id="UploadExcelKPICloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <label class="text-muted">Step 1: Download the Excel format by clicking <a class="h6 text-color-link" id="ExcelKPIFormatButton">here</a>.</label>
                        <br />
                        <label class="text-muted">Step 2: To upload your file with the data, click the button below.</label>
                    </div>
                    <div class="row mb-15px">
                        <div id="dxKPIFileUploader"></div>
                    </div>
                    <div id="successKPIMessage" class="alert alert-success" hidden></div>
                    <div id="errorKPIMessages" class="alert alert-danger" hidden></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary float-end" id="ClearExcelKPIButton" type="button">Clear</button>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenKPIID" value="0" />
    <input type="hidden" id="hiddenStatusID" value="0" />
    <script type="module" src="/App/Features/Management/Edashboard/KPI/KPICatalog.js"></script>
</asp:Content>
