import { GetDXUnitOfMeasureDataSource, CreateUnitOfMeasure, UpdateUnitOfMeasure, DeleteUnitOfMeasure } from './UnitOfMeasure_Service.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'

//#region UnitOfMeasure Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeUnitOfMeasureCatalogControls();
});
async function InitializeUnitOfMeasureCatalogControls() {
    $("#dxUnitOfMeasureIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxUnitOfMeasureNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxUnitOfMeasureDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxUnitOfMeasureGrid").dxDataGrid({
        dataSource: await GetDXUnitOfMeasureDataSource_Global({ IsActive: true }),
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
            fileName: "UnitOfMeasureCatalog",
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
            let _unitOfMeasureData = data.selectedRowsData[0];
            if (_unitOfMeasureData != null) {
                UnitOfMeasureActionButtons("Update");
                PopulateUnitOfMeasureFields(_unitOfMeasureData);
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
                                    $('#SaveUnitOfMeasureRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenUnitOfMeasureID').value = options.data.ID;
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
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseUnitOfMeasureModal").addEventListener("click", ClearUnitOfMeasureFields);
    UnitOfMeasureActionButtons("Save");
}
async function PopulateUnitOfMeasureFields(data) {
    $("#hiddenUnitOfMeasureID").val(data.ID);
    $("#dxUnitOfMeasureNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxUnitOfMeasureDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxUnitOfMeasureIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the unit of measure, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteUnitOfMeasure_Global();
    } else {
        ClearUnitOfMeasureFields();
    }
}
function UnitOfMeasureActionButtons(Action) {
    $("#UnitOfMeasureActionButtons").empty();
    document.getElementById('UnitOfMeasureModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewUnitOfMeasureBtn").addEventListener("click", ClearUnitOfMeasureFields);
        document.getElementById('UnitOfMeasureModalTitle').innerText = 'Add Unit Of Measure'
        document.getElementById("UnitOfMeasureActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateUnitOfMeasureButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearUnitOfMeasureButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearUnitOfMeasureButton").addEventListener("click", ClearUnitOfMeasureFields);
        document.getElementById("CreateUnitOfMeasureButton").addEventListener("click", CreateUnitOfMeasure_Global);
    }
    else {
        // Update
        document.getElementById('UnitOfMeasureModalTitle').innerText = 'Update Unit Of Measure'
        document.getElementById("UnitOfMeasureActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateUnitOfMeasureButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearUnitOfMeasureButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearUnitOfMeasureButton").addEventListener("click", ClearUnitOfMeasureFields);
        document.getElementById("UpdateUnitOfMeasureButton").addEventListener("click", UpdateUnitOfMeasure_Global);
    }
}
function ClearUnitOfMeasureFields() {
    $('#SaveUnitOfMeasureRecordModal').modal('hide');
    UnitOfMeasureActionButtons("Save");
    $("#hiddenUnitOfMeasureID").val("");
    $("#dxUnitOfMeasureNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxUnitOfMeasureDescription").dxTextArea("instance").option("value", '');
    $("#dxUnitOfMeasureIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxUnitOfMeasureGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxUnitOfMeasureGrid").dxDataGrid("instance").deselectRows(keys);
    ClearErrorFeedback();
}
function GetUnitOfMeasureDTO() {
    let _unitOfMeasureDTO = {
        ID: $("#hiddenUnitOfMeasureID").val(),
        Name: $("#dxUnitOfMeasureNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxUnitOfMeasureDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxUnitOfMeasureIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _unitOfMeasureDTO;
}
//#endregion

//#region UnitOfMeasure CRUD Functions
async function CreateUnitOfMeasure_Global() {
    await dxLoadPanel.show();
    const _unitOfMeasureDTO = GetUnitOfMeasureDTO();
    const _validation_ResultDTO = await CreateUnitOfMeasure(_unitOfMeasureDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxUnitOfMeasureGrid").dxDataGrid("instance").refresh();
        ClearUnitOfMeasureFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateUnitOfMeasure_Global() {
    await dxLoadPanel.show();
    const _unitOfMeasureDTO = GetUnitOfMeasureDTO();
    const _validation_ResultDTO = await UpdateUnitOfMeasure(_unitOfMeasureDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxUnitOfMeasureGrid").dxDataGrid("instance").refresh();
        ClearUnitOfMeasureFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteUnitOfMeasure_Global() {
    await dxLoadPanel.show();
    const _unitOfMeasureDTO = GetUnitOfMeasureDTO();
    const _validation_ResultDTO = await DeleteUnitOfMeasure(_unitOfMeasureDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxUnitOfMeasureGrid").dxDataGrid("instance").refresh();
        ClearUnitOfMeasureFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearUnitOfMeasureFields();
    dxLoadPanel.hide();
}
const GetDXUnitOfMeasureDataSource_Global = () => {
    return GetDXUnitOfMeasureDataSource()
}
//#endregion