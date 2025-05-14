import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { GetDXSupportGroupDataSource, CreateSupportGroup, UpdateSupportGroup, DeleteSupportGroup } from './SupportGroup/SupportGroup_Service.js'
import { GetDXFacilityDataSource } from '../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXStationDataSource } from '../StationManagement/Station/Station_Service.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeSupportGroupCatalogControls();
});

async function InitializeSupportGroupCatalogControls() {
    $("#dxSupportGroupEnglishNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxSupportGroupFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxSupportGroupDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxSupportGroupIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSupportGroupGrid").dxDataGrid({
        dataSource: await GetDXSupportGroupDataSource(),
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
            fileName: "SupportGroupCatalog",
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
            let _supportGroupData = data.selectedRowsData[0];
            if (_supportGroupData != null) {
                SupportGroupActionButtons("Update");
                PopulateSupportGroupFields(_supportGroupData);
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
                                    $('#SaveSupportGroupRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenSupportGroupID').value = options.data.ID;
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
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Facility",
                    dataField: "FacilityDTO.Name"
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
    document.getElementById("btnCloseSupportGroupModal").addEventListener("click", ClearSupportGroupFields);
    SupportGroupActionButtons("Save");
}

function SupportGroupActionButtons(Action) {
    $("#SupportGroupActionButtons").empty();
    document.getElementById('SupportGroupModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewSupportGroupBtn").addEventListener("click", ClearSupportGroupFields);
        document.getElementById('SupportGroupModalTitle').innerText = 'Add Support Group';
        document.getElementById("SupportGroupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateSupportGroupButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSupportGroupButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSupportGroupButton").addEventListener("click", ClearSupportGroupFields);
        document.getElementById("CreateSupportGroupButton").addEventListener("click", CreateSupportGroup_Global);
    }
    else {
        // Update
        document.getElementById('SupportGroupModalTitle').innerText = 'Update Support Group';
        document.getElementById("SupportGroupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateSupportGroupButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSupportGroupButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSupportGroupButton").addEventListener("click", ClearSupportGroupFields);
        document.getElementById("UpdateSupportGroupButton").addEventListener("click", UpdateSupportGroup_Global);
    }
}
function ClearSupportGroupFields() {
    $('#SaveSupportGroupRecordModal').modal('hide');
    SupportGroupActionButtons("Save");
    $('#hiddenSupportGroupID').val("");
    $("#dxSupportGroupIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSupportGroupEnglishNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxSupportGroupDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxSupportGroupFacilitySelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxSupportGroupGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSupportGroupGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSupportGroupGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSupportGroupGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

async function PopulateSupportGroupFields(data) {
    $('#hiddenSupportGroupID').val(data.ID);
    $("#dxSupportGroupIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSupportGroupEnglishNameTextBox").dxTextBox("instance").option("value", data.EnglishName);
    $("#dxSupportGroupDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    await $("#dxSupportGroupFacilitySelectBox").dxSelectBox("instance").option("value", data.FacilityDTO.ID);
}
function GetSupportGroupDTO() {
    let _supportGroupDTO = {
        ID: $('#hiddenSupportGroupID').val(),
        EnglishName: $("#dxSupportGroupEnglishNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxSupportGroupDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSupportGroupIsActiveCheckBox").dxCheckBox("instance").option("value"),
        FacilityDTO: {
            ID: $("#dxSupportGroupFacilitySelectBox").dxSelectBox("instance").option("value")
        },
    }
    return _supportGroupDTO;
}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Support Group',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSupportGroup_Global();
    } else {
        ClearSupportGroupFields();
    }
}
async function CreateSupportGroup_Global() {
    await dxLoadPanel.show();
    const _supportGroupDTO = GetSupportGroupDTO();
    const _validation_ResultDTO = await CreateSupportGroup(_supportGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSupportGroup_Global() {
    await dxLoadPanel.show();
    const _supportGroupDTO = GetSupportGroupDTO();
    const _validation_ResultDTO = await UpdateSupportGroup(_supportGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSupportGroup_Global() {
    await dxLoadPanel.show();
    const _supportGroupDTO = GetSupportGroupDTO();
    const _validation_ResultDTO = await DeleteSupportGroup(_supportGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}