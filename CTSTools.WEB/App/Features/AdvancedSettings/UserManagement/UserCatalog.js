import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreateUser, UpdateUser, DeleteUser, GetDXUserDataSource } from './User/User_Service.js'
import { GetDXFacilityDataSource } from '../LocationManagement/Facility/Facility_Service.js'
import { GetDXDepartmentDataSource } from '../LocationManagement/Department/Department_Service.js'
import { GetDXRoleDataSource } from '../SecurityManagement/Role/Role_Service.js'
import { GetDXPermissionDataSource } from '../SecurityManagement/Permission/Permission_Service.js'
import { GetUser_PermissionInformation, CreateUser_Permission, DeleteUser_Permission } from './User_Permission/User_Permission_Service.js'
import { GetUser_RoleInformation, CreateUser_Role, DeleteUser_Role } from './User_Role/User_Role_Service.js'



document.addEventListener("DOMContentLoaded", () => {
    InitializeUserCatalogControls();
    InitializeUser_PermissionControls();
    InitializeUser_RoleControls();
});
//#region User Catalog
async function InitializeUserCatalogControls() {
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
    UserActionButtons("Save");
}
function masterDetailTemplate(_, masterDetailOptions) {
    return $('<div>').dxTabPanel({
        items: [{
            title: 'Permissions',
            template: PermissionTabTemplate(masterDetailOptions.data),
        }, {
            title: 'Roles',
            template: RoleTabTemplate(masterDetailOptions.data),
        }],
    });
}

function PermissionTabTemplate(masterDetailData) {

    return function () {
        //document.getElementById("User_PermissionButton").addEventListener("click", ClearUser_PermissionFields);
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
                                $("#hiddenUserID").val(options.data.UserID);
                                $("#hiddenUser_PermissionID").val(options.data.ID);
                                ShowUser_PermissionDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Permission", dataField: "PermissionName" },
                { caption: "Module", dataField: "PermissionDTO.Module" },
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

function RoleTabTemplate(masterDetailData) {

    return function () {
        //document.getElementById("User_RoleButton").addEventListener("click", ClearUser_RoleFields);
        let _user_roleDataGrid;
        async function onDataGridInitialized(e, ID) {
            _user_roleDataGrid = e.component;
            let _user_roleDTO = await GetUser_RoleInformation({ UserID: masterDetailData.ID });
            _user_roleDataGrid.option('dataSource', _user_roleDTO);

        }
        return $('<div>').addClass('form-container').dxForm({
            labelLocation: 'top',
            items: [
                {
                    template: RoleButton(),
                }
                , {
                    template: RoleGridTemplate(onDataGridInitialized),
                }],
        });
    };
}
function RoleGridTemplate(onDataGridInitialized) {

    return function () {
        return $('<div>').dxDataGrid({
            onInitialized: onDataGridInitialized,
            paging: {
                pageSize: 10,
            },
            showBorders: true,
            columns: [
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
                                $("#hiddenUser_PermissionID").val(options.data.ID);
                                ShowUser_PermissionDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Permission", dataField: "PermissionName" },
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
function RoleButton() {
    return $('<br><a class="btn btn-success mt-2" data-bs-toggle="modal" data-bs-target="#AddNewRoleUserModal" id="User_RoleButton"><i class="fa-solid fa-circle-plus"></i> Role</a>');
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
                { caption: "Module", dataField: "Module", groupIndex: 0 },
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
function ClearUser_PermissionFields() {
    User_PermissionActionButtons("Save");
    $('#hiddenUser_PermissionID').val("");
    let dxUser_PermissionPermissionKeys = $("#dxPermissionDataGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxPermissionDataGrid").dxDataGrid("instance").deselectRows(dxUser_PermissionPermissionKeys);
    $("#dxPermissionDataGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxPermissionDataGrid").dxDataGrid("instance").refresh();

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
        $(`#dxUser_PermissionGrid${_user_PermissionDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_PermissionDTO.UserID }));
        ClearUser_PermissionFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteUser_Permission_Global() {
    await dxLoadPanel.show();
    const _user_PermissionDTO = GetUser_PermissionDTO();
    const _validation_ResultDTO = await DeleteUser_Permission(_user_PermissionDTO);
    if (_validation_ResultDTO.Result) {
        $(`#dxUser_PermissionGrid${_user_PermissionDTO.UserID}`).dxDataGrid("instance").option("dataSource", await GetUser_PermissionInformation({ UserID: _user_PermissionDTO.UserID }));
        ClearUser_PermissionFields();
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
        dataSource: await GetDXRoleDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    User_RoleActionButtons("Save");
}
function ClearUser_RoleFields() {
    User_RoleActionButtons("Save");
    $('#hiddenUser_RoleID').val("");
    $("#dxUser_RoleRoleSelectBox").dxSelectBox("instance").reset();
    let keys = $("#dxUser_RoleGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxUser_RoleGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxUser_RoleGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxUser_RoleGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function User_RoleActionButtons(Action) {
    $("#UserActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("User_RoleActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateUser_RoleButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateUser_RoleButton").addEventListener("click", CreateUser_Role_Global);

    }
    else {
        // Update
        document.getElementById("ClearUser_RoleButton").addEventListener("click", ClearUserFields);
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
        UserID: $("#dxUser_RoleUserSelectBox").dxSelectBox("instance").option("value"),
        RoleID: $("#dxUser_RoleRoleSelectBox").dxSelectBox("instance").option("value"),
    }
    return _user_RoleDTO;
}
async function CreateUser_Role_Global() {
    await dxLoadPanel.show();
    const _user_RoleDTO = GetUser_RoleDTO();
    const _validation_ResultDTO = await CreateUser_Role(_user_RoleDTO);
    if (_validation_ResultDTO.Result) {
        ClearUser_RoleFields();
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
        ClearUser_RoleFields();
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
        ClearUser_RoleFields();
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

//Reload SelectBox and TagBox for related fields

