import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { CreatePart, UpdatePart, DeletePart, GetDXPartDataSource } from '../PartManagement/Part/Part_Service.js'




document.addEventListener("DOMContentLoaded", async () => {
    InitializePartListControls();

});
async function InitializePartListControls() {

    $("#dxPartGrid").dxDataGrid({
        dataSource: await GetDXPartDataSource(),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: 100,
        showRowLines: true,
        showColumnLines: false,
        showBorders: true,
        focusedRowEnabled: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: false,
        columnAutoWidth: true,
        groupPanel: {
            visible: true
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "PartList",
            allowExportSelectedData: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: true,
            placeholder: "Search...",
            width: 300
        },
        sorting: {
            mode: "multiple"
        },
        selection: {
            mode: 'single'
        },
        headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
        },
        columns:
            [
                
                {
                    caption: "Request ID",
                    dataField: "ID",
                },
                {
                    caption: "Number",
                    dataField: "Number"
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Comment",
                    dataField: "Comment",
                },
                {
                    caption: "Mfg Part #",
                    dataField: "MfgPartNumber",
                },
                {
                    caption: "Supplier",
                    dataField: "SupplierName",
                },
                {
                    caption: "Added By ID",
                    dataField: "AddedByID",
                    visible: false
                },
                {
                    caption: "Added By",
                    dataField: "AddedByName"
                },
                {
                    caption: "Added Date",
                    dataField: "AddedDate",
                    dataType: 'datetime'
                },
                {
                    caption: "Last Update By I D",
                    dataField: "LastUpdateByID",
                    visible: false
                },
                {
                    caption: "Last Update By",
                    dataField: "LastUpdateByName"
                },
                {
                    caption: "Last Update",
                    dataField: "LastUpdate",
                    dataType: 'datetime'
                },


            ],
    });
}
