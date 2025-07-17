<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ViewTicket.aspx.cs" Inherits="CTSTools.WEB.App.Features.Maintenance.Tickets.ViewTicket.ViewTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ViewTicket.css" rel="stylesheet" />
    <div id="TicketContent">
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Tickets</a></li>
                    <li class="breadcrumb-item active">View Ticket</li>
                </ol>
            </div>
            <div class="row">
                <div class="col-12 row p-0 m-0">

                    <h1 class="page-header mb-1 col-auto me-10px" id="TicketNumber"></h1>

                    <h3 class="page-header mb-1 col-auto" id="CreatedBySection"></h3>
                </div>
                <div class="col-auto mb-2 px-0 align-items-center d-flex">
                    <h3 class="page-header me-1 mb-0">Status :</h3>
                    <div id="TicketStatus"></div>
                </div>
            </div>
        </div>
        <%--tab section--%>
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item" role="presentation">
                <a href="#DataEntry-tab" data-bs-toggle="tab" class="default-nav-link nav-link active " aria-selected="true" role="tab">
                    <span class="d-sm-none">Details</span>
                    <span class="d-sm-block d-none">Details</span>
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="Attachment-tab-button" href="#Attachments-tab" data-bs-toggle="tab" class="default-nav-link nav-link " aria-selected="false" role="tab" tabindex="-1">
                    <span class="d-sm-none">Attachments</span>
                    <span class="d-sm-block d-none">Attachments</span>
                </a>
            </li>
        </ul>
        <div class="tab-content panel borderCustom rounded-0 p-3 m-0">
            <!-- BEGIN tab-pane -->
            <!-- Spare Part Information Tab-->
            <div class="tab-pane fade active show" id="DataEntry-tab" role="tabpanel">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="textColor float-start">General Information</h4>
                        <a id="SaveButton" class="btn btn-success float-end" hidden>Save</a>

                    </div>
                    <div class=" bg-white" <%--id="stickyTop"--%>>

                        <div class="col-md-12 mb-0">
                            <div class="row mb-2">
                                <div class="col-md-12 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Title </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketTitleTextBox"></div>
                                        <div class="invalid-feedback" id="TicketTitleValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Description  </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketDescriptionTextArea"></div>
                                        <div class="invalid-feedback" id="TicketDescriptionValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Note </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketNoteTextArea"></div>
                                        <div class="invalid-feedback" id="TicketNoteValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Facility </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketFacilitySelectBox"></div>
                                        <div class="invalid-feedback" id="TicketFacilityValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Department </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketDepartmentSelectBox"></div>
                                        <div class="invalid-feedback" id="TicketDepartmentValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Support Group </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketSupportGroupSelectBox"></div>
                                        <div class="invalid-feedback" id="TicketSupportGroupValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Item </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketItemLookup"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Priority </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketPrioritySelectBox"></div>
                                        <div class="invalid-feedback" id="TicketPriorityValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Category </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketCategorySelectBox"></div>
                                        <div class="invalid-feedback" id="TicketCategoryValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Subcategory </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketSubcategorySelectBox"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Third Category </label>
                                    <div class="col-md-12">
                                        <div id="dxTicketThirdLevelCategorySelectBox"></div>
                                    </div>
                                </div>

                            </div>

                            <%--<hr class="textColor" />--%>
                        </div>
                    </div>
                    <div class="col-md-12 mb-2">
                        <div class="row mb-2">
                        </div>
                    </div>
                    <!--Accordion Start-->
                    <div class="accordion">
                        <div class="accordion-item ">
                            <h3 class="accordion-header" id="headingOne">

                                <button class="accordion-button accordionHeaderCustom px-2 accordionBtnText pointer-cursor" type="button" data-bs-toggle="collapse" data-bs-target="#FollowUp" aria-expanded="true">
                                    Follow Up
                                </button>
                            </h3>
                            <div id="FollowUp" class="accordion-collapse collapse show" data-bs-parent="#accordion" style="">
                                <div class="accordion-body px-0">
                                    <div class="row mb-2">
                                        <div class="col-md-6 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Solution  </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketSolutionTextArea"></div>
                                                <div class="invalid-feedback" id="TicketSolutionValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Resolution </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketResolutionTextArea"></div>
                                                <div class="invalid-feedback" id="TicketResolutionValidation"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Status </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketStatusSelectBox"></div>
                                                <div class="invalid-feedback" id="TicketStatusValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Created Date </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketAddedDateDateBox"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Assigned To </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketAssignedToSelectBox"></div>
                                                <div class="invalid-feedback" id="TicketAssignedToValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Assigned Date </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketAssignedDateDateBox"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Closed By </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketClosedBySelectBox"></div>
                                                <div class="invalid-feedback" id="TicketClosedByValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Closed Date </label>
                                            <div class="col-md-12">
                                                <div id="dxTicketClosedDateDateBox"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Accordion end-->
                    <!--Accordion Start-->
                    <div class="accordion" id="SparePartAccordion" hidden>
                        <div class="accordion-item ">
                            <h3 class="accordion-header" id="headingTwo">

                                <button class="accordion-button accordionHeaderCustom px-2 accordionBtnText pointer-cursor collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#SparePartUsage">
                                    Spare Part
                                </button>
                            </h3>
                            <div id="SparePartUsage" class="accordion-collapse collapse " data-bs-parent="#accordion" style="">
                                <div class="accordion-body px-0">

                                    <div class="row mb-2">
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Spare Part </label>
                                            <div class="col-md-12">
                                                <div id="dxSparePart_LotSelectBox"></div>
                                                <div class="invalid-feedback" id="TicketSparePartValidation"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-2">
                                            <label class="form-label col-form-label  pb-0 col-md-12">Quantity </label>
                                            <div class="input-group col-12 flex-nowrap">
                                                <div class="col-10" id="dxSparePartUsageQuantityNumberBox"></div>
                                                <div class="input-group-text col-2 py-0 justify-content-center fw-bold numberBorder" id="sparePart_LotAvailableQty">/ 0</div>
                                            </div>
                                            <div class="invalid-feedback" id="SparePartUsageQuantityValidation"></div>
                                        </div>
                                        <div class="col-md-3 mb-2 mt-3 d-flex align-items-end">
                                            <button id="CreateSparePartUsageButton" class="btn btn-primary btn-sm px-4  col-12 col-md-auto  ms-0" disabled><i class="fa fa-circle-plus me-1"></i>Add</button>
                                        </div>

                                    </div>
                                    <div id="dxSparePartUsageGrid"></div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Accordion end-->

                </div>

            </div>
            <!-- END tab-pane -->
            <!-- BEGIN tab-pane -->
            <!-- Attachments Information Tab-->
            <div class="tab-pane fade " id="Attachments-tab" role="tabpanel">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="note alert-primary noteCustom mb-2">
                                <div class="note-icon"><i class="fa fa-lightbulb"></i></div>
                                <div class="note-content">
                                    <h4><b>The maximun size per file is 5 MB</b></h4>
                                    <p>
                                        File extension allow: .jpg, .jpeg, .gif, .png, .pdf, .docx								
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-3" id="dxTicketFileUploader"></div>
                            <div id="TicketFilelist" class="row"></div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- END tab-pane -->
        </div>
    </div>
    <input type="hidden" id="hiddenTicketID" />
    <input type="hidden" id="hiddenSupportGroupID" />
    <input type="hidden" id="hiddenSparePartInventoryID" />
    <input type="hidden" id="hiddenSparePartUsageID" />
    <input type="hidden" id="hiddenCreatedByID" />
    <input type="hidden" id="hiddenCurrentStatusID" />
    <script type="module" src="/App/Features/Maintenance/Tickets/ViewTicket/ViewTicket.js"></script>
</asp:Content>
