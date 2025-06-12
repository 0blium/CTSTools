import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreateUser, UpdateUser, DeleteUser, GetDXUserDataSource } from './User/User_Service.js'
import { GetDXFacilityDataSource } from '../LocationManagement/Facility/Facility_Service.js'
import { GetDXDepartmentDataSource } from '../LocationManagement/Department/Department_Service.js'
import { GetDXRoleDataSource } from '../SecurityManagement/Role/Role_Service.js'
import { GetDXPermissionDataSource } from '../SecurityManagement/Permission/Permission_Service.js'
import { GetUser_PermissionInformation, CreateUser_Permission, DeleteUser_Permission } from './User_Permission/User_Permission_Service.js'
import { GetDXMailGroupDataSource } from '../MailGroupManagement/MailGroup/MailGroup_Service.js'
import { GetDXMailGroupMemberDataSource, GetMailGroupMemberInformation, CreateMailGroupMemberByGroups,DeleteMailGroupMember } from '../MailGroupManagement/MailGroupMember/MailGroupMember_Service.js'

import { GetDXUser_RoleDataSource, GetUser_RoleInformation, CreateUser_Role, DeleteUser_Role } from './User_Role/User_Role_Service.js'
import { RoleType_Enum } from '../../AdvancedSettings/SecurityManagement/RoleType/RoleType_Enum.js'



