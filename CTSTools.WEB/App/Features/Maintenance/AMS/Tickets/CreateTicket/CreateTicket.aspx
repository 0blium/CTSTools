<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="CreateTicket.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.Tickets.CreateTicket.CreateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CreateTicket.css" rel="stylesheet" />
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Tickets</a></li>
                </ol>
            </div>
            <div class="row">
                <h1 class="page-header">Create Ticket</h1>
            </div>
            <div class="col-md-12" id="success-message"></div>
        </div>
        <%--tab section--%>
        <div id="tabsContainer" class="<%--sticky-top--%> ">
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item" role="presentation">
                    <a href="#DataEntry-tab" data-bs-toggle="tab" class="default-nav-link nav-link active " aria-selected="true" role="tab">
                        <span class="d-sm-none">Data Entry</span>
                        <span class="d-sm-block d-none">Data Entry</span>
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
                            <a id="SubmitButton" class="btn btn-success float-end">Submit Ticket</a>
                        </div>
                        <div class="col-md-12 mb-3">
                            
                            <div class="row mb-3">
                                <div class="col-md-12 mb-3">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Title (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxTicketTitleTextBox"></div>
                                        <div class="invalid-feedback" id="TicketTitleValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Description (<span class="text-danger">*</span>) </label>
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
                            </div>
                            <hr />
                            <div class="row mb-3">
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Facility (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxTicketFacilitySelectBox"></div>
                                        <div class="invalid-feedback" id="TicketFacilityValidation"></div>
                                    </div>
                                </div>

                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Department (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxTicketDepartmentSelectBox"></div>
                                        <div class="invalid-feedback" id="TicketDepartmentValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Support Group (<span class="text-danger">*</span>)</label>
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
                            </div>
                            <div class="row mb-3">

                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Priority (<span class="text-danger">*</span>)</label>
                                    <div class="col-md-12">
                                        <div id="dxTicketPrioritySelectBox"></div>
                                        <div class="invalid-feedback" id="TicketPriorityValidation"></div>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2">
                                    <label class="form-label col-form-label  pb-0 col-md-12">Category (<span class="text-danger">*</span>)</label>
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
                            <div class="row mb-2">
                            </div>
                        </div>
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


    </div>
    <script type="module" src="/App/Features/Maintenance/AMS/Tickets/CreateTicket/CreateTicket.js?v=1"></script>
</asp:Content>
