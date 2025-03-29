import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXItemClassificationDataSource, CreateItemClassification, UpdateItemClassification, DeleteItemClassification } from './ItemClassification/ItemClassification_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeItemClassificationCatalogControls();
});

async function InitializeItemClassificationCatalogControls() {
    $("#dxItemClassificationEnglishNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    //$("#dxItemClassificationExchangeRateTextBox").dxTextBox({
    //    placeholder: 'Type exchange rate..',
    //});
    $("#dxItemClassificationDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxItemClassificationIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxItemClassificationGrid").dxDataGrid({
        dataSource: await GetDXItemClassificationDataSource(),
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
            fileName: "ItemClassificationCatalog",
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
            let _itemClassificationData = data.selectedRowsData[0];
            if (_itemClassificationData != null) {
                ItemClassificationActionButtons("Update");
                PopulateItemClassificationFields(_itemClassificationData);
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
                                    { text: "Edit item", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Delete item", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveItemClassificationRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenItemClassificationID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive"
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Name",
                    dataField: "EnglishName"
                },
                {
                    caption: "Added By I D",
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
    document.getElementById("btnCloseItemClassificationModal").addEventListener("click", ClearItemClassificationFields);
    ItemClassificationActionButtons("Save");
}

function ItemClassificationActionButtons(Action) {
    $("#ItemClassificationActionButtons").empty();
    document.getElementById('ItemClassificationModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewItemClassificationBtn").addEventListener("click", ClearItemClassificationFields);
        document.getElementById('ItemClassificationModalTitle').innerText = 'Add Item Classification';
        document.getElementById("ItemClassificationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateItemClassificationButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItemClassificationButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItemClassificationButton").addEventListener("click", ClearItemClassificationFields);
        document.getElementById("CreateItemClassificationButton").addEventListener("click", CreateItemClassification_Global);
    }
    else {
        // Update
        document.getElementById('ItemClassificationModalTitle').innerText = 'Update Item Classification';
        document.getElementById("ItemClassificationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateItemClassificationButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearItemClassificationButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearItemClassificationButton").addEventListener("click", ClearItemClassificationFields);
        document.getElementById("UpdateItemClassificationButton").addEventListener("click", UpdateItemClassification_Global);
    }
}
function ClearItemClassificationFields() {
    $('#SaveItemClassificationRecordModal').modal('hide');
    ItemClassificationActionButtons("Save");
    $('#hiddenItemClassificationID').val("");
    $("#dxItemClassificationIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxItemClassificationEnglishNameTextBox").dxTextBox("instance").option("value", '');
    //$("#dxItemClassificationExchangeRateTextBox").dxTextBox("instance").option("value", "");
    $("#dxItemClassificationDescriptionTextArea").dxTextArea("instance").option("value", '');
    let keys = $("#dxItemClassificationGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxItemClassificationGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxItemClassificationGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxItemClassificationGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateItemClassificationFields(data) {
    $('#hiddenItemClassificationID').val(data.ID);
    $("#dxItemClassificationIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxItemClassificationEnglishNameTextBox").dxTextBox("instance").option("value", data.EnglishName);
    //$("#dxItemClassificationExchangeRateTextBox").dxTextBox("instance").option("value", data.ExchangeRate);
    $("#dxItemClassificationDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetItemClassificationDTO() {
    let _itemClassificationDTO = {
        ID: $('#hiddenItemClassificationID').val(),
        EnglishName: $("#dxItemClassificationEnglishNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxItemClassificationDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxItemClassificationIsActiveCheckBox").dxCheckBox("instance").option("value"),
        //ExchangeRate: $("#dxItemClassificationExchangeRateTextBox").dxTextBox("instance").option("value")
    }
    return _itemClassificationDTO;
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Item classification',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteItemClassification_Global();
    } else {
        ClearItemClassificationFields();
    }
}
async function CreateItemClassification_Global() {
    await dxLoadPanel.show();
    const _itemClassificationDTO = GetItemClassificationDTO();
    const _validation_ResultDTO = await CreateItemClassification(_itemClassificationDTO);
    if (_validation_ResultDTO.Result) {
        ClearItemClassificationFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateItemClassification_Global() {
    await dxLoadPanel.show();
    const _itemClassificationDTO = GetItemClassificationDTO();
    const _validation_ResultDTO = await UpdateItemClassification(_itemClassificationDTO);
    if (_validation_ResultDTO.Result) {
        ClearItemClassificationFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteItemClassification_Global() {
    await dxLoadPanel.show();
    const _itemClassificationDTO = GetItemClassificationDTO();
    const _validation_ResultDTO = await DeleteItemClassification(_itemClassificationDTO);
    if (_validation_ResultDTO.Result) {
        ClearItemClassificationFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}