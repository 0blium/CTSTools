import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXProviderDataSource, CreateProvider, UpdateProvider, DeleteProvider } from './Provider/Provider_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeProviderCatalogControls();
});

async function InitializeProviderCatalogControls() {
    $("#dxProviderNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxProviderDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxProviderIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxProviderGrid").dxDataGrid({
        dataSource: await GetDXProviderDataSource(),
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
            fileName: "ProviderCatalog",
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
            let _ProviderData = data.selectedRowsData[0];
            if (_ProviderData != null) {
                ProviderActionButtons("Update");
                PopulateProviderFields(_ProviderData);
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
                                $("#hiddenProviderID").val(options.data.ID);
                                ShowProviderDeleteQuestion();
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
                    caption: "Name",
                    dataField: "Name"
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
    ProviderActionButtons("Save");
}
function ProviderActionButtons(Action) {
    $("#ProviderActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("ProviderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateProviderButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateProviderButton").addEventListener("click", CreateProvider_Global);
    }
    else {
        // Update
        document.getElementById("ProviderActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearProviderButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateProviderButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearProviderButton").addEventListener("click", ClearProviderFields);
        document.getElementById("UpdateProviderButton").addEventListener("click", UpdateProvider_Global);
    }
}
function ClearProviderFields() {
    ProviderActionButtons("Save");
    $('#hiddenProviderID').val("");
    $("#dxProviderIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxProviderNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxProviderDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxProviderGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxProviderGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxProviderGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxProviderGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateProviderFields(data) {
    $('#hiddenProviderID').val(data.ID);
    $("#dxProviderIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxProviderNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxProviderDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetProviderDTO() {
    let _ProviderDTO = {
        ID: $('#hiddenProviderID').val(),
        Name: $("#dxProviderNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxProviderDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxProviderIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _ProviderDTO;
}
async function ShowProviderDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Provider',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteProvider_Global();
    } else {
        ClearProviderFields();
    }
}
async function CreateProvider_Global() {
    await dxLoadPanel.show();
    const _ProviderDTO = GetProviderDTO();
    const _validation_ResultDTO = await CreateProvider(_ProviderDTO);
    if (_validation_ResultDTO.Result) {
        ClearProviderFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateProvider_Global() {
    await dxLoadPanel.show();
    const _ProviderDTO = GetProviderDTO();
    const _validation_ResultDTO = await UpdateProvider(_ProviderDTO);
    if (_validation_ResultDTO.Result) {
        ClearProviderFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteProvider_Global() {
    await dxLoadPanel.show();
    const _ProviderDTO = GetProviderDTO();
    const _validation_ResultDTO = await DeleteProvider(_ProviderDTO);
    if (_validation_ResultDTO.Result) {
        ClearProviderFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}