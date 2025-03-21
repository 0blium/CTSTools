<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SparePartInventory.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.AMS.SparePartManagement.SparePartInventory.SparePartInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="SparePartInventory.css" rel="stylesheet" />
<div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Support Groups</a></li>
                <li class="breadcrumb-item active">Spare Part Inventory</li>
            </ol>
        </div>
        <div class="row">
            <h1 class="page-header">Spare Part Inventory</h1>
        </div>
    </div>
    <%--tab section--%>
    <div id="tabsContainer" class="<%--sticky-top--%> ">
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item" role="presentation">
                <a href="#SparePart-tab" data-bs-toggle="tab" class="default-nav-link nav-link active " aria-selected="true" role="tab">
                    <span class="d-sm-none">Spare Part</span>
                    <span class="d-sm-block d-none">Spare Part</span>
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="Inventory-Tab" href="#sparePart-inventory-tab" data-bs-toggle="tab" class="default-nav-link nav-link disabled" aria-selected="false" role="tab" tabindex="-1">
                    <span class="d-sm-none">Inventory</span>
                    <span class="d-sm-block d-none">Inventory</span>
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="SparePart-lot-tab" href="#sparePart-lot" data-bs-toggle="tab" class="default-nav-link nav-link disabled" aria-selected="false" role="tab" tabindex="-1">
                    <span class="d-sm-none">Lot Information</span>
                    <span class="d-sm-block d-none">Lot Information</span>
                </a>
            </li>
        </ul>
        <div class="tab-content panel borderCustom rounded-0 pt-4 p-3 m-0">
            <!-- BEGIN tab-pane -->
            <!-- Spare Part Information Tab-->
            <div class="tab-pane fade active show" id="SparePart-tab" role="tabpanel">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="col-md-12 text-center">
                                    <img id="SparePartThumbnail" src="/App/Common/Assets/img/no-product-image.png" class="img-fluid" style="height: 200px;" />
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-6 mb-2">
                                        <label class="form-label col-form-label  col-md-12">Name</label>
                                        <div class="col-md-12">
                                            <div id="dxSparePartNameTextBox"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mb-2">
                                        <label class="form-label col-form-label  col-md-12">Manufacture ID</label>
                                        <div class="col-md-12">
                                            <div id="dxSparePartManufactureIDTextBox"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 mb-2">
                                        <label class="form-label col-form-label col-md-12">Description</label>
                                        <div class=" col-md-12">
                                            <div id="dxSparePartDescriptionTextArea"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <%--<div class="col-md-6 mb-2">--%>
                                    <label class="form-label col-form-label  col-md-auto">Is Active?</label>
                                    <div class="col-md-1">
                                        <div class="mt-2 mb-2">
                                            <div type="text" id="dxSparePartIsActiveCheckBox"></div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
                                </div>
                                <div class="row" id="SparePartActionButtons"></div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- END tab-pane -->
            <!-- BEGIN tab-pane -->
            <!-- Spare Part Inventory Information Tab-->
            <div class="tab-pane fade" id="sparePart-inventory-tab" role="tabpanel">
                <div class="row ">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4 mb-2">
                                <label class="form-label col-form-label pb-0 col-md-12">Min </label>
                                <div class="col-md-12">
                                    <div id="dxSparePartInventoryMinQtyNumberBox"></div>
                                </div>
                            </div>
                            <div class="col-md-4 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Max </label>
                                <div class="col-md-12">
                                    <div id="dxSparePartInventoryMaxQtyNumberBox"></div>
                                </div>
                            </div>
                            <div class="col-md-4 mb-2">
                                <label class="form-label col-form-label pb-0 col-md-12">Available Qty </label>
                                <div class="col-md-12">
                                    <div id="dxSparePartInventoryAvailableQtyTextBox"></div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <%--<div class="col-md-6 mb-2">--%>
                            <label class="form-label col-form-label  col-md-auto">Is Active?</label>
                            <div class="col-md-1">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxSparePartInventoryIsActiveCheckBox"></div>
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="row mb-2" id="SparePartInventoryActionButtons"></div>
                        <div id="dxSparePartInventorySparePartGrid"></div>
                    </div>
                </div>
            </div>
            <!-- END tab-pane -->
            <!-- BEGIN tab-pane -->
            <!-- Spare Part Lot Information Tab-->
            <div class="tab-pane fade " id="sparePart-lot" role="tabpanel">
                <div class="row">
                    <div class="col-md-12 mb-2">
                        <div class="row mb-2">
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Part Number </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotPartNumberTextBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotPartNumberValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Provider </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotProviderSelectBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotProviderValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Qty </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotQtyNumberBox"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Available Qty </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotAvailableQtyTextBox"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Serial </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotSerialTextBox"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Transaction Origin </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotTransactionOriginSelectBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotTransactionOriginValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label pb-0 col-md-12">Transaction Number</label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotTransactionNumberTextBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotTransactionNumberValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Transaction Line</label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotTransactionLineNumberBox"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label pb-0 col-md-12">Lot Cost </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotCostNumberBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotCostValidation"></div>
                                </div>
                            </div>
                            <div class="col-md-3 mb-2">
                                <label class="form-label col-form-label  pb-0 col-md-12">Unit Cost </label>
                                <div class="col-md-12">
                                    <div id="dxSparePart_LotUnitCostNumberBox"></div>
                                    <div class="invalid-feedback" id="SparePart_LotUnitCostValidation"></div>
                                </div>
                            </div>
                            <div type="text" id="dxSparePart_LotIsActiveCheckBox"></div>
                        </div>
                        <div class="row mb-2" id="SparePartLotActionButtons"></div>
                        <div id="dxSparePartLotGrid"></div>
                    </div>
                </div>
            </div>
            <!-- END tab-pane -->
        </div>
    </div>
    <div class="col-md-12 mt-3">
        <div class="panel panel-inverse">
            <div class="panel-heading p-2">
                <h4 class="panel-title ">Spare Part List</h4>
                <div class="panel-heading-btn">
                    <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex">
                            <a class="btn btn-link text-info d-flex align-items-center text-decoration-none" data-bs-toggle="modal" data-bs-target="#SparePartInventoryModal">
                                <i class="fas fa-circle-plus fs-20px text-info me-1"></i>Can't find the part you are looking for? Click here 
                            </a>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div id="dxSparePartInventoryGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!-- Modal -->
    <!-- New SparePart inventory -->
    <div class="modal fade" id="SparePartInventoryModal" data-bs-backdrop="static">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 65%;">
            <div class="modal-content" style="background-color: #DEE2E6;">
                <div class="modal-header">
                    <h5 class="modal-title">Create new inventory</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="CloseModalXButton" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="panel panel-inverse">
                        <div class="panel-body">
                            <div class="row ">
                                <div class="col-md-12">
                                    <div class="row mb-2">
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label pb-0 col-md-12">Spare Part </label>
                                            <div class="col-md-12">
                                                <div id="dxSparePartInventorySparePartTextBox"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Support Group </label>
                                            <div class="col-md-12">
                                                <div id="dxSparePartInventorySupportGroupModalSelectBox"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label col-xl-3 pb-0 col-md-12">Min </label>
                                            <div class="col-md-12">
                                                <div id="dxSparePartInventoryMinQtyModalNumberBox"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label col-xl-3 pb-0 col-md-12">Max </label>
                                            <div class="col-md-12">
                                                <div id="dxSparePartInventoryMaxQtyModalNumberBox"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3" id="SparePartInventoryModalActionButtons"></div>
                                    <div id="dxSparePartInventoryModalGrid"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    
                    <%--<button type="button" id="CloseModalButton" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>--%>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hiddenSparePartInventoryID" />
<input type="hidden" id="hiddenSparePartLotID" />
<input type="hidden" id="hiddenSparePartID" />
<input type="hidden" id="hiddenSupportGroupID" />
<input type="hidden" id="hiddenSparePartModalID" />
<script type="module" src="/App/Features/Maintenance/AMS/SparePartManagement/SparePartInventory.js?v=1"></script>
</asp:Content>
