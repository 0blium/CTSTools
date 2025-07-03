<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="TicketConsult.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.Tickets.Consult.TicketConsult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">Tickets</a></li>
                <li class="breadcrumb-item active">Ticket Consult</li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Ticket Consult</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-xl-12">
            <div class="panel panel-inverse">
                <%--<div class="panel-heading">
                    <h4 class="panel-title"></h4>
                    <div class="panel-heading-btn">
                        <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>
                    </div>
                </div>--%>
                <div class="panel-body">
                    <div class="row ">
                        <div class="col-12">
                            <p class="p-0 mb-1">Select filters to begin your search</p>
                            <div class="mb-1 d-flex filter-section align-items-baseline flex-wrap">
                                <a class="btn btn-dark mb-1" id="OpenFilterTicket" data-bs-toggle="collapse" href="#collapseFilterTicket" role="button" aria-expanded="false" aria-controls="collapseExample"><i class="fas fa-arrow-down-wide-short me-2"></i>Filters</a>
                                <div class="d-flex align-items-center fw-400 mb-2 showFilter  flex-wrap" id="FiltersTicketApply">
                                </div>
                            </div>
                            <div class="collapse" id="collapseFilterTicket">
                                <div class="p-3 row">
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12">Requested Date</label>
                                        <div class="col-xl-12 col-lg-12 d-flex align-items-baseline">
                                            <p class="pe-2">from</p>
                                            <div class="m-b-5" id="dxTicketStartDateDateBox"></div>
                                            <p class="px-2">to</p>
                                            <div class="m-b-5" id="dxTicketEndDateDateBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12"># Ticket</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div id="dxTicketNumberNumberBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12">Facility</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div id="dxTicketFacilityTagBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12">Support Group</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div class="m-b-5" id="dxTicketSupportGroupTagBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12">Priority</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div class="m-b-5" id="dxTicketPriorityTagBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6" id="ItemContainer" hidden>
                                        <label class="col-form-label col-xl-12 col-lg-12">Item</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div class="m-b-5" id="dxTicketItemTagBox"></div>
                                        </div>
                                    </div>
                                    <div class="form-group col-xl-4 col-lg-6">
                                        <label class="col-form-label col-xl-12 col-lg-12">Status</label>
                                        <div class="col-xl-12 col-lg-12">
                                            <div class="m-b-5" id="dxTicketStatusTagBox"></div>
                                        </div>
                                    </div>
                                    <div class="col-12 d-flex justify-content-end mt-2">
                                        <a class="btn btn-secondary mb-1" id="ClearTicketFilters">Reset</a>
                                        <a class="btn btn-success mb-1 ms-1" id="GetTicketInformation">Search</a>
                                    </div>
                                </div>
                            </div>
                            <div id="dxTicketGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="module" src="/App/Features/Maintenance/AMS/Tickets/Consult/TicketConsult.js"></script>
</asp:Content>
