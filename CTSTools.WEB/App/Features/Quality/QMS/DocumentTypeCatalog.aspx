<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DocumentTypeCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Quality.QMS.DocumentTypeCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- BEGIN breadcrumb -->
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-end">
                    <li class="breadcrumb-item"><a href="javascript:;">Quality</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Document Type</a></li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Document Type</h1>
            </div>
            <!-- END page-header -->
        </div>
        <!-- BEGIN DocumentType Catalog -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="NewDocumentTypeBtn" href="#SaveDocumentTypeRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i>Document Type</a>
                        </div>
                        <div id="dxDocumentTypeGrid"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END DocumentType Catalog -->
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveDocumentTypeRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="DocumentTypeModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseDocumentTypeModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxDocumentTypeNameTextBox"></div>
                            <div class="invalid-feedback" id="DocumentTypeNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Folder Name (<span class="text-danger">*</span>)</label>
                        <div class="col-md-12">
                            <div id="dxDocumentTypeFolderNameTextBox"></div>
                            <div class="invalid-feedback" id="DocumentTypeFolderNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-md-12">Description</label>
                        <div class="col-md-12">
                            <div id="dxDocumentTypeDescription"></div>
                        </div>
                    </div>

                    
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-md-auto">Is Active?</label>
                    <div class="col-md-auto">
                        <div class="mt-2 mb-2">
                            <div type="text" id="dxDocumentTypeIsActiveCheckBox"></div>
                        </div>
                    </div>
                </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="DocumentTypeActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDocumentTypeID" hidden />
    <script type="module" src="/App/Features/Quality/QMS/DocumentTypeCatalog.js"></script>
</asp:Content>
