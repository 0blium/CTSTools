<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="ValueLinker.aspx.cs" Inherits="CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.ValueLinker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- BEGIN breadcrumb -->
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
            <h1 class="page-header">Attribute Filtering Configurator</h1>
        </div>
        <!-- END page-header -->
    </div>
    <div class="col-md-12">
        <div class="panel panel-inverse">
            <div class="panel-body">
                <a class="btn btn-success mb-2" data-bs-toggle="modal" data-bs-target="#ValueLinkModal" id="StatusRelationButton">Add New</a>
                <div id="dxValueLinkGrid"></div>
            </div>
        </div>
    </div>
    <div class="row col-lg-12">
        <div class="col-lg-6">
            <div style="margin-top: 10px">
                <a href="javascript:;" data-bs-toggle="modal" data-bs-target="#MailGroupMemberModal">Attribute is not there? Create it here. </a>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ValueLinkModal" tabindex="-1" aria-labelledby="exampleModalLabel" data-bs-backdrop="static" data-bs-keyboard="false" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ValueLinkModalTitle"></h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="ValueLinkModalCloseButton"></button>
                </div>
                <div class="modal-body">
                    <h5>Parent</h5>

                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Attribute (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxParentAttributeLookup"></div>
                            <div class="invalid-feedback" id="ParentAttributeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Value (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxParentValueLookup"></div>
                            <div class="invalid-feedback" id="ParentValueValidation"></div>
                        </div>
                    </div>
                    <hr />
                    <h5>Filtered Values</h5>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Attribute (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxChildAttributeLookup"></div>
                            <div class="invalid-feedback" id="ChildAttributeValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="ChildValuesGroup">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Values (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxChildValueTagBox"></div>
                            <div class="invalid-feedback" id="ChildValueArrayValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px" id="ChildValueGroup">
                        <label class="form-label col-form-label col-xl-3 col-md-12">Value (<span class="text-danger">*</span>)</label>
                        <div class="col-xl-9 col-md-12">
                            <div id="dxChildValueLookup"></div>
                            <div class="invalid-feedback" id="ChildValueValidation"></div>
                        </div>
                    </div>
                    <div class="row mb-15px">
                        <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                        <div class="col-xl-9 col-md-auto">
                            <div class="mt-2 mb-2">
                                <div type="text" id="dxValueLinkIsActiveCheckBox"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row" id="ValueLinkActionButtons"></div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hiddenValueLinkID" hidden />
    <script type="module" src="/App/Features/Engineering/ComponentID/AttributeManagement/ValueLinker.js"></script>
</asp:Content>
