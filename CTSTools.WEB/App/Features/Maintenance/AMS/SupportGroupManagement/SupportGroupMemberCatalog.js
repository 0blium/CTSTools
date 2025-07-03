import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreateSupportGroupMember, UpdateSupportGroupMember, DeleteSupportGroupMember, GetDXSupportGroupMemberDataSource } from './SupportGroupMember/SupportGroupMember_Service.js'
import { GetDXUserDataSource, GetUserInformation } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
//import { GetDXRoleRelationDataSource } from '../../../AdvancedSettings/Security/Roles/RoleRelation/RoleRelation_Service.js'
//import { GetDXUser_RoleDataSource } from "../../../AdvancedSettings/UserManagement/User_Role/User_Role_Service.js"
import { GetDXRoleDataSource } from '../../../AdvancedSettings/SecurityManagement/Role/Role_Service.js'
import { GetDXSupportGroupDataSource } from './SupportGroup/SupportGroup_Service.js'

import { RoleType_Enum } from '../../../AdvancedSettings/SecurityManagement/RoleType/RoleType_Enum.js'
import { Role_Enum } from '../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'

document.addEventListener("DOMContentLoaded", async () => {
    InitializeSupportGroupMemberCatalogControls();
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
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
            $("#dxSupportGroupMemberGrid").dxDataGrid("instance").option("dataSource", await GetDXSupportGroupMemberDataSource());
        }
        if (UserDTO.SupportGroupIDArray != null && UserDTO.RoleIDArray.includes(Role_Enum.Administrator) && !UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            if (UserDTO.SupportGroupIDArray.length > 0) {
                $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxSupportGroupMemberGrid").dxDataGrid("instance").option("dataSource", await GetDXSupportGroupMemberDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetSupportGroupDTO: true }));
            }
        }
    }
}
async function InitializeSupportGroupMemberCatalogControls() {
    $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        searchExpr: ["EnglishName"],
        searchMode: 'contains'
    });
    $("#dxSupportGroupMemberUserSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxSupportGroupMemberRoleSelectBox").dxSelectBox({
        //dataSource: await GetDXUser_RoleDataSource({
        //    RoleTypeID: 1,
        //    /*...{ 'RoleTypeDTO.ID': RoleType_Enum.SupportGroup }*/
        //})
        dataSource: await GetDXRoleDataSource({ RoleTypeID: RoleType_Enum.SupportGroup }),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxSupportGroupMemberIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSupportGroupMemberGrid").dxDataGrid({
        dataSource: [],
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
        focusedRowEnabled: false,
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
            fileName: "SupportGroupMemberCatalog",
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
            let _supportGroupMemberData = data.selectedRowsData[0];
            if (_supportGroupMemberData != null) {
                SupportGroupMemberActionButtons("Update");
                PopulateSupportGroupMemberFields(_supportGroupMemberData);
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
                                    $('#SaveSupportGroupMemberRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenSupportGroupMemberID').value = options.data.ID;
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
                    caption: "Support Group",
                    dataField: "SupportGroupDTO.EnglishName",
                    //groupIndex: 0
                },
                {
                    caption: "User",
                    dataField: "UserDTO.Name"
                },
                {
                    caption: "Role",
                    dataField: "RoleDTO.Name"
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
    document.getElementById("btnCloseSupportGroupMemberModal").addEventListener("click", ClearSupportGroupMemberFields);
    SupportGroupMemberActionButtons("Save");
}

function SupportGroupMemberActionButtons(Action) {
    $("#SupportGroupMemberActionButtons").empty();
    document.getElementById('SupportGroupMemberModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewSupportGroupMemberBtn").addEventListener("click", ClearSupportGroupMemberFields);
        document.getElementById('SupportGroupMemberModalTitle').innerText = 'Add Member';
        document.getElementById("SupportGroupMemberActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateSupportGroupMemberButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSupportGroupMemberButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSupportGroupMemberButton").addEventListener("click", ClearSupportGroupMemberFields);
        document.getElementById("CreateSupportGroupMemberButton").addEventListener("click", CreateSupportGroupMember_Global);
    }
    else {
        // Update
        document.getElementById('SupportGroupMemberModalTitle').innerText = 'Update Member';
        document.getElementById("SupportGroupMemberActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSupportGroupMemberButton" type="button">Update</button>' +
            '<button class="btn btn-secondary float-end" id="ClearSupportGroupMemberButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSupportGroupMemberButton").addEventListener("click", ClearSupportGroupMemberFields);
        document.getElementById("UpdateSupportGroupMemberButton").addEventListener("click", UpdateSupportGroupMember_Global);
    }
}

function ClearSupportGroupMemberFields() {
    $('#SaveSupportGroupMemberRecordModal').modal('hide');
    SupportGroupMemberActionButtons("Save");
    $('#hiddenSupportGroupMemberID').val("");
    $("#dxSupportGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox("instance").option("value", '');
    $("#dxSupportGroupMemberUserSelectBox").dxSelectBox("instance").option("value", '');
    $("#dxSupportGroupMemberRoleSelectBox").dxSelectBox("instance").option("value", '');
    let keys = $("#dxSupportGroupMemberGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSupportGroupMemberGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSupportGroupMemberGrid").dxDataGrid("instance").option("selectedRowIndex", -1);
    $("#dxSupportGroupMemberGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateSupportGroupMemberFields(data) {
    $('#hiddenSupportGroupMemberID').val(data.ID);
    $("#dxSupportGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox("instance").option("value", data.SupportGroupDTO.ID);
    $("#dxSupportGroupMemberUserSelectBox").dxSelectBox("instance").option("value", data.UserDTO.ID);
    $("#dxSupportGroupMemberRoleSelectBox").dxSelectBox("instance").option("value", data.RoleDTO.ID);

}

function GetSupportGroupMemberDTO() {
    let _supportGroupMemberDTO = {
        ID: $('#hiddenSupportGroupMemberID').val(),
        SupportGroupDTO: {
            ID: $("#dxSupportGroupMemberSupportGroupSelectBox").dxSelectBox("instance").option("value")
        },
        UserDTO: {
            ID: $("#dxSupportGroupMemberUserSelectBox").dxSelectBox("instance").option("value")
        },
        RoleDTO: {
            ID: $("#dxSupportGroupMemberRoleSelectBox").dxSelectBox("instance").option("value")
        },
        IsActive: $("#dxSupportGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _supportGroupMemberDTO;
}

async function CreateSupportGroupMember_Global() {
    await dxLoadPanel.show();
    const _supportGroupMemberDTO = GetSupportGroupMemberDTO();
    const _validation_ResultDTO = await CreateSupportGroupMember(_supportGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupMemberFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function UpdateSupportGroupMember_Global() {
    await dxLoadPanel.show();
    const _supportGroupMemberDTO = GetSupportGroupMemberDTO();
    const _validation_ResultDTO = await UpdateSupportGroupMember(_supportGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupMemberFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function DeleteSupportGroupMember_Global() {
    await dxLoadPanel.show();
    const _supportGroupMemberDTO = GetSupportGroupMemberDTO();
    const _validation_ResultDTO = await DeleteSupportGroupMember(_supportGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearSupportGroupMemberFields();
        //ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this support group member',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSupportGroupMember_Global();
    } else {
        ClearSupportGroupMemberFields();
    }
}


//Reload SelectBox and TagBox for related fields
