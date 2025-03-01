import { GetDXChangeLogDataSource } from '../ChangeLog/ChangeLog_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeChangeLogControls();
    EventHandler();
});
function EventHandler() {
    document.getElementById('UpdateChangelogbtn').addEventListener('click', function () {
        $("#dxChangelogDataGrid").dxDataGrid("instance").refresh();
    });
}
async function InitializeChangeLogControls() {
    $("#dxChangelogDataGrid").dxDataGrid({
        dataSource: await GetDXChangeLogDataSource({}),
        keyExpr: "ID",
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100],
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
            visible: false
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "ChangeLogHistory",
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
        }, headerFilter: {
            visible: true
        },
        columns:
            [
                { caption: "ID", dataField: "ID" },
                { caption: "RecordID", dataField: "RecordID" },
                { caption: "Table", dataField: "Table" },
                { caption: "Field", dataField: "Field" },
                { caption: "Old Value", dataField: "OldValue" },
                { caption: "New Value", dataField: "NewValue" },
                { caption: "User", dataField: "UserDTO.Name" },
                { caption: "User ID", dataField: "UserDTO.ID" },
                { caption: "Action", dataField: "Action" },
                { caption: "Change Group", dataField: "ChangeGroup" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },


            ],
    });
}