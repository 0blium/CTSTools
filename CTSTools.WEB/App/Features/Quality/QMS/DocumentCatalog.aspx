<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DocumentCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Quality.QMS.DocumentCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Quality</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">QMS</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Documents</h1>
            </div>
            <!-- END page-header -->
        </div>

        <!-- BEGIN Document Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 mb-3">
                            <a id="NewDocumentBtn" href="#SaveDocumentRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>&nbsp Document</a>
                        </div>
                        <div id="dxDocumentGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END Document Catalog -->

        <!-- Modal save record -->
        <div class="modal fade" id="SaveDocumentRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="DocumentModalTitle" class="modal-title fs-5"></h4>
                        <button type="button" id="btnCloseDocumentModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <!-- Name -->
                            <div class="mb-3">
                                <label for="dxDocumentNameTextBox" class="form-label">Name (<span class="text-danger">*</span>)</label>
                                <div id="dxDocumentNameTextBox"></div>
                                <div class="invalid-feedback" id="DocumentNameValidation"></div>
                            </div>

                            <!-- Description -->
                            <div class="mb-3">
                                <label for="dxDocumentDescriptionTextArea" class="form-label">Description </label>
                                <div id="dxDocumentDescriptionTextArea"></div>
                            </div>

                            <!-- Row: Owner & Number -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentNumberTextBox" class="form-label">Number (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentNumberTextBox"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentOwnerSelectBox" class="form-label">Owner (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentOwnerSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentOwnerValidation"></div>
                                </div>
                            </div>

                            <!-- Row: Facility & DocumentType -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentTypeSelectBox" class="form-label">Type (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentTypeSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentTypeValidation"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentFacilitySelectBox" class="form-label">Facility (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentFacilitySelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentFacilityValidation"></div>
                                </div>
                            </div>
                            <!-- Row: Department & Customer-->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentDepartmentSelectBox" class="form-label">Department (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentDepartmentSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentDepartmentValidation"></div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentCustomerSelectBox" class="form-label">Customer (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentCustomerSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentCustomerValidation"></div>
                                </div>
                            </div>
                            <%-- Row: Product --%>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentProductSelectBox" class="form-label">Product (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentProductSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentProductValidation"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <!-- Action Buttons -->
                        <div class="row" id="DocumentActionButtons"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDocumentID" hidden />
    <script type="module" src="/App/Features/Quality/QMS/DocumentCatalog.js"></script>
</asp:Content>
