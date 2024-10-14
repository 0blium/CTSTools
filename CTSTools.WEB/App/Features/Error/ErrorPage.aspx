<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/Error/Error.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="CTSTools.WEB.App.Features.Error.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="notfound">
        <div class="notfound-404">
            <h1>401</h1>
        </div>
        <div>
            <h2 id="ErrorMessage"></h2>
            <p id="ErrorDescription"></p>
            <a class="btn btn-success" href="../Default.aspx">Back to Homepage</a>
        </div>
    </div>
    <script src="Error.js" type="module"></script>
</asp:Content>
