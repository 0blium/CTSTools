<%@ Page Title="" Language="C#" MasterPageFile="~/App/Features/MasterPage/CTSTools.Master" AutoEventWireup="true" CodeBehind="PrintDashboard.aspx.cs" Inherits="CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.PrintDashboard" %>

<%@ Register Assembly="DevExpress.XtraReports.v23.2.Web.WebForms, Version=23.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
    $(document).ready(function () {
        $(document).on("keydown", disableF5);
    })
    function disableF5(e) { if ((e.which || e.keyCode) == 116 || (e.which || e.keyCode) == 82) e.preventDefault(); };
    window.onbeforeunload = function () { return false; }
    </script>
<div class="content">
    <div class="row">
        <form runat="server">
            <dx:ASPxDocumentViewer runat="server" ID="PrintDashboardFormat"></dx:ASPxDocumentViewer>
        </form>
    </div>
</div>
</asp:Content>
