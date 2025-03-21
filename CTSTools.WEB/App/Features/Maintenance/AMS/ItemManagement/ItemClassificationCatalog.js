import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXItemClassificationDataSource, CreateItemClassification, UpdateItemClassification, DeleteItemClassification } from './ItemClassification_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeItemClassificationCatalogControls();
});

async function InitializeItemClassificationCatalogControls() {
    $("#dxItemClassificationEnglishNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxItemClassificationSpanishNameTextBox").dxTextBox({
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
                    caption: "Delete",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 70,
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" class="btn btn-danger" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenItemClassificationID").val(options.data.ID);
                                ShowItemClassificationDeleteQuestion();
                            }).appendTo(container);
                    },
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
                    caption: "English Name",
                    dataField: "EnglishName"
                },
                {
                    caption: "Spanish Name",
                    dataField: "SpanishName"
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
    ItemClassificationActionButtons("Save");
}

function ItemClassificationActionButtons(Action) {
    $("#ItemClassificationActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("ItemClassificationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateItemClassificationButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateItemClassificationButton").addEventListener("click", CreateItemClassification_Global);
    }
    else {
        // Update
        document.getElementById("ItemClassificationActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearItemClassificationButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateItemClassificationButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearItemClassificationButton").addEventListener("click", ClearItemClassificationFields);
        document.getElementById("UpdateItemClassificationButton").addEventListener("click", UpdateItemClassification_Global);
    }
}
function ClearItemClassificationFields() {
    ItemClassificationActionButtons("Save");
    $('#hiddenItemClassificationID').val("");
    $("#dxItemClassificationIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxItemClassificationEnglishNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxItemClassificationSpanishNameTextBox").dxTextBox("instance").option("value", '');
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
    $("#dxItemClassificationSpanishNameTextBox").dxTextBox("instance").option("value", data.SpanishName);
    //$("#dxItemClassificationExchangeRateTextBox").dxTextBox("instance").option("value", data.ExchangeRate);
    $("#dxItemClassificationDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetItemClassificationDTO() {
    let _itemClassificationDTO = {
        ID: $('#hiddenItemClassificationID').val(),
        EnglishName: $("#dxItemClassificationEnglishNameTextBox").dxTextBox("instance").option("value"),
        SpanishName: $("#dxItemClassificationSpanishNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxItemClassificationDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxItemClassificationIsActiveCheckBox").dxCheckBox("instance").option("value"),
        //ExchangeRate: $("#dxItemClassificationExchangeRateTextBox").dxTextBox("instance").option("value")
    }
    return _itemClassificationDTO;
}
async function ShowItemClassificationDeleteQuestion() {
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