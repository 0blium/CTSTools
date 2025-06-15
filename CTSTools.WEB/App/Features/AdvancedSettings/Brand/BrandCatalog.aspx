<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="BrandCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.Brand.BrandCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                <li class="breadcrumb-item active">Brand</li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Brand</h1>
        </div>
    </div>
    <!-- END page-header -->
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="col-md-12 ">
                        <a id="NewBrandBtn" href="#SaveBrandRecordModal" class="btn btn-success mb-2" data-bs-toggle="modal"><i class="fa-solid fa-circle-plus"></i> Brand</a>
                    </div>
                    <div id="dxBrandGrid"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Modal save record -->
<div class="modal fade" id="SaveBrandRecordModal" tabindex="-1" data-bs-backdrop="static" aria-modal="true" role="dialog">
    <div class="modal-dialog ">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="BrandModalTitle" class="modal-title fs-5"></h4>
                <button type="button" id="btnCloseBrandModal" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
            </div>
            <div class="modal-body">
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxBrandNameTextBox"></div>
                        <div class="invalid-feedback" id="BrandNameValidation"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                    <div class="col-xl-9 col-md-12">
                        <div id="dxBrandDescriptionTextArea"></div>
                    </div>
                </div>
                <div class="row mb-15px">
                    <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                    <div class="col-xl-9 col-md-auto">
                        <div class="mt-2 mb-2">
                            <div type="text" id="dxBrandIsActiveCheckBox"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row" id="BrandActionButtons"></div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hiddenBrandID" hidden />
<script type="module" src="/App/Features/AdvancedSettings/Brand/BrandCatalog.js"></script>
</asp:Content>
