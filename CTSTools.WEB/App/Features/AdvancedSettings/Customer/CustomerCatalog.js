import { GetDXCustomerDataSource, CreateCustomer, UpdateCustomer, DeleteCustomer } from '../Customer/Customer_Service.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js';
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js';

//#region Customer Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeCustomerCatalogControls();
});
async function InitializeCustomerCatalogControls() {
    $("#dxCustomerIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxCustomerNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxCustomerDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxCustomerGrid").dxDataGrid({
        dataSource: await GetDXCustomerDataSource_Global({ IsActive: true }),
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
            fileName: "CustomerCatalog",
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
            let _CustomerData = data.selectedRowsData[0];
            if (_CustomerData != null) {
                CustomerActionButtons("Update");
                PopulateCustomerFields(_CustomerData);
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
                                    $('#SaveCustomerRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenCustomerID').value = options.data.ID;
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
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseCustomerModal").addEventListener("click", ClearCustomerFields);
    CustomerActionButtons("Save");
}
async function PopulateCustomerFields(data) {
    $("#hiddenCustomerID").val(data.ID);
    $("#dxCustomerNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCustomerDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxCustomerIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the Customer, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteCustomer_Global();
    } else {
        ClearCustomerFields();
    }
}
function CustomerActionButtons(Action) {
    $("#CustomerActionButtons").empty();
    document.getElementById('CustomerModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewCustomerBtn").addEventListener("click", ClearCustomerFields);
        document.getElementById('CustomerModalTitle').innerText = 'Add Customer'
        document.getElementById("CustomerActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateCustomerButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCustomerButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCustomerButton").addEventListener("click", ClearCustomerFields);
        document.getElementById("CreateCustomerButton").addEventListener("click", CreateCustomer_Global);
    }
    else {
        // Update
        document.getElementById('CustomerModalTitle').innerText = 'Update Customer'
        document.getElementById("CustomerActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateCustomerButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCustomerButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCustomerButton").addEventListener("click", ClearCustomerFields);
        document.getElementById("UpdateCustomerButton").addEventListener("click", UpdateCustomer_Global);
    }
}
function ClearCustomerFields() {
    $('#SaveCustomerRecordModal').modal('hide');
    CustomerActionButtons("Save");
    $("#hiddenCustomerID").val("");
    $("#dxCustomerNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCustomerDescription").dxTextArea("instance").option("value", '');
    $("#dxCustomerIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxCustomerGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxCustomerGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetCustomerDTO() {
    let _CustomerDTO = {
        ID: $("#hiddenCustomerID").val(),
        Name: $("#dxCustomerNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxCustomerDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxCustomerIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _CustomerDTO;
}
//#endregion

//#region Customer CRUD Functions
async function CreateCustomer_Global() {
    await dxLoadPanel.show();
    const _CustomerDTO = GetCustomerDTO();
    const _validation_ResultDTO = await CreateCustomer(_CustomerDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCustomerGrid").dxDataGrid("instance").refresh();
        ClearCustomerFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateCustomer_Global() {
    await dxLoadPanel.show();
    const _CustomerDTO = GetCustomerDTO();
    const _validation_ResultDTO = await UpdateCustomer(_CustomerDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCustomerGrid").dxDataGrid("instance").refresh();
        ClearCustomerFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteCustomer_Global() {
    await dxLoadPanel.show();
    const _CustomerDTO = GetCustomerDTO();
    const _validation_ResultDTO = await DeleteCustomer(_CustomerDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCustomerGrid").dxDataGrid("instance").refresh();
        ClearCustomerFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearCustomerFields();
    dxLoadPanel.hide();
}
const GetDXCustomerDataSource_Global = () => {
    return GetDXCustomerDataSource()
}
//#endregion