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

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <a id="NewDocumentBtn" href="#SaveDocumentRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>&nbsp Document</a>
                                </div>
                              <%--  <div class="col-md-6 mb-3 text-end ">
                                    <a class="btn btn-dark" id="OpenFilterItem_Line" data-bs-toggle="collapse" href="#collapseFilterItemOwner" role="button" aria-expanded="false" aria-controls="collapseExample"><i class="fa-solid fa-filter"></i></a>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-md-12"></div>
                      <%--  <div class="collapse" id="collapseFilterItemOwner">
                            <div class="p-3 row">
                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Added Date</label>
                                    <div class="col-xl-12 col-lg-12 d-flex align-items-baseline">
                                        <p class="pe-2">from</p>
                                        <div class="m-b-5" id="dxAddedStartDateDateBox"></div>
                                        <p class="px-2">to</p>
                                        <div class="m-b-5" id="dxAddedEndDateDateBox"></div>
                                    </div>
                                </div>
                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Last Update</label>
                                    <div class="col-xl-12 col-lg-12 d-flex align-items-baseline">
                                        <p class="pe-2">from</p>
                                        <div class="m-b-5" id="dxLastUpdateStartDateDateBox"></div>
                                        <p class="px-2">to</p>
                                        <div class="m-b-5" id="dxLastUpdateEndDateDateBox"></div>
                                    </div>
                                </div>
                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Status</label>
                                    <div class="col-xl-12 col-lg-12">
                                        <div class="m-b-5" id="dxFilterStatusTagBox"></div>
                                    </div>
                                </div>

                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Department</label>
                                    <div class="col-xl-12 col-lg-12">
                                        <div class="m-b-5" id="dxFilterDepartmentTagBox"></div>
                                    </div>
                                </div>

                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Type</label>
                                    <div class="col-xl-12 col-lg-12">
                                        <div class="m-b-5" id="dxFilterDocumentTypeTagBox"></div>
                                    </div>
                                </div>
                                <div class="form-group col-xl-4 col-lg-6">
                                    <label class="col-form-label col-xl-12 col-lg-12">Owner</label>
                                    <div class="col-xl-12 col-lg-12">
                                        <div id="dxFilterOwnerTagBox"></div>
                                    </div>
                                </div>
                                <div class="col-12 d-flex justify-content-end mt-2">
                                    <a class="btn btn-secondary mb-1" id="ClearItem_LineFilters">Reset</a>
                                    <a class="btn btn-success mb-1 ms-1" id="GetItem_LineInformation">Search</a>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-12">
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

                            <!-- Row: Department & DocumentType -->
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
                            <%-- Row: Type && Product --%>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentTypeSelectBox" class="form-label">Type (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentTypeSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentTypeValidation"></div>
                                </div>
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
