<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DecoderCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.DecoderManagement.DecoderCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">ComponentID</a></li>
                <li class="breadcrumb-item active"></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->
        <div class="row">
            <h1 class="page-header">Decoders</h1>
        </div>
        <!-- END page-header -->
    </div>



    <div class="row">
        <div class="col-xl-12">
            <div class="panel panel-inverse">
                <div class="panel-body">
                    <div class="row ">
                        <div class="col-12">
                            <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#DecoderModal"><i class="fa-solid fa-circle-plus"></i>Decoder</a>

                            <div id="dxDecoderGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="DecoderModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Decoder Form</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="DecoderModalCloseButton"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Part Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderPartTypeLookup"></div>
                            <div class="invalid-feedback" id="DecoderPartTypeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="ComponentTypeGroup" hidden>
                        <label class="form-label col-form-label col-xl-3 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderComponentTypeLookup"></div>
                            <div class="invalid-feedback" id="DecoderComponentTypeValidation"></div>

                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Class (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderClassLookup"></div>
                            <div class="invalid-feedback" id="DecoderClassValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Sub Class (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderSubClassLookup"></div>
                            <div class="invalid-feedback" id="DecoderSubClassValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderDescriptionTextArea"></div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="DecoderActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDecoderID" hidden />
    <script type="module" src="/App/Features/Engineering/ComponentID/DecoderManagement/DecoderCatalog.js"></script>

</asp:Content>
