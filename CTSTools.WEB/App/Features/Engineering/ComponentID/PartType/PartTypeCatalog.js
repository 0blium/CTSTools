import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreatePartType, UpdatePartType, DeletePartType, GetDXPartTypeDataSource } from '../PartType/PartType_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializePartTypeCatalogControls();
});

async function InitializePartTypeCatalogControls() {
    $("#dxPartTypeNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxCodeTextBox").dxTextBox({
        placeholder: 'Type code..'
    });
    $("#dxPartTypeDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxPartTypeIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxPartTypeGrid").dxDataGrid({
        dataSource: await GetDXPartTypeDataSource(),
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
            fileName: "PartTypeCatalog",
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
            let _PartTypeData = data.selectedRowsData[0];
            if (_PartTypeData != null) {
                PartTypeActionButtons("Update");
                PopulatePartTypeFields(_PartTypeData);
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
                                    $('#SavePartTypeRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenPartTypeID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "ID",
                    dataField: "ID",
                    visible: false
                },
                {
                    caption: "Is Active",
                    dataField: "IsActive"
                },
                {
                    caption: "Name",
                    dataField: "Name"
                },
                {
                    caption: "Code",
                    dataField: "Code"
                },
                {
                    caption: "Attribute",
                    dataField: "AttributeName"
                },
                {
                    caption: "Value",
                    dataField: "ValueName"
                },
                {
                    caption: "Description",
                    dataField: "Description"
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
                    caption: "Last Update By ID",
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
                }
            ],
    });
    document.getElementById("btnClosePartTypeModal").addEventListener("click", ClearPartTypeFields);
    PartTypeActionButtons("Save");
}
function PartTypeActionButtons(Action) {
    $("#PartTypeActionButtons").empty();
    document.getElementById('PartTypeModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewPartTypeBtn").addEventListener("click", ClearPartTypeFields);
        document.getElementById('PartTypeModalTitle').innerText = 'Add Part Type';
        document.getElementById("PartTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreatePartTypeButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearPartTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearPartTypeButton").addEventListener("click", ClearPartTypeFields);
        document.getElementById("CreatePartTypeButton").addEventListener("click", CreatePartType_Global);
    }
    else {
        // Update
        document.getElementById('PartTypeModalTitle').innerText = 'Update Part Type';
        document.getElementById("PartTypeActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdatePartTypeButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearPartTypeButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearPartTypeButton").addEventListener("click", ClearPartTypeFields);
        document.getElementById("UpdatePartTypeButton").addEventListener("click", UpdatePartType_Global);
    }
}
function ClearPartTypeFields() {
    $('#SavePartTypeRecordModal').modal('hide');
    PartTypeActionButtons("Save");
    $('#hiddenPartTypeID').val("");
    $('#hiddenValueID').val("");
    $("#dxPartTypeIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxPartTypeNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCodeTextBox").dxTextBox("instance").option("value", '');
    $("#dxPartTypeDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxPartTypeGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxPartTypeGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxPartTypeGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxPartTypeGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulatePartTypeFields(data) {
    $('#hiddenPartTypeID').val(data.ID);
    $('#hiddenValueID').val(data.ValueID);
    $("#dxPartTypeIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxPartTypeNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCodeTextBox").dxTextBox("instance").option("value", data.Code);
    $("#dxPartTypeDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}

function GetPartTypeDTO() {
    let _PartTypeDTO = {
        ID: $('#hiddenPartTypeID').val(),
        ValueID: $('#hiddenValueID').val(),
        Name: $("#dxPartTypeNameTextBox").dxTextBox("instance").option("value"),
        Code: $("#dxCodeTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxPartTypeDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxPartTypeIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _PartTypeDTO;
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this part type',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeletePartType_Global();
    } else {
        ClearPartTypeFields();
    }
}

async function CreatePartType_Global() {
    await dxLoadPanel.show();
    const _PartTypeDTO = GetPartTypeDTO();
    const _validation_ResultDTO = await CreatePartType(_PartTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearPartTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdatePartType_Global() {
    await dxLoadPanel.show();
    const _PartTypeDTO = GetPartTypeDTO();
    const _validation_ResultDTO = await UpdatePartType(_PartTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearPartTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeletePartType_Global() {
    await dxLoadPanel.show();
    const _PartTypeDTO = GetPartTypeDTO();
    const _validation_ResultDTO = await DeletePartType(_PartTypeDTO);
    if (_validation_ResultDTO.Result) {
        ClearPartTypeFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}