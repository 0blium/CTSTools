import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../../Common/Utils/Response.js'
import { GetDXPriorityDataSource, CreatePriority, UpdatePriority, DeletePriority } from './Priority_Service.js'
import { GetDXSupportGroupDataSource } from '../../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetUserInformation } from '../../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { Role_Enum } from '../../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializePriorityCatalogControls();
    await GetUserInformationbyID();
});
async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    await FilterDataSourceBySupportGroups(_userInformation[0])
    dxLoadPanel.hide();
}
function GetUserDTO() {
    let _userDTO = {
        ID: document.getElementById('hiddenUserID').value,
        GetSupportGroupArray: true,
        GetRoleArray: true
    }
    return _userDTO;
}

async function FilterDataSourceBySupportGroups(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.SupportGroupIDArray != null && !UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            if (UserDTO.SupportGroupIDArray.length > 0) {
                $("#dxPrioritySupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxPriorityGrid").dxDataGrid("instance").option("dataSource", await GetDXPriorityDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetSupportGroupDTO: true }));

            }

        }
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxPrioritySupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
            $("#dxPriorityGrid").dxDataGrid("instance").option("dataSource", await GetDXPriorityDataSource());
        }
    }
}

async function InitializePriorityCatalogControls() {
    $("#dxPriorityNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxPriorityDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxPriorityIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxPrioritySupportGroupSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
        searchMode: 'contains'
    });
    $("#dxPriorityGrid").dxDataGrid({
        dataSource: [],
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
            fileName: "PriorityCatalog",
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
            let _PriorityData = data.selectedRowsData[0];
            if (_PriorityData != null) {
                PriorityActionButtons("Update");
                PopulatePriorityFields(_PriorityData);
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
                                    $('#SavePriorityRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenPriorityID').value = options.data.ID;
                                    ShowPriorityDeleteQuestion();
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
                    caption: "Support Group",
                    dataField: "SupportGroupDTO.EnglishName",
                    //groupIndex: 0
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
    document.getElementById("btnClosePriorityModal").addEventListener("click", ClearSupportGroupFields);

    PriorityActionButtons("Save");
}
function PriorityActionButtons(Action) {
    $("#PriorityActionButtons").empty();
    document.getElementById('PriorityModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewPriorityBtn").addEventListener("click", ClearPriorityFields);
        document.getElementById('PriorityModalTitle').innerText = 'Add Priority';
        document.getElementById("PriorityActionButtons").innerHTML =
            '<div class="col-md-12">' +
        '<button class="btn btn-success m-b-15 float-end" id="CreatePriorityButton" type="button">Save</button>' +
        '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearPriorityButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("CreatePriorityButton").addEventListener("click", CreatePriority_Global);
        document.getElementById("ClearPriorityButton").addEventListener("click", ClearPriorityFields);
    }
    else {
        // Update
        document.getElementById('PriorityModalTitle').innerText = 'Update Priority';
        document.getElementById("PriorityActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearPriorityButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdatePriorityButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearPriorityButton").addEventListener("click", ClearPriorityFields);
        document.getElementById("UpdatePriorityButton").addEventListener("click", UpdatePriority_Global);
    }
}
function ClearPriorityFields() {

    $('#SavePriorityRecordModal').modal('hide');
    PriorityActionButtons("Save");
    $('#hiddenPriorityID').val("");
    $("#dxPriorityIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxPriorityNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxPriorityDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxPrioritySupportGroupSelectBox").dxSelectBox("instance").option("value", '');

    let keys = $("#dxPriorityGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxPriorityGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxPriorityGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxPriorityGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulatePriorityFields(data) {
    $('#hiddenPriorityID').val(data.ID);
    $("#dxPriorityIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxPriorityNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxPriorityDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxPrioritySupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTO.ID);
}
function GetPriorityDTO() {
    let _PriorityDTO = {
        ID: $('#hiddenPriorityID').val(),
        Name: $("#dxPriorityNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxPriorityDescriptionTextArea").dxTextArea("instance").option("value"),
        SupportGroupDTO: {
            ID: $("#dxPrioritySupportGroupSelectBox").dxSelectBox("instance").option("value")
        },
        IsActive: $("#dxPriorityIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _PriorityDTO;
}
async function ShowPriorityDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Priority',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeletePriority_Global();
    } else {
        ClearPriorityFields();
    }
}
async function CreatePriority_Global() {
    await dxLoadPanel.show();
    const _PriorityDTO = GetPriorityDTO();
    const _validation_ResultDTO = await CreatePriority(_PriorityDTO);
    if (_validation_ResultDTO.Result) {
        ClearPriorityFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdatePriority_Global() {
    await dxLoadPanel.show();
    const _PriorityDTO = GetPriorityDTO();
    const _validation_ResultDTO = await UpdatePriority(_PriorityDTO);
    if (_validation_ResultDTO.Result) {
        ClearPriorityFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeletePriority_Global() {
    await dxLoadPanel.show();
    const _PriorityDTO = GetPriorityDTO();
    const _validation_ResultDTO = await DeletePriority(_PriorityDTO);
    if (_validation_ResultDTO.Result) {
        ClearPriorityFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}