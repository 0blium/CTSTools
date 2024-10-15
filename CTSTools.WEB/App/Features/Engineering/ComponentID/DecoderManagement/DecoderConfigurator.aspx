<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="DecoderConfigurator.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.DecoderManagement.DecoderConfigurator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <ol class="breadcrumb float-xl-start">
                <li class="breadcrumb-item"><a href="javascript:;">Engineering</a></li>
                <li class="breadcrumb-item"><a href="javascript:;">ComponentID</a></li>
                <li class="breadcrumb-item"><a href="/App/Features/Engineering/ComponentID/DecoderCatalog.aspx">Decoder Catalog</a></li>
                <li class="breadcrumb-item active"></li>
            </ol>
        </div>
        <!-- END breadcrumb -->
        <!-- BEGIN page-header -->

        <!-- END page-header -->
    </div>

    <div class="row">
        <div class="col-xl-8 col-lg-8 col-md-8 col-sm-8  col-xs-1">
            <h1 class="page-header">Decoder Configurator</h1>
        </div>
        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4  col-xs-1" style="text-align: right;">

            <div class="col-md-12">
                <button class="btn btn-primary me-1 float-end" id="SubmitDecoderButton" type="button" hidden><i class="fas fa-paper-plane"></i>&nbsp Submit </button>
                <button class="btn btn-danger me-1 float-end" id="DeleteDecoderButton" type="button" hidden><i class="fas fa-trash"></i>&nbsp Delete </button>
                <button class="btn btn-success me-1 mb-2 float-end" id="EditDecoderButton" type="button" hidden><i class="fas fa-paper-plane"></i>&nbsp Edit</button>
            </div>
        </div>
    </div>

    <div class="accordion" id="accordionPanelsStayOpenExample">
        <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingOne">
                <button class="bg-white accordion-button text-dark" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                    <h5 style="font-weight: 400;">General Information</h5>
                </button>
            </h2>
            <div id="panelsStayOpen-collapseOne" class="  accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                <div class="accordion-body">
                    <div class="row col-xl-12">
                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Sub Class:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-9  col-md-12" id="DecoderSubClassName"></label>
                        </div>
                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Class:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-9  col-md-12" id="DecoderClassName"></label>
                        </div>
                        <div class="col-lg-2" id="ComponentTypeGroup" hidden>
                            <label class="form-label col-form-label col-xl-5 col-md-12">Component Type:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-9  col-md-12" id="DecoderComponentTypeName"></label>
                        </div>
                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Part Type:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-9  col-md-12" id="DecoderPartTypeName"></label>
                        </div>


                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-12 col-md-12 mb-2">Status:</label>
                            <span style="font-weight: normal !important; font-size: 14px" class="badge text-bg-primary form-label col-form-label col-xl-9 mt-2  col-md-12" id="DecoderStatusName"></span>
                            <div id="StatusGroup"></div>
                        </div>



                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Created By:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-7  col-md-12" id="DecoderAddedBy"></label>
                        </div>

                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Created Date:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-7 col-md-12" id="DecoderAddedDate"></label>
                        </div>
                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Last Update By:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-7  col-md-12" id="DecoderLastUpdateBy"></label>

                        </div>
                        <div class="col-lg-2">
                            <label class="form-label col-form-label col-xl-5 col-md-12">Last Date:</label>
                            <label style="font-weight: normal !important" class="form-label col-form-label col-xl-7  col-md-12" id="DecoderLastUpdate"></label>
                        </div>



                    </div>

                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                <button class="accordion-button bg-white text-dark" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="true" aria-controls="panelsStayOpen-collapseTwo">
                    <h5 style="font-weight: 400;">Number Structure</h5>
                </button>
            </h2>
            <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingTwo">
                <div class="accordion-body">
                    <div class="row">
                        <div class="col-md-12 col-lg-12">
                            <div class="panel-body">
                                <%--                                <a class="btn btn-success mb-2" id="AddNumberAttributeButton"><i class="fa-solid fa-circle-plus"></i>Attribute</a>--%>

                                <div type="text" id="dxDecoderStructureNumberGrid"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                <button class="accordion-button  bg-white text-dark" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
                    <h5 style="font-weight: 400;">Description Structure</h5>
                </button>
            </h2>
            <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingThree">
                <div class="accordion-body">
                    <div class="row">
                        <div class="col-xs-12 col-md-12 col-lg-12">
                            <a class="btn btn-success mb-2" id="AddDescriptionAttributeButton"><i class="fa-solid fa-circle-plus"></i>&nbsp Attribute</a>

                            <div id="dxDecoderStructureDescriptionGrid"></div>
                            <div class="row" id="AttributeSection"></div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header" id="Panel4">
                <button class="accordion-button  bg-white text-dark" type="button" data-bs-toggle="collapse" data-bs-target="#panelcollapse4" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
                    <h5 style="font-weight: 400;">Manufacturers</h5>
                </button>
            </h2>
            <div id="panelcollapse4" class="accordion-collapse collapse show" aria-labelledby="Panel4">
                <div class="accordion-body">
                    <div class="row">
                        <div class="col-xs-12 col-md-12 col-lg-12">
                            <a class="btn btn-success mb-2" id="AddSubClass_SupplierButton" data-bs-toggle="modal" data-bs-target="#SubClass_SupplierModal"><i class="fa-solid fa-circle-plus"></i>&nbsp Manufacturer</a>
                            <div id="dxSubClass_SupplierGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="DecoderStructureModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Decoder Form</h1>
                    <button id="DecoderStructureCloseModalButton" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-xl-3 col-md-12">Attribute (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderStructureAttributeLookup"></div>
                            <div class="invalid-feedback" id="DecoderStructureAttributeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="DecoderStructureValueGroup">
                        <label class="form-label col-xl-3 col-md-12">Value (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderStructureValueLookup"></div>
                            <div class="invalid-feedback" id="DecoderStructureValueValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="DecoderStructureOptionsGroup">
                        <label class="form-label col-xl-3 col-md-12">Options (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxDecoderStructureOptionsTagBox"></div>
                            <div class="invalid-feedback" id="DecoderStructureOptionsValidation"></div>
                        </div>
                    </div>
                    <input type="hidden" id="DecoderConfiguratorNumberOrder" />
                    <div class="row mb-15px" hidden>
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is in Number?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxNumberBodyCheckBox"></div>
                            </div>
                        </div>
                    </div>
                    <input type="hidden" id="DecoderConfiguratorDescriptionOrder" />
                    <div class="row mb-15px" hidden>
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is in Description?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxDescriptionBodyCheckBox"></div>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <div class="row" id="DecoderStructureActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="SubClass_SupplierModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Supplier Form</h1>
                    <button id="SubClass_SupplierCloseModalButton" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-15px">
                        <label class="form-label col-xl-3 col-md-12">Manufacturer (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxSupplierLookup"></div>
                            <div class="invalid-feedback" id="SupplierValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxSubClass_SupplierIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row" id="SubClass_SupplierActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenDecoderID" />
    <input type="hidden" id="hiddenSubClass_SupplierID" />
    <input type="hidden" id="hiddenDecoderStructureID" />
    <input type="hidden" id="hiddenDecoderSubClassID" />
    <script type="module" src="/App/Features/Engineering/ComponentID/DecoderManagement/DecoderConfigurator.js"></script>

</asp:Content>
