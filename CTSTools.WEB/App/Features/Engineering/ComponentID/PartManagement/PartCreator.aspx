<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="PartCreator.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.PartManagement.PartCreator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">ComponentID</a></li>
                <li class="breadcrumb-item active"></li>
            </ol>
        </div>
    </div>

    <div class="row">
        <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8  col-xs-1">
            <h1 class="page-header">Part Creator</h1>
        </div>
        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4  col-xs-1" style="text-align: right;">

            <%--<div class="col-md-12">
                <button class="btn btn-danger float-end" id="m,. " type="button"><i class="fas fa-trash"></i></button>
                <button class="btn btn-success me-1 float-end" id="UpdateAttributeButton" type="button"><i class="fas fa-paper-plane"></i></button>
            </div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <ul class="list-group list-group-flush">
                    <li class="list-group-item" style="background: #f6f6f6">
                        <h5 style="font-weight: 400">General Information</h5>
                    </li>
                    <li class="list-group-item">
                        <div class="row col-xl-12">
                            <div class="row col-lg-3 mb-15px">
                                <label class="form-label col-form-label col-xl-12 col-md-12">Part Type (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderPartTypeLookup"></div>
                                    <div class="invalid-feedback" id="DecoderPartTypeValidation"></div>
                                </div>
                            </div>
                            <div class="row col-lg-3 mb-15px" id="ComponentTypeGroup" hidden>
                                <label class="form-label col-form-label col-xl-12 col-md-12">Component Type (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderComponentTypeLookup"></div>
                                    <div class="invalid-feedback" id="DecoderComponentTypeValidation"></div>
                                </div>
                            </div>
                            <div class="row col-lg-3 mb-15px">
                                <label class="form-label col-form-label col-xl-12 col-md-12">Class (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderClassLookup"></div>
                                    <div class="invalid-feedback" id="DecoderClassValidation"></div>
                                </div>
                            </div>
                            <div class="row col-lg-3 mb-15px">
                                <label class="form-label col-form-label col-xl-12 col-md-12">Sub Class (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderSubClassLookup"></div>
                                    <div class="invalid-feedback" id="DecoderSubClassValidation"></div>
                                </div>
                            </div>

                        </div>
                    </li>
                    <li id="MfgGroup" class="list-group-item" style="background: #f6f6f6;" hidden>
                        <h5 style="font-weight: 400">Mfg / Vendor</h5>
                    </li>
                    <li id="MfgAttributesGroup" class="list-group-item" hidden>
                        <div class="row">
                            <div class="col-lg-3 mb-15px">
                                <label class="form-label col-form-label col-xl-12 col-md-12">Name (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderSubClass_SupplierLookup"></div>
                                    <div class="invalid-feedback" id="DecoderSubClass_SupplierValidation"></div>
                                </div>
                            </div>
                            <div class="col-lg-3 mb-15px">
                                <label class="form-label col-form-label col-xl-12 col-md-12">Identification Number (<span class="text-danger">*</span>)</label>
                                <div class="col-xl-12 col-md-12">
                                    <div id="dxDecoderManufactureNumberTextBox"></div>
                                    <div class="invalid-feedback" id="DecoderManufactureNumberValidation"></div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li id="CommentCardGroup" class="list-group-item" style="background: #f6f6f6" hidden>
                        <h5 style="font-weight: 400">Comments</h5>
                    </li>
                    <li id="CommentGroup" class="list-group-item" hidden>
                        <div class="row">
                            <div class="col-xs-12 col-md-12 col-lg-12">
                                <div id="dxDecoderCommentTextArea"></div>
                            </div>
                        </div>
                    </li>
                    <li id="AttributeCardGroup" class="list-group-item" style="background: #f6f6f6" hidden>
                        <h5 style="font-weight: 400">Attributes</h5>
                    </li>
                    <li id="AttributesGroup" class="list-group-item" hidden>
                        <div class="row">
                            <div class="col-xs-12 col-md-12 col-lg-12">
                                <div class="row" id="AttributeSection"></div>
                                <br />
                                <button class="btn btn-success m-b-15 float-end" id="CreateButton" type="button">Create</button>
                            </div>
                        </div>
                    </li>

                    <li id="PartInfoCardGroup" class="list-group-item" style="background: #f6f6f6" hidden>
                        <h5 style="font-weight: 400">Part Information</h5>
                    </li>
                    <li id="PartInfoGroup" class="list-group-item" hidden>
                        <h5>Description</h5>
                        <label id="PartDescription"></label>
                        <hr />
                        <h5>Request ID</h5>
                        <label id="RequestID"></label>
                    </li>
                </ul>
            </div>

        </div>
        <%--        <div class="col-md-6">
            <div class="card" style="height: 500px">
                <div class="card-header">
                    <b>Part Information</b>
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">
                        <h5>Description</h5>
                        <label>SWITCH fdsmkdskfmsfksmdksf dfskmfdksmfdskfm sdkfdsnmfkdsmk</label>
                        <h5>Number ID</h5>


                    </li>

                </ul>
            </div>
        </div>--%>
    </div>
    <input type="hidden" id="HiddenPartDecoderID" />
    <script type="module" src="/App/Features/Engineering/ComponentID/PartManagement/PartCreator.js"></script>

</asp:Content>
