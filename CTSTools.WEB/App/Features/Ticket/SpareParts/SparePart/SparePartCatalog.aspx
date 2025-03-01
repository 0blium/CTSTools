<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="SparePartCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Ticket.SpareParts.SparePart.SparePartCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
    <div class="row">
        <ol class="breadcrumb float-xl-start">
            <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
            <li class="breadcrumb-item"><a href="javascript:;">Settings</a></li>
            <li class="breadcrumb-item active">SparePart Catalog</li>
        </ol>
    </div>
    <!-- END breadcrumb -->
    <!-- BEGIN page-header -->
    <div class="row">
        <h1 class="page-header">Spare Part Catalog</h1>
    </div>
</div>
<!-- END page-header -->
<div class="row">
    <div class="col-md-4">
        <div class="panel panel-inverse">
            <div class="panel-heading">
                <h4 class="panel-title">Spare Part Form</h4>
                <div class="panel-heading-btn">
                </div>
            </div>
            <div class="panel-body">
                <div class="form-group row mb-5">
                    <span class="fs-11px">Maximum file size: <span>5 MB</span>.</span>
                    <div id="dxSparePartThumbnailFileUploader"></div>
                    <div class="col-md-12 text-center">
                        <img id="SparePartThumbnail" src="/App/Common/Assets/img/no-product-image.png" class="img-fluid" style="height: 200px;" />
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxSparePartNameTextBox"></div>
                        <div class="invalid-feedback" id="SparePartNameValidation"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-12">Manufacture ID (<span class="text-danger">*</span>)</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxSparePartManufactureIDTextBox"></div>
                        <div class="invalid-feedback" id="SparePartManufactureIDValidation"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxSparePartDescriptionTextArea"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                    <div class="col-xl-9 col-md-auto">
                        <div class="mt-2 mb-2">
                            <div type="text" id="dxSparePartIsActiveCheckBox"></div>
                        </div>
                    </div>
                </div>
                <div class="row" id="SparePartActionButtons"></div>
            </div>
        </div>
    </div>
    <div class="col-md-8">
        <div class="panel panel-inverse">
            <div class="panel-heading">
                <h4 class="panel-title">SparePart Information</h4>
                <div class="panel-heading-btn">
                    <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                </div>
            </div>
            <div class="panel-body">
                <div id="dxSparePartGrid"></div>
            </div>
        </div>

    </div>
</div>
<input type="hidden" id="hiddenSparePartID" hidden />
<script type="module" src="/App/Features/Ticket/SpareParts/SparePart/SparePartCatalog.js"></script>
</asp:Content>
