<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ItemLineCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.ItemLineCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">AMS</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Items</a></li>
                    <li class="breadcrumb-item active">Administration</li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Asset Catalog</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="col-md-12 ">
                            <a id="NewItemLineBtn" href="#SaveItemLineRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal" hidden><i class="fa-solid fa-circle-plus"></i> Asset</a>
                        </div>
                        <div id="dxItemLineGrid"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal save record -->
    <div class="modal fade" id="SaveItemLineRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="ItemLineModalTitle" class="modal-title fs-5"></h4>
                    <button type="button" id="btnCloseItemLineModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Item (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineItem_HeaderSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineItem_HeaderValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Legacy ID</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineLegacyIDTextBox"></div>
                                    <div class="invalid-feedback" id="Item_LineLegacyIDValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Owner (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineOwnerSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineOwnerValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Serial (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineSerialTextBox"></div>
                                    <div class="invalid-feedback" id="Item_LineSerialValidation"></div>
                                    <div id="dxItem_LineGenerateSerialCheckBox"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Comments </label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineCommentsTextArea"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineSupportGroupSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineSupportGroupValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Manufacture Serial ID (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineManufactureSerialIDTextBox"></div>
                                    <div class="invalid-feedback" id="Item_LineManufactureSerialIDValidation"></div>
                                </div>
                            </div>
                            <div class="row mb-15px">
                                <label class="form-label col-form-label col-md-12">Price (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineBasePriceUSDTextBox"></div>
                                    <div class="invalid-feedback" id="Item_LineBasePriceUSDValidation"></div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Station (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineStationSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineStationValidation"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Supply Type(<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineSupplyTypeSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineSupplyTypeValidation"></div>
                                </div>
                            </div>
                            <div class="row d-none" id="SupplyTypeLocal">
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">PO Number (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxItem_LinePONumberTextBox"></div>
                                        <div class="invalid-feedback" id="Item_LinePONumberValidation"></div>
                                    </div>
                                </div>
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">PO Line (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxItem_LinePOLineTextBox"></div>
                                        <div class="invalid-feedback" id="Item_LinePOLineValidation"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row d-none" id="SupplyTypeImport">
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">Import Invoice (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxItem_LineImportInvoiceTextBox"></div>
                                        <div class="invalid-feedback" id="Item_LineImportInvoiceValidation"></div>
                                    </div>
                                </div>
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">Shipment Receipt # (AV)</label>
                                    <div class="col-md-12">
                                        <div id="dxItem_LineShipmentReceiptNumberTextBox"></div>
                                        <div class="invalid-feedback" id="Item_LineShipmentReceiptNumberValidation"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">Status</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineStatusSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineStatusValidation"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="UserDefinedTemplateSection">
                    </div>
                </div>
            <div class="modal-footer">
                <div class="row" id="ItemLineActionButtons"></div>
            </div>
        </div>
    </div>
    </div>
    <%-- Update Support Group to Item  line Modal --%>
    <div class="modal fade" id="ReassingSupportGroupModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ModalTitle">Reassign Support Group</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxReassignSupportGroupModalSelectBox"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="ReassignSupportGroupModalActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenItemLineID" hidden />
    <input type="hidden" id="hiddenItemLineSupportGroupID" hidden />
    <input type="hidden" id="hiddenItemHeaderID" hidden />
    <input type="hidden" id="hiddenItemSupportGroupID" hidden />
    <script type="module" src="/App/Features/Maintenance/AMS/ItemManagement/ItemLineCatalog.js"></script>
</asp:Content>
