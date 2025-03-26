import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { GetDXCurrencyDataSource, CreateCurrency, UpdateCurrency } from './Currency_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeCurrencyCatalogControls();
});

async function InitializeCurrencyCatalogControls() {
    $("#dxCurrencyNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxCurrencyExchangeRateTextBox").dxTextBox({
        placeholder: 'Type exchange rate..',
    });
    $("#dxCurrencyDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxCurrencyIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxCurrencyGrid").dxDataGrid({
        dataSource: await GetDXCurrencyDataSource(),
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
            fileName: "DataTypeCatalog",
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
            let _currencyData = data.selectedRowsData[0];
            if (_currencyData != null) {
                CurrencyActionButtons("Update");
                PopulateCurrencyFields(_currencyData);
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
                                    //{ text: "Delete item", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveCurrencyRecordModal').modal('show');
                                }
                                //else if (e.itemData.value == 2) {
                                //    document.getElementById('hiddenCurrencyID').value = options.data.ID;
                                //    ShowDeleteQuestion();
                                //}
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
                    dataField: "Name"
                },
                {
                    caption: "Exchange Rate",
                    dataField: "ExchangeRate"
                },
                {
                    caption: "Description",
                    dataField: "Description"
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
    document.getElementById("btnCloseCurrencyModal").addEventListener("click", ClearCurrencyFields);
    CurrencyActionButtons("Save");
}

function CurrencyActionButtons(Action) {
    $("#CurrencyActionButtons").empty();
    document.getElementById('CurrencyModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewCurrencyBtn").addEventListener("click", ClearCurrencyFields);
        document.getElementById('CurrencyModalTitle').innerText = 'Add Currency';
        document.getElementById("CurrencyActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateCurrencyButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCurrencyButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCurrencyButton").addEventListener("click", ClearCurrencyFields);
        document.getElementById("CreateCurrencyButton").addEventListener("click", CreateCurrency_Global);
    }
    else {
        // Update
        document.getElementById('CurrencyModalTitle').innerText = 'Update Currency';
        document.getElementById("CurrencyActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateCurrencyButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCurrencyButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCurrencyButton").addEventListener("click", ClearCurrencyFields);
        document.getElementById("UpdateCurrencyButton").addEventListener("click", UpdateCurrency_Global);
    }
}
function ClearCurrencyFields() {
    $('#SaveCurrencyRecordModal').modal('hide');
    CurrencyActionButtons("Save");
    $('#hiddenCurrencyID').val("");
    $("#dxCurrencyIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxCurrencyNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCurrencyExchangeRateTextBox").dxTextBox("instance").option("value", "");
    $("#dxCurrencyDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxCurrencyGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxCurrencyGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxCurrencyGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxCurrencyGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateCurrencyFields(data) {
    $('#hiddenCurrencyID').val(data.ID);
    $("#dxCurrencyIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxCurrencyNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCurrencyExchangeRateTextBox").dxTextBox("instance").option("value", data.ExchangeRate);
    $("#dxCurrencyDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetCurrencyDTO() {
    let _currencyDTO = {
        ID: $('#hiddenCurrencyID').val(),
        Name: $("#dxCurrencyNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxCurrencyDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxCurrencyIsActiveCheckBox").dxCheckBox("instance").option("value"),
        ExchangeRate: $("#dxCurrencyExchangeRateTextBox").dxTextBox("instance").option("value")
    }
    return _currencyDTO;
}
async function CreateCurrency_Global() {
    await dxLoadPanel.show();
    const _currencyDTO = GetCurrencyDTO();
    const _validation_ResultDTO = await CreateCurrency(_currencyDTO);
    if (_validation_ResultDTO.Result) {
        ClearCurrencyFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateCurrency_Global() {
    await dxLoadPanel.show();
    const _currencyDTO = GetCurrencyDTO();
    const _validation_ResultDTO = await UpdateCurrency(_currencyDTO);
    if (_validation_ResultDTO.Result) {
        ClearCurrencyFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}