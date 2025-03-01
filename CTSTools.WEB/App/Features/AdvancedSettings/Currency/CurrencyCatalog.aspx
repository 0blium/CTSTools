<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="CurrencyCatalog.aspx.cs" Inherits="CTSTools.WEB.App.Features.AdvancedSettings.Currency.CurrencyCatalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
     <div class="container-fluid">
         <div class="row">
             <ol class="breadcrumb float-xl-start">
                 <li class="breadcrumb-item"><a href="javascript:;">Home</a></li>
                 <li class="breadcrumb-item"><a href="javascript:;">Advanced Settings</a></li>
                 <li class="breadcrumb-item active">Currency Catalog</li>
             </ol>
         </div>
         <!-- END breadcrumb -->
         <!-- BEGIN page-header -->
         <div class="row">
             <h1 class="page-header">Currency Catalog</h1>
         </div>
     </div>
     <!-- END page-header -->
     <div class="row">
         <div class="col-md-4">
             <div class="panel panel-inverse">
                 <div class="panel-heading">
                     <h4 class="panel-title">Currency Form</h4>
                     <div class="panel-heading-btn">
                     </div>
                 </div>
                 <div class="panel-body">
                     <div class="row mb-15px">
                         <label class="form-label col-form-label col-xl-3 col-md-12">Name (<span class="text-danger">*</span>)</label>
                         <div class="col-xl-9 col-md-12">
                             <div id="dxCurrencyNameTextBox"></div>
                             <div class="invalid-feedback" id="CurrencyNameValidation"></div>
                         </div>
                     </div>
                     <div class="row mb-15px">
                         <label class="form-label col-form-label col-xl-3 col-md-12">Description</label>
                         <div class="col-xl-9 col-md-12">
                             <div id="dxCurrencyDescriptionTextArea"></div>
                         </div>
                     </div>
                     <div class="row mb-15px">
                         <label class="form-label col-form-label col-xl-3 col-md-12">Exchange Rate (<span class="text-danger">*</span>)</label>
                         <div class="col-xl-9 col-md-12">
                             <div id="dxCurrencyExchangeRateTextBox"></div>
                             <div class="invalid-feedback" id="CurrencyExchangeRateValidation"></div>
                         </div>
                     </div>
                     <div class="row mb-15px">
                         <label class="form-label col-form-label col-xl-3 col-md-auto">Is Active?</label>
                         <div class="col-xl-9 col-md-auto">
                             <div class="mt-2 mb-2">
                                 <div type="text" id="dxCurrencyIsActiveCheckBox"></div>
                             </div>
                         </div>
                     </div>
                     <div class="row" id="CurrencyActionButtons"></div>
                 </div>
             </div>
         </div>
         <div class="col-md-8">
             <div class="panel panel-inverse">
                 <div class="panel-heading">
                     <h4 class="panel-title">Currency Information</h4>
                     <div class="panel-heading-btn">
                         <a href="javascript:;" class="btn btn-xs btn-icon btn-default" data-toggle="panel-expand"><i class="fa fa-expand"></i></a>

                     </div>
                 </div>
                 <div class="panel-body">
                     <div id="dxCurrencyGrid"></div>
                 </div>
             </div>

         </div>
     </div>
 </div>
 <input type="hidden" id="hiddenCurrencyID" hidden />
 <script type="module" src="/App/Features/AdvancedSettings/Currency/CurrencyCatalog.js"></script>
</asp:Content>
