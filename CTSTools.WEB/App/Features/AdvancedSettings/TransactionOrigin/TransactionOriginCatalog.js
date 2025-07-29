import { GetDXTransactionOriginDataSource, CreateTransactionOrigin, UpdateTransactionOrigin, DeleteTransactionOrigin } from './TransactionOrigin_Service.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'

//#region TransactionOrigin Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeTransactionOriginCatalogControls();
});
async function InitializeTransactionOriginCatalogControls() {
    $("#dxTransactionOriginIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxTransactionOriginNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxTransactionOriginDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxTransactionOriginGrid").dxDataGrid({
        dataSource: await GetDXTransactionOriginDataSource_Global({ IsActive: true }),
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
            fileName: "TransactionOriginCatalog",
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
            let _TransactionOriginData = data.selectedRowsData[0];
            if (_TransactionOriginData != null) {
                TransactionOriginActionButtons("Update");
                PopulateTransactionOriginFields(_TransactionOriginData);
            }
        },
        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 1 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveTransactionOriginRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenTransactionOriginID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseTransactionOriginModal").addEventListener("click", ClearTransactionOriginFields);
    TransactionOriginActionButtons("Save");
}
async function PopulateTransactionOriginFields(data) {
    $("#hiddenTransactionOriginID").val(data.ID);
    $("#dxTransactionOriginNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxTransactionOriginDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxTransactionOriginIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the transaction origin, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteTransactionOrigin_Global();
    } else {
        ClearTransactionOriginFields();
    }
}
function TransactionOriginActionButtons(Action) {
    $("#TransactionOriginActionButtons").empty();
    document.getElementById('TransactionOriginModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewTransactionOriginBtn").addEventListener("click", ClearTransactionOriginFields);
        document.getElementById('TransactionOriginModalTitle').innerText = 'Add Transaction Origin'
        document.getElementById("TransactionOriginActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateTransactionOriginButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearTransactionOriginButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearTransactionOriginButton").addEventListener("click", ClearTransactionOriginFields);
        document.getElementById("CreateTransactionOriginButton").addEventListener("click", CreateTransactionOrigin_Global);
    }
    else {
        // Update
        document.getElementById('TransactionOriginModalTitle').innerText = 'Update Transaction Origin'
        document.getElementById("TransactionOriginActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateTransactionOriginButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearTransactionOriginButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearTransactionOriginButton").addEventListener("click", ClearTransactionOriginFields);
        document.getElementById("UpdateTransactionOriginButton").addEventListener("click", UpdateTransactionOrigin_Global);
    }
}
function ClearTransactionOriginFields() {
    $('#SaveTransactionOriginRecordModal').modal('hide');
    TransactionOriginActionButtons("Save");
    $("#hiddenTransactionOriginID").val("");
    $("#dxTransactionOriginNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxTransactionOriginDescription").dxTextArea("instance").option("value", '');
    $("#dxTransactionOriginIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxTransactionOriginGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxTransactionOriginGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetTransactionOriginDTO() {
    let _TransactionOriginDTO = {
        ID: $("#hiddenTransactionOriginID").val(),
        Name: $("#dxTransactionOriginNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxTransactionOriginDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxTransactionOriginIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _TransactionOriginDTO;
}
//#endregion

//#region TransactionOrigin CRUD Functions
async function CreateTransactionOrigin_Global() {
    await dxLoadPanel.show();
    const _TransactionOriginDTO = GetTransactionOriginDTO();
    const _validation_ResultDTO = await CreateTransactionOrigin(_TransactionOriginDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxTransactionOriginGrid").dxDataGrid("instance").refresh();
        ClearTransactionOriginFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateTransactionOrigin_Global() {
    await dxLoadPanel.show();
    const _TransactionOriginDTO = GetTransactionOriginDTO();
    const _validation_ResultDTO = await UpdateTransactionOrigin(_TransactionOriginDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxTransactionOriginGrid").dxDataGrid("instance").refresh();
        ClearTransactionOriginFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteTransactionOrigin_Global() {
    await dxLoadPanel.show();
    const _TransactionOriginDTO = GetTransactionOriginDTO();
    const _validation_ResultDTO = await DeleteTransactionOrigin(_TransactionOriginDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxTransactionOriginGrid").dxDataGrid("instance").refresh();
        ClearTransactionOriginFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearTransactionOriginFields();
    dxLoadPanel.hide();
}
const GetDXTransactionOriginDataSource_Global = () => {
    return GetDXTransactionOriginDataSource()
}
//#endregion