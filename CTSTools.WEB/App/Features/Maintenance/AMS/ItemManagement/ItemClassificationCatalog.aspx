<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ItemClassificationCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.ItemClassification.ItemClassificationCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">AMS</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Items</a></li>
                    <li class="breadcrumb-item active">Classification</li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Classification Catalog</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="NewItemClassificationBtn" href="#SaveItemClassificationRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i> Classification</a>
                        </div>
                        <div id="dxItemClassificationGrid"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveItemClassificationRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="ItemClassificationModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseItemClassificationModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxItemClassificationEnglishNameTextBox"></div>
                            <div class="invalid-feedback" id="ItemClassificationEnglishNameValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxItemClassificationDescriptionTextArea"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxItemClassificationIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="ItemClassificationActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenItemClassificationID" hidden />
    <script type="module" src="/App/Features/Maintenance/AMS/ItemManagement/ItemClassificationCatalog.js"></script>
</asp:Content>