document.addEventListener("DOMContentLoaded", async function () {
    await InitializeUserCatalogControls();
    InitializeUser_PermissionControls();
    InitializeUser_RoleControls();
    InitializeMailGroupControls();
});
//#region User Catalog
async function InitializeUserCatalogControls() {
    
    document.getElementById("UserButton").addEventListener("click", function (e) {
        e.preventDefault();
        UserActionButtons("Save");

    });


    $("#dxUserLoginTextBox").dxTextBox({
        value: '',
    });

    $("#dxUserIsActiveCheckBox").dxCheckBox({
        value: true,
        text: " Active?"
    });
    $("#dxUserSendWelcomeEmailCheckBox").dxCheckBox({
        value: true,
        text: " Send Welcome email?"
    });
    $("#dxUserFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _facilityList = await GetFacilityDXDatasource_Global();
                $("#dxUserDepartmentSelectBox").dxSelectBox("instance").reset();
                $("#dxUserDepartmentSelectBox").dxSelectBox("instance").option("dataSource", _facilityList);
            }
            //else {
            //    $("#dxUserDepartmentSelectBox").dxSelectBox({
            //        dataSource: []
            //    });
            //}
        }
    });
    $("#dxUserDepartmentSelectBox").dxSelectBox({
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
    });
    $("#dxUserRoleTagBox").dxTagBox({
        dataSource: await GetDXRoleDataSource(),
        displayExpr: "Name",
        deferRendering: false,
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'useButtons',
        popupWidth: 450,
    });


    $("#dxUserNameTextBox").dxTextBox({
        placeholder: "Type name.."
    });
    $("#dxUserEmailTextBox").dxTextBox({
        placeholder: "Type email.."
    });
    $("#dxUserPositionTextBox").dxTextBox({
        placeholder: "Type position.."
    });
    $("#dxUserGrid").dxDataGrid({
        dataSource: await getUserDataGridDataSource_Global(),
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
            fileName: "UserCatalog",
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
            let _userData = data.selectedRowsData[0];
            if (_userData != null) {
                UserActionButtons("Update");
                PopulateUserFields(_userData);
            }
        },

        columns:
            [
                {
                    caption: "Update",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#UserModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenValueID").val(options.data.ID);

                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Login", dataField: "Login" },
                { caption: "Email", dataField: "Email" },
                { caption: "Position", dataField: "Position" },
                { caption: "Facility", dataField: "FacilityName" },
                { caption: "Department", dataField: "DepartmentName" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: "datetime" }
            ],
        masterDetail: {
            enabled: true,
            template: masterDetailTemplate,
        }
    });
}
function masterDetailTemplate(_, masterDetailOptions) {
    return $('<div>').dxTabPanel({
        items: [{
            title: 'Permissions',
            template: PermissionTabTemplate(masterDetailOptions.data),
        },
        {
            title: 'Roles',
            template: RoleTabTemplate(masterDetailOptions.data),
        },
        {
            title: 'Email Groups',
            template: MailGroupMemberTabTemplate(masterDetailOptions.data),
        }
        ],
        onSelectionChanged: function () {
            setTimeout(function () {
                masterDetailOptions.component.updateDimensions();
            }, 0); 
        }
    });
}
function ClearUserFields() {
    UserActionButtons("Save");
    $('#hiddenUserID').val("");
    $("#dxUserLoginTextBox").dxTextBox("instance").option("value", '\\');
    $("#dxUserNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxUserEmailTextBox").dxTextBox("instance").option("value", "");
    $("#dxUserPositionTextBox").dxTextBox("instance").option("value", "");
    $("#dxUserFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxUserDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxUserRoleTagBox").dxTagBox("instance").reset();
    $("#dxUserSendWelcomeEmailCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxUserIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxUserGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxUserGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxUserGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxUserGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function UserActionButtons(Action) {
    $("#UserActionButtons").empty();
    document.getElementById("UserModalCloseButtton").addEventListener("click", ClearUserFields);
    if (Action == "Save") {
        document.getElementById("UserActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateUserButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateUserButton").addEventListener("click", CreateUser_Global);
    }
    else {
        // Update
        document.getElementById("UserActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateUserButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateUserButton").addEventListener("click", UpdateUser_Global);
    }
}
async function PopulateUserFields(UserDTO) {
    $('#hiddenUserID').val(UserDTO.ID);
    $("#dxUserLoginTextBox").dxTextBox("instance").option("value", UserDTO.Login);
    $("#dxUserNameTextBox").dxTextBox("instance").option("value", UserDTO.Name);
    $("#dxUserEmailTextBox").dxTextBox("instance").option("value", UserDTO.Email);
    $("#dxUserPositionTextBox").dxTextBox("instance").option("value", UserDTO.Position);
    $("#dxUserFacilitySelectBox").dxSelectBox("instance").option("value", UserDTO.FacilityID);
    $("#dxUserDepartmentSelectBox").dxSelectBox("instance").option("value", UserDTO.DepartmentID);
    $("#dxUserRoleTagBox").dxTagBox("instance").option("value", UserDTO.RoleIDArray);
    $("#dxUserSendWelcomeEmailCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxUserIsActiveCheckBox").dxCheckBox("instance").option("value", UserDTO.IsActive);
}
function GetUserDTO() {
    let _userDTO = {
        ID: $('#hiddenUserID').val(),
        Name: $("#dxUserNameTextBox").dxTextBox("instance").option("value"),
        Email: $("#dxUserEmailTextBox").dxTextBox("instance").option("value"),
        Position: $("#dxUserPositionTextBox").dxTextBox("instance").option("value"),
        Login: $("#dxUserLoginTextBox").dxTextBox("instance").option("value"),
        RoleIDArray: $("#dxUserRoleTagBox").dxTagBox("instance").option("value"),
        FacilityID: $("#dxUserFacilitySelectBox").dxSelectBox("instance").option("value"),
        DepartmentID: $("#dxUserDepartmentSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxUserIsActiveCheckBox").dxCheckBox("instance").option("value"),
        SendWelcomeEmail: $("#dxUserSendWelcomeEmailCheckBox").dxCheckBox("instance").option("value"),
    }
    return _userDTO;
}
async function CreateUser_Global() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _validation_ResultDTO = await CreateUser(_userDTO);
    if (_validation_ResultDTO.Result) {
        ClearUserFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateUser_Global() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _validation_ResultDTO = await UpdateUser(_userDTO);
    if (_validation_ResultDTO.Result) {
        ClearUserFields();
        ReloadRelated();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function GetFacilityDXDatasource_Global() {
    let _departmentDTO = {
        FacilityID: $("#dxUserFacilitySelectBox").dxSelectBox("instance").option("value"),
        IsActive: true

    };
    return await GetDXDepartmentDataSource(_departmentDTO, "")
}

//#endregion

//#region User Permissions
async function InitializeUser_PermissionControls() {
    $("#dxPermissionDataGrid").dxDataGrid({
        dataSource: await GetDXPermissionDataSource(),
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
        grouping: {
            autoExpandAll: true,
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        "export": {
            enabled: true,
            fileName: "Permission Catalog",
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
            mode: 'multiple',
            selectAllMode: 'page',
            showCheckBoxesMode: 'always'
        },
        columns:
            [
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Module", dataField: "ModuleName", sortIndex: 0, sortOrder: "asc" },
                { caption: "Description", dataField: "Description" },
                { caption: "Action", dataField: "ActionName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    User_PermissionActionButtons("Save");
}
function PermissionTabTemplate(masterDetailData) {

    return function () {
        let _user_permissionDataGrid;
        async function onDataGridInitialized(e) {
            _user_permissionDataGrid = e.component;
            let _user_permissionDTO = await GetUser_PermissionInformation({ UserID: masterDetailData.ID, GetPermissionDTO: true });
            _user_permissionDataGrid.option('dataSource', _user_permissionDTO);

        }
        return $('<div>').addClass('form-container').dxForm({
            labelLocation: 'top',
            items: [
                {
                    template: PermissionButton(masterDetailData.ID)


                }
                , {
                    template: PermissionGridTemplate(onDataGridInitialized, masterDetailData.ID),
                }],
        });
    };
}
function PermissionGridTemplate(onDataGridInitialized, UserID) {

    return function () {
        return $(`<div id="dxUser_PermissionGrid${UserID}">`).dxDataGrid({
            onInitialized: onDataGridInitialized,
            keyExpr: "ID",
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
                fileName: "Permissions",
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
            columns: [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 80,
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 1 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $("#hiddenUserID").val(options.data.UserID);
                                    $("#hiddenUser_PermissionID").val(options.data.ID);
                                    ShowUser_PermissionDeleteQuestion();
                                }
                                else if (e.itemData.value == 2) {

                                }
                            },
                        });
                    },
                },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Permission", dataField: "PermissionName" },
                { caption: "Module", dataField: "PermissionDTO.ModuleName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },

            ]
        });
    };
}
function PermissionButton(UserID) {
    return $(`<br><a  class="btn btn-success mt-2" data-bs-toggle="modal" data-bs-target="#AddNewPermissionUserModal" data-userid="${UserID}" id="User_PermissionButton${UserID}"><i class="fa-solid fa-circle-plus"></i> Permission</a>`).on("click", $("#hiddenUserID").val(UserID));

}
function ClearUser_PermissionFields(UserID) {
    User_PermissionActionButtons("Save");
    $('#hiddenUser_PermissionID').val("");
    
    ClearErrorFeedback();
}
function User_PermissionActionButtons(Action) {

    document.getElementById("User_PermissionModalCloseButton").addEventListener("click", ClearUser_PermissionFields);
    $("#UserActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("User_PermissionActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateUser_PermissionButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateUser_PermissionButton").addEventListener("click", CreateUser_Permission_Global);

    }
}
function GetUser_PermissionDTO() {
    let _user_PermissionDTO = {
        ID: $("#hiddenUser_PermissionID").val(),
        UserID: $("#hiddenUserID").val(),
        PermissionIDArray: ($("#dxPermissionDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.ID),
    }
    return _user_PermissionDTO;
}
async function CreateUser_Permission_Global() {
    await dxLoadPanel.show();
    const _user_PermissionDTO = GetUser_PermissionDTO();
    const _validation_ResultDTO = await CreateUser_Permission(_user_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxUser_PermissionGrid${_user_PermissionDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_PermissionDTO.UserID, GetPermissionDTO: true }));
        ClearUser_PermissionFields(_user_PermissionDTO.UserID);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteUser_Permission_Global() {
    await dxLoadPanel.show();
    const _user_PermissionDTO = GetUser_PermissionDTO();
    const _validation_ResultDTO = await DeleteUser_Permission(_user_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxUser_PermissionGrid${_user_PermissionDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_PermissionDTO.UserID, GetPermissionDTO: true }));
        ClearUser_PermissionFields(_user_PermissionDTO.UserID);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function ShowUser_PermissionDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this permission',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteUser_Permission_Global();
    } else {
        ClearUser_PermissionFields();
    }
}

//#endregion

//#region User Roles
async function InitializeUser_RoleControls() {
    $("#dxUser_RoleRoleSelectBox").dxSelectBox({
        dataSource: await GetDXRoleDataSource({ RoleTypeID: RoleType_Enum.System }),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    User_RoleActionButtons("Save");
}
function RoleTabTemplate(masterDetailData) {
    return function () {
        let _user_roleDataGrid;
        async function onDataGridInitialized(e) {
            _user_roleDataGrid = e.component;
            let _user_roleDTO = await GetDXUser_RoleDataSource({ UserID: masterDetailData.ID })
            _user_roleDataGrid.option('dataSource', _user_roleDTO);

        }
        return $('<div>').addClass('form-container').dxForm({
            labelLocation: 'top',
            items: [
                {
                    template: RoleButton(masterDetailData.ID),
                }
                , {
                    template: RoleGridTemplate(onDataGridInitialized, masterDetailData.ID),
                }],
        });
    };
}
function RoleGridTemplate(onDataGridInitialized, UserID) {
    return function () {
        return $(`<div id="dxUser_RoleGrid${UserID}">`).dxDataGrid({
            onInitialized: onDataGridInitialized,
            keyExpr: "ID",
            paging: {
                pageSize: 10,
            },
            showBorders: true,
            columns: [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 80,
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 1 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $("#hiddenUserID").val(options.data.UserID);
                                    $("#hiddenUser_RoleID").val(options.data.ID);
                                    ShowUser_RoleDeleteQuestion();
                                }
                                else if (e.itemData.value == 2) {
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Role", dataField: "RoleName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: "datetime" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: "datetime" },
            ]
        });
    };
}
function RoleButton(UserID) {
    return $(`<br><a class="btn btn-success mt-2" data-bs-toggle="modal" data-bs-target="#AddNewRoleUserModal" id="User_RoleButton" data-userid="${UserID}" id="User_RoleButton${UserID}"><i class="fa-solid fa-circle-plus"></i> Role</a>`).on("click", $("#hiddenUserID").val(UserID));
}

function ClearUser_RoleFields(UserID) {
    User_RoleActionButtons("Save");
    $('#hiddenUser_RoleID').val("");
    $("#dxUser_RoleRoleSelectBox").dxSelectBox("instance").reset();
    ClearErrorFeedback();
}
function User_RoleActionButtons(Action) {
    $("#UserActionButtons").empty();
    document.getElementById("User_RoleModalCloseButton").addEventListener("click", ClearUser_RoleFields);
    if (Action == "Save") {
        document.getElementById("User_RoleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateUser_RoleButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateUser_RoleButton").addEventListener("click", CreateUser_Role_Global);

    }
    else {
    }
}
async function PopulateUser_RoleFields(data) {
    $("#hiddenUser_RoleID").val(data.ID);
    $("#dxUser_RoleUserSelectBox").dxSelectBox("instance").option("value", data.UserID);
    $("#dxUser_RoleRoleSelectBox").dxSelectBox("instance").option("value", data.RoleID);
}
function GetUser_RoleDTO() {
    let _user_RoleDTO = {
        ID: $("#hiddenUser_RoleID").val(),
        UserID: $("#hiddenUserID").val(),
        RoleID: $("#dxUser_RoleRoleSelectBox").dxSelectBox("instance").option("value"),
        IsActive: true
    }
    return _user_RoleDTO;
}
async function CreateUser_Role_Global() {
    await dxLoadPanel.show();
    const _user_RoleDTO = GetUser_RoleDTO();
    const _validation_ResultDTO = await CreateUser_Role(_user_RoleDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxUser_RoleGrid${_user_RoleDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetDXUser_RoleDataSource({ UserID: _user_RoleDTO.UserID }));
        $(`#dxUser_PermissionGrid${_user_RoleDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_RoleDTO.UserID, GetPermissionDTO: true }));

        ClearUser_RoleFields(_user_RoleDTO.UserID);
        $("#AddNewRoleUserModal").modal('hide');
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteUser_Role_Global() {
    await dxLoadPanel.show();
    const _user_RoleDTO = GetUser_RoleDTO();
    const _validation_ResultDTO = await DeleteUser_Role(_user_RoleDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxUser_RoleGrid${_user_RoleDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetDXUser_RoleDataSource({ UserID: _user_RoleDTO.UserID }));
        $(`#dxUser_PermissionGrid${_user_RoleDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_RoleDTO.UserID, GetPermissionDTO: true }));

        ClearUser_RoleFields(_user_RoleDTO.UserID);
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function ShowUser_RoleDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this role',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteUser_Role_Global();
    } else {
        ClearUser_RoleFields($("#hiddenUserID").val());
    }
}
//#endregion

//#region User Global functions

async function getUserDataGridDataSource_Global() {
    let _userDTO = {
        GetRoleArray: true
    };
    return await GetDXUserDataSource(_userDTO);
}
//#endregion


//#region Mail Group 
async function InitializeMailGroupControls() {
    
    $("#dxUserMailGroupTagBox").dxTagBox({
        dataSource: await GetDXMailGroupDataSource({IsActive:true}),
        displayExpr: "Name",
        deferRendering: false,
        valueExpr: "ID",
        searchEnabled: true,
        showSelectionControls: true,
        applyValueMode: 'useButtons',
        popupWidth: 450,
    });
    MailGroupMemberActionButtons("Save");
}
function MailGroupMemberTabTemplate(masterDetailData) {

    return function () {
        let _mailgroupmemberDataGrid;
        async function onDataGridInitialized(e) {
            _mailgroupmemberDataGrid = e.component;
            let _mailgroupmemberDTO = await GetDXMailGroupMemberDataSource({ UserID: masterDetailData.ID });
            _mailgroupmemberDataGrid.option('dataSource', _mailgroupmemberDTO);
        }
        return $('<div>').addClass('form-container').dxForm({
            labelLocation: 'top',
            items: [
                {
                    template: MailGroupButton(masterDetailData.ID)


                }
                , {
                    template: MailGroupMemberGridTemplate(onDataGridInitialized, masterDetailData.ID),
                }],
        });
    };
}
function MailGroupMemberGridTemplate(onDataGridInitialized, UserID) {

    return function () {
        return $(`<div id="dxMailGroupMemberGrid${UserID}">`).dxDataGrid({
            onInitialized: onDataGridInitialized,
            keyExpr: "ID",
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
                fileName: "Mail Group Member",
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
            columns: [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 80,
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 1 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $("#hiddenUserID").val(options.data.UserID);
                                    $("#hiddenMailGroupMemberID").val(options.data.ID);
                                    ShowMailGroupMemberDeleteQuestion();
                                }
                                else if (e.itemData.value == 2) {
                                    
                                }
                            },
                        });
                    },                   
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Mail Group", dataField: "MailGroupName" },
                { caption: "User", dataField: "UserName" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },

            ]
        });
    };
}
function MailGroupButton(UserID) {
    return $(`<br><a  class="btn btn-success mt-2" data-bs-toggle="modal" data-bs-target="#AddNewMailGroupMemberUserModal" data-userid="${UserID}" id="User_PermissionButton${UserID}"><i class="fa-solid fa-circle-plus"></i> Mail Group</a>`).on("click", $("#hiddenUserID").val(UserID));

}
function ClearMailGroupFields() {
    MailGroupMemberActionButtons("Save");
    $('#hiddenMailGroupMemberID').val("");
    $("#dxUserMailGroupTagBox").dxTagBox("instance").reset();

    ClearErrorFeedback();
}
function MailGroupMemberActionButtons(Action) {

    document.getElementById("UserMailGroupModalCloseButton").addEventListener("click", ClearMailGroupFields);
    $("#UserMailGroupActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("UserMailGroupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateMailGroupMemberButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateMailGroupMemberButton").addEventListener("click", CreateMailGroupMember_Global);

    }
}
function GetMailGroupMemberDTO() {
    let _mailGroupMemberDTO = {
        ID: $("#hiddenMailGroupMemberID").val(),
        UserID: $("#hiddenUserID").val(),
        MailGroupIDArray: $("#dxUserMailGroupTagBox").dxTagBox("instance").option("value")
,
    }
    return _mailGroupMemberDTO;
}
async function CreateMailGroupMember_Global() {
    await dxLoadPanel.show();
    const _mailGroupMemberDTO = GetMailGroupMemberDTO();
    const _validation_ResultDTO = await CreateMailGroupMemberByGroups(_mailGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxMailGroupMemberGrid${_mailGroupMemberDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetDXMailGroupMemberDataSource({ UserID: _mailGroupMemberDTO.UserID }));
        ClearMailGroupFields();

        $("#AddNewMailGroupMemberUserModal").modal('hide');
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteMailGroupMember_Global() {
    await dxLoadPanel.show();
    const _mailGroupMemberDTO = GetMailGroupMemberDTO();
    const _validation_ResultDTO = await DeleteMailGroupMember(_mailGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxMailGroupMemberGrid${_mailGroupMemberDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetDXMailGroupMemberDataSource({ UserID: _mailGroupMemberDTO.UserID }));
        ClearMailGroupFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function ShowMailGroupMemberDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this permission',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteMailGroupMember_Global();
    } else {
        ClearMailGroupFields();
    }
}
//#endregion

