<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ItemAdministration.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.ItemAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ItemAdministration.css" rel="stylesheet" />
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
                <h1 class="page-header">Administration</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-inverse">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 ">
                                <a id="AddNewItemHeaderBtn" class="btn btn-success mb-1" data-bs-toggle="modal" data-bs-target="#AddNewItemHeaderModal"><i class="fa-solid fa-circle-plus"></i> Item</a>
                                <a id="AssignUserDefinedBtn" class="btn btn-success d-none mb-2 ms-1 float-end" data-bs-target="#AssignUserDefinedFieldsModal" data-bs-toggle="modal">Assign Fields</a>
                                <a class="btn btn-success mb-2 float-end" data-bs-toggle="modal" data-bs-target="#AddUserDefinedFieldsModal"><i class="fa-solid fa-circle-plus"></i> Fields</a>
                            </div>
                            <div class="col-12">
                                <div id="dxItemAdministrationDatGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Add new item Modal -->
    <%-- Wizard Modal --%>

    <!-- Modal -->
    <div class="modal fade wizard" id="AddNewItemHeaderModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Add New Item</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="WizardCloseButton"></button>
                </div>
                <div class="modal-body">
                    <div class="card" id="wizard-card">
                        <div class="card-body bg-light">
                            <ul class="nav nav-tabs workflow-wizard justify-content-center" id="LetterType">
                                <li class="nav-item item">
                                    <a href="#Item_SupportGroupStep1" data-bs-toggle="tab" class="active">
                                        <div class="nav-no">1</div>
                                        <div class="nav-text">Select Item</div>
                                    </a>
                                </li>
                                <li class="nav-item item">
                                    <a href="#Item_SupportGroupStep2" data-bs-toggle="tab" class="">
                                        <div class="nav-no">2</div>
                                        <div class="nav-text">Support Group</div>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="wizard tab-content panel p-3 rounded-0 rounded-bottom mb-0" id="wizard-panel">
                        <div class="wizard-step tab-pane fade active show" id="Item_SupportGroupStep1">
                            <!-- BEGIN nav-tabs -->
                            <ul class="nav nav-tabs" id="modal-nav-tabs">
                                <li class="nav-item">
                                    <a id="select-item-tab" href="#select-item-content" data-bs-toggle="tab" class="nav-link active">
                                        <span class="d-sm-none">Tab 1</span>
                                        <span class="d-sm-block d-none">Select Item</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a id="new-item-tab" href="#new-item-content" data-bs-toggle="tab" class="nav-link">
                                        <span class="d-sm-none">Tab 2</span>
                                        <span class="d-sm-block d-none">New Item</span>
                                    </a>
                                </li>
                            </ul>
                            <!-- END nav-tabs -->
                            <!-- BEGIN tab-content -->
                            <div class="tab-content panel rounded-0 p-3 m-0">

                                <!-- BEGIN tab-pane for Existing Item Selectbox -->
                                <div class="tab-pane fade active show" id="select-item-content">
                                    <label class="form-label col-form-label col-md-12">Item</label>
                                    <div class="col-md-12">
                                        <div id="dxItem_SupportGroupItem_HeaderIDSelectBox"></div>
                                        <div class="invalid-feedback" id="Item_SupportGroupItem_HeaderValidation">
                                        </div>
                                    </div>
                                </div>
                                <!-- END tab-pane -->

                                <!-- BEGIN tab-pane for New Item -->
                                <div class="tab-pane fade" id="new-item-content">
                                    <!-- BEGIN nav-tabs -->
                                    <div class="mb-2">
                                        <ul class="nav inlineTabs tab-border nav-tabs">
                                            <li class="nav-item">
                                                <a href="#equipment-tab" data-bs-toggle="tab" class="nav-link active">
                                                    <span class="d-sm-none">Equipment</span>
                                                    <span class="d-sm-block d-none">Equipment</span>
                                                </a>
                                            </li>
                                            <li class="nav-item">
                                                <a href="#attachments-tab" data-bs-toggle="tab" class="nav-link">
                                                    <span class="d-sm-none">Attachments</span>
                                                    <span class="d-sm-block d-none">Attachments</span>
                                                </a>
                                            </li>
                                        </ul>
                                        <hr class="m-0" />
                                    </div>
                                    <!-- END nav-tabs -->
                                    <!-- BEGIN tab-content -->
                                    <div class="tab-content rounded-0 m-0">
                                        <!-- BEGIN tab-pane -->
                                        <div class="tab-pane fade active show" id="equipment-tab">
                                            <div class="form-group row">
                                                <span class="fs-11px">Maximum file size: <span>5 MB</span>.</span>
                                                <div id="dxItem_HeaderThumbnailFileUploader"></div>
                                                <div class="col-md-12 text-center">
                                                    <img id="ItemThumbnail"
                                                        src="/App/Common/Assets/img/no-product-image.png"
                                                        class="img-fluid" style="height: 200px;" />
                                                </div>
                                            </div>
                                            <div class="mb-15px">
                                                <label class="form-label col-form-label col-md-12">
                                                    English Name (<span
                                                        class="text-danger">*</span>)</label>
                                                <div class="col-md-12">
                                                    <div id="dxItem_HeaderEnglishNameTextBox"></div>
                                                    <div class="invalid-feedback" id="Item_HeaderEnglishNameValidation">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mb-15px">
                                                <label class="form-label col-form-label col-md-12">
                                                    Spanish Name (<span
                                                        class="text-danger">*</span>)</label>
                                                <div class="col-md-12">
                                                    <div id="dxItem_HeaderSpanishNameTextBox"></div>
                                                    <div class="invalid-feedback" id="Item_HeaderSpanishNameValidation">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mb-15px">
                                                <label class="form-label col-form-label col-md-12">
                                                    Model (<span
                                                        class="text-danger">*</span>)</label>
                                                <div class="col-md-12">
                                                    <div id="dxItem_HeaderModelTextBox"></div>
                                                    <div class="invalid-feedback" id="Item_HeaderModelValidation"></div>
                                                </div>
                                            </div>
                                            <div class="mb-15px">
                                                <label class="form-label col-form-label col-md-12">
                                                    Brand (<span
                                                        class="text-danger">*</span>)</label>
                                                <div class="col-md-12">
                                                    <div id="dxItem_HeaderBrandTextBox"></div>
                                                    <div class="invalid-feedback" id="Item_HeaderBrandValidation"></div>
                                                </div>
                                            </div>
                                            <div class="mb-15px">
                                                <label class="form-label col-form-label col-md-12">
                                                    Item
                                                Classification</label>
                                                <div class="col-md-12">
                                                    <div id="dxItem_HeaderItemClassificationSelectBox"></div>
                                                    <div class="invalid-feedback"
                                                        id="Item_HeaderItemClassificationValidation">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-15px">
                                                <div class="col-md-4">
                                                    <div id="dxItem_HeaderIsActiveCheckBox"></div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div id="dxItem_HeaderIsESDCheckBox"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- END tab-pane -->
                                        <!-- BEGIN tab-pane -->
                                        <div class="tab-pane fade" id="attachments-tab">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label class="col-form-label col-md-4">Files:</label>
                                                    <div class="col-md-12">
                                                        <div id="dxFileOptionsSelectBox"></div>
                                                    </div>
                                                    <br />
                                                </div>
                                                <div id="TreeView" class="col-md-12" hidden>
                                                    <div style="text-align: right;">
                                                        <a href="#" id="TreeViewExpandBtn"
                                                            class="btn btn-default">Expand all</a>
                                                        <a href="#" id="TreeViewCollapseBtn"
                                                            class="btn btn-default">Collapse all</a>
                                                    </div>
                                                    <div id="dxTreeViewList"></div>
                                                </div>
                                                <div id="Item_HeaderAttachment" class="col-md-12" hidden>
                                                    <span class="fs-11px">Maximum file size: <span>5 MB</span>.</span>
                                                    <div id="dxItem_HeaderAttachmentFileUploader"></div>
                                                    <div id="Item_HeaderFilelistSection" class="row"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- END tab-pane -->
                                    </div>
                                    <!-- END tab-content -->
                                </div>
                                <!-- END tab-pane -->

                            </div>
                            <!-- END tab-content -->
                        </div>
                        <!-- BEGIN tab-pane for Step 2 -->
                        <div class="wizard-step tab-pane fade " id="Item_SupportGroupStep2">
                            <div class="mb-15px">
                                <label class="form-label col-form-label col-md-12">
                                    Support Group (<span
                                        class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_SupportGroupSupportGroupSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_SupportGroupSupportGroupValidation">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END tab-pane for Step 2 -->
                        <!-- BEGIN tab-content -->
                    </div>

                </div>
                <div class="modal-footer d-flex justify-content-between" id="modalFooter">
                    <a class="btn btn-secondary" id="btnAddNewItemBack">Back</a>
                    <a class="btn btn-success" id="btnAddNewItemNext">Next</a>
                </div>
            </div>
        </div>
    </div>

    <%-- END Wizard Modal --%>



    <div class="modal fade" id="AddUserDefinedFieldsModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-xl" style="width: 1180px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Custom fields</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="row justify-content-between">
                            <div class="col-md-4" style="width: 30%">
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">Name (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxUserDefinedNameTextBox"></div>
                                        <div class="invalid-feedback" id="UserDefinedNameValidation"></div>
                                    </div>
                                </div>
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">Data Type (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxUserDefinedDataTypeSelectBox"></div>
                                        <div class="invalid-feedback" id="UserDefinedDataTypeValidation"></div>
                                    </div>
                                </div>
                                <div class="mb-15px">
                                    <label class="form-label col-form-label col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxUserDefinedSupportGroupSelectBox"></div>
                                        <div class="invalid-feedback" id="UserDefinedSupportGroupValidation"></div>
                                    </div>
                                </div>
                                <div class="row mb-15px">
                                    <div class="col-md-4">
                                        <div class="mt-2 mb-2">
                                            <div type="text" id="dxUserDefinedIsActiveCheckBox"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mt-2 mb-2">
                                            <div type="text" id="dxUserDefinedIsMandatoryCheckBox"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="UserDefinedActionButtons"></div>
                            </div>
                            <div class="col-md-8">
                                <div id="dxUserDefinedDataGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Add new item Modal -->
    <div class="modal fade" id="AddNewItemLineModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Add new item</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-pills mb-2" role="tablist">
                        <li class="nav-item" role="presentation">
                            <a href="#ItemLineTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                                <span class="d-sm-none">Item</span>
                                <span class="d-sm-block d-none">Item</span>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a href="#AttachmentsLineTab" data-bs-toggle="tab" class="nav-link" aria-selected="true" role="tab">
                                <span class="d-sm-none">Attachments</span>
                                <span class="d-sm-block d-none">Attachments</span>
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content rounded-0 m-0">
                        <div class="tab-pane fade active show" id="ItemLineTab" role="tabpanel">
                            <div class="panel panel-inverse">
                                <div class="panel-body">
                                    <div class="row mb-15px">
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Item Type (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineItem_HeaderSelectBox"></div>
                                                <div class="invalid-feedback" id="Item_LineItem_HeaderValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineSupportGroupSelectBox"></div>
                                                <div class="invalid-feedback" id="Item_LineSupportGroupValidation"></div>
                                            </div>
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
                                        <label class="form-label col-form-label col-md-12">Legacy ID</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineLegacyIDTextBox"></div>
                                            <div class="invalid-feedback" id="Item_LineLegacyIDValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-15px">
                                        <label class="form-label col-form-label col-md-12">Manufacture Serial ID (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineManufactureSerialIDTextBox"></div>
                                            <div class="invalid-feedback" id="Item_LineManufactureSerialIDValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-15px">
                                        <label class="form-label col-form-label col-md-12">Supply Type(<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineSupplyTypeSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineSupplyTypeValidation"></div>
                                        </div>
                                    </div>
                                    <div class="row mb-15px d-none" id="SupplyTypeLocal">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">PO Number (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LinePONumberTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LinePONumberValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">PO Line (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LinePOLineTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LinePOLineValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-15px d-none" id="SupplyTypeImport">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Import Invoice (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineImportInvoiceTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineImportInvoiceValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Shipment Receipt # (AV)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineShipmentReceiptNumberTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineShipmentReceiptNumberValidation"></div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class=" row mb-15px">
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Admission Date (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineIntroductionDateDateBox"></div>
                                                <div class="invalid-feedback" id="Item_LineIntroductionDateValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Country of Origin (COO) (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineCOOTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineCOOValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-15px">
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Base Price (MXN) (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineBasePriceMXNTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineBasePriceMXNValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="form-label col-form-label col-md-12">Base Price (USD) (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineBasePriceUSDTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineBasePriceUSDValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mb-15px">
                                        <label class="form-label col-form-label col-md-12">Station (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineStationSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineStationValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-15px">
                                        <label class="form-label col-form-label col-md-12">Status <%--(<span class="text-danger">*</span>)--%></label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineStatusSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineStatusValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-15px">
                                        <label class="form-label col-form-label col-md-12">Comments </label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineCommentsTextArea"></div>
                                        </div>
                                    </div>
                                    <div id="UserDefinedTemplateSection" class="mb-15px">
                                        <%--<label class="form-label col-form-label col-md-12">Comments </label>
                                    <div class="col-md-12">
                                        <input class="form-control" />
                                    </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="AttachmentsLineTab" role="tabpanel">
                            <div class="panel panel-inverse">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <span class="fs-11px">Maximum file size: <span>5 MB</span>.</span>
                                            <div id="dxItem_LineAttachmentFileUploader"></div>
                                        </div>
                                        <div class="col-md-12">
                                            <div id="Item_LineFilelistSection" class="row"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <div id="Item_LineActionButtons"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <%-- Assign user defined to view--%>
    <div class="modal fade" id="AssignUserDefinedFieldsModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog" style="width: 1180px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Custom fields List</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel-body">
                        <div class="row justify-content-between">
                            <div class="col-md-12">
                                <div id="dxUserDefinedList"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary float-end" data-bs-dismiss="modal" type="button">Cancel</button>
                    <button class="btn btn-success me-1 m-b-15 float-end" id="UpdateUserDefinedTemplateButton" type="button">Update</button>
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
    <input type="hidden" id="hiddenItem_HeaderID" hidden />
    <input type="hidden" id="hiddenItem_SupportGroupID" hidden />
    <input type="hidden" id="hiddenItem_LineID" hidden />
    <input type="hidden" id="hiddenUserDefinedID" />
    <input type="hidden" id="hiddenUserSupportGroupID" hidden />
    <input type="hidden" id="hiddenStationID" hidden />

    <script type="module" src="/App/Features/Maintenance/AMS/ItemManagement/ItemAdministration.js?v=2"></script>
</asp:Content>
