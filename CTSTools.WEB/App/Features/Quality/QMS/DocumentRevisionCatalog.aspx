<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DocumentRevisionCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Quality.QMS.DocumentRevisionCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Quality</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">QMS</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Revisions</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;"></a></li>

                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 id="DocumentRevisionTitle" class="page-header"></h1>
            </div>
            <!-- END page-header -->
        </div>

        <!-- BEGIN DocumentRevision Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 mb-3">
                            <a id="NewDocumentRevisionBtn" href="#SaveDocumentRevisionRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>&nbsp Revision</a>
                        </div>
                        <div id="dxDocumentRevisionGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END DocumentRevision Catalog -->

        <!-- Modal save record -->
        <div class="modal fade" id="SaveDocumentRevisionRecordModal" data-bs-backdrop="static" aria-modal="true" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="DocumentRevisionModalTitle" class="modal-title fs-5"></h4>
                        <button type="button" id="btnCloseDocumentRevisionModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <%-- Document Change Reason --%>
                            <div class="mb-3">
                                <label for="dxDocumentRevisionChangeReasonTextArea" class="form-label">Change Reason (<span class="text-danger">*</span>)</label>
                                <div id="dxDocumentRevisionChangeReasonTextArea"></div>
                                <div class="invalid-feedback" id="DocumentRevisionChangeReasonValidation"></div>
                            </div>
                            <!-- Row: Revision and Status  -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentRevisionRevisionTextBox" class="form-label">Revision (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentRevisionRevisionTextBox"></div>
                                    <div class="invalid-feedback" id="DocumentRevisionRevisionValidation"></div>
                                </div>
                                <div id="StatusSection" class="col-md-6 mb-3" hidden>
                                    <label for="dxDocumentRevisionStatusSelectBox" class="form-label">Status (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentRevisionStatusSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentRevisionStatusValidation"></div>
                                </div>
                            </div>
                            <div class="row" id="RevisionFileSection">
                                <div class="col-md-12 mb-3">
                                    <label class="form-label">Document (<span class="text-danger">*</span>)</label>

                                    <div id="dxRevisionFileUploader"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <!-- Action Buttons -->
                        <div class="row" id="DocumentRevisionActionButtons"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="UpdateDocumentModal" data-bs-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title fs-5">Update Document</h4>
                        <button type="button" id="XBtnModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">                           
                            <div class="col-md-12 mt-2">
                                <h6>Document (<span class="text-danger">*</span>)</h6>
                                <div id="dxUpdateRevisionFileUploader"></div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a class="btn btn-success" id="UpdateDocumentButton">Update</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDocumentID" hidden />
    <input type="hidden" id="hiddenDocumentRevisionID" hidden />
    <script type="module" src="/App/Features/Quality/QMS/DocumentRevisionCatalog.js"></script>
</asp:Content>
