import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSupplyTypeDataSource, CreateSupplyType, UpdateSupplyType, DeleteSupplyType } from './SupplyType_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeSupplyTypeCatalogControls();
});

async function InitializeSupplyTypeCatalogControls() {
    $("#dxSupplyTypeNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxSupplyTypeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxSupplyTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSupplyTypeGrid").dxDataGrid({
        dataSource: await GetDXSupplyTypeDataSource(),
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
            fileName: "SupplyTypeCatalog",
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
            let _supplyTypeData = data.selectedRowsData[0];
            if (_supplyTypeData != null) {
                SupplyTypeActionButtons("Update");
                PopulateSupplyTypeFields(_supplyTypeData);
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
                                $("#hiddenSupplyTypeID").val(options.data.ID);
                                ShowSupplyTypeDeleteQuestion();
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
    SupplyTypeActionButtons("Save");
}
function SupplyTypeActionButtons(Action) {
    $("#SupplyTypeActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("SupplyTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSupplyTypeButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSupplyTypeButton").addEventListener("click", CreateSupplyType_Global);
    }
    else {
        // Update
        document.getElementById("SupplyTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearSupplyTypeButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSupplyTypeButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearSupplyTypeButton").addEventListener("click", ClearSupplyTypeFields);
        document.getElementById("UpdateSupplyTypeButton").addEventListener("click", UpdateSupplyType_Global);
    }
}
function ClearSupplyTypeFields() {
    SupplyTypeActionButtons("Save");
    $('#hiddenSupplyTypeID').val("");
    $("#dxSupplyTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSupplyTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxSupplyTypeDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxSupplyTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSupplyTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSupplyTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSupplyTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateSupplyTypeFields(data) {
    $('#hiddenSupplyTypeID').val(data.ID);
    $("#dxSupplyTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSupplyTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxSupplyTypeDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetSupplyTypeDTO() {
    let _supplyTypeDTO = {
        ID: $('#hiddenSupplyTypeID').val(),
        Name: $("#dxSupplyTypeNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxSupplyTypeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSupplyTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _supplyTypeDTO;
}
async function ShowSupplyTypeDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Supply Type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSupplyType_Global();
    } else {
        ClearSupplyTypeFields();
    }
}
async function CreateSupplyType_Global() {
    await dxLoadPanel.show();
    const _supplyTypeDTO = GetSupplyTypeDTO();
    const _validation_ResultDTO = await CreateSupplyType(_supplyTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupplyTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSupplyType_Global() {
    await dxLoadPanel.show();
    const _supplyTypeDTO = GetSupplyTypeDTO();
    const _validation_ResultDTO = await UpdateSupplyType(_supplyTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupplyTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSupplyType_Global() {
    await dxLoadPanel.show();
    const _supplyTypeDTO = GetSupplyTypeDTO();
    const _validation_ResultDTO = await DeleteSupplyType(_supplyTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupplyTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}