<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SupplyTypeCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.Item.SupplyType.SupplyTypeCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container-fluid">
            <div class="row">
                <ol class="breadcrumb float-xl-start">
                    <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                    <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
                    <li class="breadcrumb-item active">Supply Type Catalog</li>
                </ol>
            </div>
            <!-- END breadcrumb -->
            <!-- BEGIN page-header -->
            <div class="row">
                <h1 class="page-header">Supply Type Catalog</h1>
            </div>
        </div>
        <!-- END page-header -->
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-inverse">
                    <div class="panel-heading">
                        <h4 class="panel-title">Supply Type Form</h4>
                        <div class="panel-heading-btn">
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxSupplyTypeNameTextBox"></div>
                                <div class="invalid-feedback" id="SupplyTypeNameValidation"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                            <div class="col-xl-9 col-md-12">
                                <div id="dxSupplyTypeDescriptionTextArea"></div>
                            </div>
                        </div>
                        <div class="row mb-15px">
                            <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                            <div class="col-xl-9 col-md-auto">
                                <div class="mt-2 mb-2">
                                    <div type="text" id="dxSupplyTypeIsActiveCheckBox"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="SupplyTypeActionButtons"></div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="panel panel-inverse">
                    <div class="panel-heading">
                        <h4 class="panel-title">Supply Type Information</h4>
                        <div class="panel-heading-btn">
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="dxSupplyTypeGrid"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenSupplyTypeID" hidden />
    <script type="module" src="/App/Features/Ticket/Item/SupplyType/SupplyTypeCatalog.js"></script>
</asp:Content>
