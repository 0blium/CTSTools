<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DocumentRevisionCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Quality.QMS.DocumentRevisionCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Quality</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">QMS</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">DocumentRevision</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Revisions</h1>
            </div>
            <!-- END page-header -->
        </div>

        <!-- BEGIN DocumentRevision Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 mb-3">
                            <a id="NewDocumentRevisionBtn" href="#SaveDocumentRevisionRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>&nbsp Revisions</a>
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
                            <!-- Revision -->
                            <div class="mb-3">
                                <label for="dxDocumentRevisionRevisionTextBox" class="form-label">Revision (<span class="text-danger">*</span>)</label>
                                <div id="dxDocumentRevisionRevisionTextBox"></div>
                                <div class="invalid-feedback" id="DocumentRevisionRevisionValidation"></div>
                            </div>
                            <!-- Row: Status -->
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="dxDocumentRevisionStatusSelectBox" class="form-label">Status (<span class="text-danger">*</span>)</label>
                                    <div id="dxDocumentRevisionStatusSelectBox"></div>
                                    <div class="invalid-feedback" id="DocumentRevisionStatusValidation"></div>
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
    </div>
    <input type="hidden" id="hiddenDocumentID" hidden />
    <input type="hidden" id="hiddenDocumentRevisionID" hidden />
    <script type="module" src="/App/Features/Quality/QMS/DocumentRevisionCatalog.js"></script>
</asp:Content>
