<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ItemRegistration.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.ItemRegistration.ItemRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Receiving</a></li>
                <li class="breadcrumb-item active">Item Registration</li>
            </ol>
        </div>
        <div class="row">
            <h1 class="page-header">Item Registration</h1>
        </div>
    </div>
    <%--tab section--%>
    <ul class="nav nav-tabs" role="tablist">
        <li class="nav-item" role="presentation">
            <a href="#item-inventory-tab" data-bs-toggle="tab" class="default-nav-link nav-link active" aria-selected="true" role="tab">
                <span class="d-sm-none">Tab 1</span>
                <span class="d-sm-block d-none">Item Inventory</span>
            </a>
        </li>
        <li class="nav-item" role="presentation">
            <a id="Departure-log-tab" href="#item-departure-log" data-bs-toggle="tab" class="default-nav-link nav-link" aria-selected="false" role="tab" tabindex="-1">
                <span class="d-sm-none">Tab 2</span>
                <span class="d-sm-block d-none">Departure Log</span>
            </a>
        </li>
    </ul>
    <div class="tab-content panel rounded-0 p-3 m-0">
        <!-- BEGIN tab-pane -->
        <div class="tab-pane fade active show" id="item-inventory-tab" role="tabpanel">
            <div class="row">
                <div class="col-md-12">
                    <div class="d-flex">
                        <a id="AddNewItemLineBtn" class="btn btn-success mb-1" data-bs-toggle="modal" data-bs-target="#AddNewItemLineModal">Register Item</a>
                        <a id="AddNewItemHeaderBtn" class="btn btn-secondary mb-1 ms-1" data-bs-toggle="modal" data-bs-target="#AddNewItemHeaderModal">Add Item Type</a>
                        <a class="btn btn-link text-info d-flex align-items-center text-decoration-none" data-bs-toggle="modal" data-bs-target="#AssetCriteriaModal">
                            <i class="fas fa-circle-question fs-20px text-info me-1"></i>¿Que&#769; activos registrar? 
                        </a>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div id="dxItem_HeaderDatGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END tab-pane -->
        <!-- BEGIN tab-pane -->
        <div class="tab-pane fade" id="item-departure-log" role="tabpanel">
            <div class="row ">
                <div class="col-12">
                    <p class="p-0 mb-1">Select filters to begin your search</p>
                    <div class="mb-1 d-flex filter-section align-items-baseline">
                        <a class="btn btn-dark mb-1" id="OpenFilterItem_Line" data-bs-toggle="collapse" href="#collapseFilterItem_Line" role="button" aria-expanded="false" aria-controls="collapseExample"><i class="fas fa-arrow-down-wide-short me-2"></i>Filters</a>
                        <div class="d-flex align-items-center fw-400 mb-2 showFilter" id="FiltersItem_LineApply">
                        </div>
                    </div>
                    <div class="collapse" id="collapseFilterItem_Line">
                        <div class="p-3 row">
                            <div class="form-group col-xl-4 col-lg-6">
                                <label class="col-form-label col-xl-12 col-lg-12">Received Date</label>
                                <div class="col-xl-12 col-lg-12 d-flex align-items-baseline">
                                    <p class="pe-2">from</p>
                                    <div class="m-b-5" id="dxItem_LineStartDateDateBox"></div>
                                    <p class="px-2">to</p>
                                    <div class="m-b-5" id="dxItem_LineEndDateDateBox"></div>
                                </div>
                            </div>
                            <div class="form-group col-xl-4 col-lg-6">
                                <label class="col-form-label col-xl-12 col-lg-12">Supply Type</label>
                                <div class="col-xl-12 col-lg-12">
                                    <div id="dxItem_LineSupplyTypeTagBox"></div>
                                </div>
                            </div>
                            <div class="form-group col-xl-4 col-lg-6">
                                <label class="col-form-label col-xl-12 col-lg-12">Owner</label>
                                <div class="col-xl-12 col-lg-12">
                                    <div class="m-b-5" id="dxItem_LineOwnerTagBox"></div>
                                </div>
                            </div>
                            <div class="form-group col-xl-4 col-lg-6">
                                <label class="col-form-label col-xl-12 col-lg-12">Station</label>
                                <div class="col-xl-12 col-lg-12">
                                    <div class="m-b-5" id="dxItem_LineStationTagBox"></div>
                                </div>
                            </div>
                            <div class="form-group col-xl-4 col-lg-6">
                                <label class="col-form-label col-xl-12 col-lg-12">Delivered To</label>
                                <div class="col-xl-12 col-lg-12">
                                    <div class="m-b-5" id="dxItem_LineDeliveredToTagBox"></div>
                                </div>
                            </div>
                            <div class="col-12 d-flex justify-content-end mt-2">
                                <a class="btn btn-secondary mb-1" id="ClearItem_LineFilters">Reset</a>
                                <a class="btn btn-success mb-1 ms-1" id="GetItem_LineInformation">Search</a>
                            </div>
                        </div>
                    </div>
                    <div id="dxItemLineGrid"></div>
                </div>
            </div>
        </div>
        <!-- END tab-pane -->
    </div>

    <!-- Add new item type modal -->
    <div class="modal fade" id="AddNewItemHeaderModal" tabindex="-1" data-bs-keyboard="true" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="background-color: #DEE2E6;">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Add new item</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="HeaderModalCloseButton" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-pills mb-2" role="tablist">
                        <li class="nav-item" role="presentation">
                            <a href="#ItemHeaderTab" data-bs-toggle="tab" class="nav-link active" aria-selected="true" role="tab">
                                <span class="d-sm-none">Item</span>
                                <span class="d-sm-block d-none">Item</span>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation">
                            <a href="#AttachmentsTab" data-bs-toggle="tab" class="nav-link" aria-selected="true" role="tab">
                                <span class="d-sm-none">Attachments</span>
                                <span class="d-sm-block d-none">Attachments</span>
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content rounded-0 m-0">
                        <div class="tab-pane fade active show" id="ItemHeaderTab" role="tabpanel">
                            <div class="panel panel-inverse">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <span class="fs-11px">Maximum file size: <span>5 MB</span>.</span>
                                        <div id="dxItem_HeaderThumbnailFileUploader"></div>
                                        <div class="col-md-12 text-center">
                                            <img id="ItemThumbnail" src="/App/Common/Assets/img/no-product-image.png" class="img-fluid" style="height: 200px;" />
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">English Name (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_HeaderEnglishNameTextBox"></div>
                                            <div class="invalid-feedback" id="Item_HeaderEnglishNameValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Spanish Name (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_HeaderSpanishNameTextBox"></div>
                                            <div class="invalid-feedback" id="Item_HeaderSpanishNameValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Model (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_HeaderModelTextBox"></div>
                                            <div class="invalid-feedback" id="Item_HeaderModelValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Brand (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_HeaderBrandTextBox"></div>
                                            <div class="invalid-feedback" id="Item_HeaderBrandValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Item Classification</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_HeaderItemClassificationSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_HeaderItemClassificationValidation"></div>
                                        </div>
                                    </div>
                                    <div class="row mb-10px">
                                        <div class="col-md-4">
                                            <div id="dxItem_HeaderIsActiveCheckBox"></div>
                                        </div>
                                        <div class="col-md-4">
                                            <div id="dxItem_HeaderIsESDCheckBox"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="AttachmentsTab" role="tabpanel">
                            <div class="panel panel-inverse">
                                <div class="panel-body">
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
                                                <a href="#" id="TreeViewExpandBtn" class="btn btn-default">Expand all</a>
                                                <a href="#" id="TreeViewCollapseBtn" class="btn btn-default">Collapse all</a>
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
                            </div>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <div id="Item_HeaderActionButtons"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <%--Item Line Modal--%>
    <div class="modal fade" id="AddNewItemLineModal" tabindex="-1" data-bs-keyboard="true" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="background-color: #DEE2E6;">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Add new item</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="LineModalCloseButton" aria-label="Close"></button>
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
                                    <div class="mb-15px">
                                        <div class="col-md-12">
                                            <label class="form-label col-form-label col-md-12">Item Type (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineItem_HeaderSelectBox"></div>
                                                <div class="invalid-feedback" id="Item_LineItem_HeaderValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Owner (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineOwnerSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineOwnerValidation"></div>
                                        </div>
                                    </div>
                                    <div class="row mb-10px">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Asset # (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineSerialTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineSerialValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Legacy ID</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineLegacyIDTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineLegacyIDValidation"></div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Manufacture Serial ID (<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineManufactureSerialIDTextBox"></div>
                                            <div class="invalid-feedback" id="Item_LineManufactureSerialIDValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Supply Type(<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineSupplyTypeSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineSupplyTypeValidation"></div>
                                        </div>
                                    </div>
                                    <div class="mb-10px">
                                        <label class="form-label col-form-label col-md-12">Transaction Origin(<span class="text-danger">*</span>)</label>
                                        <div class="col-md-12">
                                            <div id="dxItem_LineTransactionOriginSelectBox"></div>
                                            <div class="invalid-feedback" id="Item_LineTransactionOriginValidation"></div>
                                        </div>
                                    </div>
                                    <div class="row mb-10px" id="SupplyTypeLocal">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Transaction Number (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineTransactionNumberTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineTransactionNumberValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Transaction Line (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineTransactionLineTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineTransactionLineValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-10px d-none" id="SupplyTypeImport">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Import Invoice (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineImportInvoiceTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineImportInvoiceValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Import Invoice Line (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineImportInvoiceLineTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineImportInvoiceLineValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Shipment Receipt # (AV) (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineShipmentReceiptNumberTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineShipmentReceiptNumberValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Declaration # (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineDeclarationNumberTextBox"></div>
                                                <div class="invalid-feedback" id="Item_LineDeclarationNumberValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" row mb-10px">
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
                                    <div class="row mb-10px">
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
                                    <div class="row mb-10px">
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Station (<span class="text-danger">*</span>)</label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineStationSelectBox"></div>
                                                <div class="invalid-feedback" id="Item_LineStationValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label class="form-label col-form-label col-md-12">Status <%--(<span class="text-danger">*</span>)--%></label>
                                            <div class="col-md-12">
                                                <div id="dxItem_LineStatusSelectBox"></div>
                                                <div class="invalid-feedback" id="Item_LineStatusValidation"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="mb-10px">
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
    <%--Deliver item to Modal--%>
    <div class="modal fade" id="ItemDeliverToModal" tabindex="-1" data-bs-keyboard="true" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="background-color: #DEE2E6;">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Deliver Item To</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel panel-inverse">
                        <div class="panel-body">
                            <div class="mb-10px">
                                <label class="form-label col-form-label col-md-12">Deliver To (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineDeliverToIDSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineDeliverToValidation"></div>
                                </div>
                            </div>
                            <div class="mb-10px">
                                <label class="form-label col-form-label col-md-12">Support Group (<span class="text-danger">*</span>)</label>
                                <div class="col-md-12">
                                    <div id="dxItem_LineSupportGroupSelectBox"></div>
                                    <div class="invalid-feedback" id="Item_LineSupportGroupValidation"></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-end">
                        <div>
                            <a class="btn btn-success" id="SaveDeliverToInfo">Save</a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="AssetCriteriaModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="background-color: #DEE2E6;">
                <div class="modal-header">
                    <h5 class="modal-title">Criterios para registro de un nuevo activo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel panel-inverse">
                        <div class="panel-body">
                            <p>
                                Se registrara&#769; cualquier activo que sea <span class="text-decoration-underline fw-600">propiedad de la empresa</span>, 
                        pueda ser <span class="text-decoration-underline fw-600">administrado</span> o se le de <span class="text-decoration-underline fw-600">mantenimiento</span> por un grupo de personas, como lo pueden ser:
                            </p>
                            <ul>
                                <li>Herramientas de trabajo</li>
                                <li>Maquinaria o equipos</li>
                                <li>Equipo de computo</li>
                                <%--<li>Tonners</li>--%>
                                <li>Inmobiliario</li>
                                <li>Fixtures</li>
                                <li>Monitores</li>
                                <li>Dispositivos perifericos (rato&#769;n, teclado, audifonos, webcam, etc)</li>
                                <li>Ca&#769;mara Fotogra&#769;fica</li>
                                <li>Extintores</li>
                                <li>Tele&#769;fonos</li>
                                <li>Aires acondicionados</li>
                                <li>Entre otros</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hiddenItem_HeaderID" />
<input type="hidden" id="hiddenItem_LineID" />
<script type="module" src="/App/Features/Maintenance/AMS/ItemManagement/ItemRegistration.js"></script>
</asp:Content>
