<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SupplierCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.SupplierManagement.SupplierCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- BEGIN breadcrumb -->
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Supplier Management</a></li>
                <li class="breadcrumb-item"><a href="javascript:;"></a></li>

            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Suppliers</h1>
        </div>
        <!-- END page-header -->

    </div>
    <div class="col-md-12">
        <div class="panel panel-inverse">
            <div class="panel-body">
                <a class="btn btn-success mb-2" id="SupplierModalButton" data-bs-toggle="modal" data-bs-target="#SupplierModal" id="SupplierButton"><i class="fa-solid fa-circle-plus"></i>Supplier</a>
                <a class="btn btn-success mb-2 float-end" id="UploadMassiveSupplierModalButton" data-bs-toggle="modal" data-bs-target="#UploadExcelSupplierModal" hidden><i class="fa-solid fa-file-import"></i> Excel</a>
                <div id="dxSupplierGrid"></div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="SupplierModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Supplier Form</h1>
                    <button type="button" id="SupplierCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupplierNameTextBox"></div>
                            <div class="invalid-feedback" id="SupplierNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupplierDescriptionTextArea"></div>
                            <div class="invalid-feedback" id="SupplierDescriptionValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Manufacturer?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSupplierIsManufacturerCheckBox"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Vendor?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSupplierIsVendorCheckBox"></div>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSupplierIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="SupplierActionButtons"></div>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="UploadExcelSupplierModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Upload file excel</h1>
                    <button type="button" id="UploadExcelSupplirCloseModalButton" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <label class="text-muted">Step 1: Download the Excel format by clicking <a class="h6 text-color-link" id="ExcelSupplierFormatButton">here</a>.</label>
                        <br />
                        <label class="text-muted">Step 2: To upload your file with the data, click the button below.</label>
                    </div>
                    <div class="row mb-15px">
                        <div id="dxSupplierFileUploader"></div>
                    </div>
                    <div id="successMessage" class="alert alert-success" hidden></div>
                    <div id="errorMessages" class="alert alert-danger" hidden></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary float-end" id="ClearExcelSupplierButton" type="button">Clear</button>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenSupplierID" />
    <script type="module" src="/App/Features/Engineering/ComponentID/SupplierManagement/SupplierCatalog.js"></script>

</asp:Content>
