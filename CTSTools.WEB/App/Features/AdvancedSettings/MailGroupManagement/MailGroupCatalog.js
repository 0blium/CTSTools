import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreateMailGroup, UpdateMailGroup, DeleteMailGroup, GetDXMailGroupDataSource } from './MailGroup/MailGroup_Service.js'
import { CreateMailGroupMember, UpdateMailGroupMember, DeleteMailGroupMember, GetDXMailGroupMemberDataSource } from './MailGroupMember/MailGroupMember_Service.js'
import { GetDXUserDataSource } from '../UserManagement/User/User_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeMailGroupCatalogControls();
    InitializeMailGroupMemberCatalogControls();
});

//#region Mail Group
async function InitializeMailGroupCatalogControls() {
    $("#dxMailGroupNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });

    $("#dxMailGroupDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });

    $("#dxMailGroupIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxMailGroupGrid").dxDataGrid({
        dataSource: await GetDXMailGroupDataSource(),
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
            fileName: "StatusCatalog",
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
        }, headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
            let _mailGroupData = data.selectedRowsData[0];
            if (_mailGroupData != null) {
                MailGroupActionButtons("Update");
                PopulateMailGroupFields(_mailGroupData);
            }
        },
        columns:
            [
                {
                    caption: "Option",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#MailGroupModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenMailGroupID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenMailGroupID").val(options.data.ID);
                                ShowMailGroupDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    MailGroupActionButtons("Save");
}
function MailGroupActionButtons(Action) {
    $("#MailGroupActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("MailGroupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateMailGroupButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateMailGroupButton").addEventListener("click", CreateMailGroup_Global);
    }
    else {
        // Update
        document.getElementById("MailGroupActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearMailGroupButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateMailGroupButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearMailGroupButton").addEventListener("click", ClearMailGroupFields);
        document.getElementById("UpdateMailGroupButton").addEventListener("click", UpdateMailGroup_Global);
    }
}
function ClearMailGroupFields() {
    $("#MailGroupModal").modal("hide");
    MailGroupActionButtons("Save");
    $('#hiddenMailGroupID').val("");
    $("#dxMailGroupIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxMailGroupNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxMailGroupDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxMailGroupGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxMailGroupGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxMailGroupGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxMailGroupGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateMailGroupFields(data) {
    $('#hiddenMailGroupID').val(data.ID);
    $("#dxMailGroupIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxMailGroupNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxMailGroupDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
}
function GetMailGroupDTO() {
    let _mailGroupDTO = {
        ID: $('#hiddenMailGroupID').val(),
        Name: $("#dxMailGroupNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxMailGroupDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxMailGroupIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _mailGroupDTO;
}
async function ShowMailGroupDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this mail group',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteMailGroup_Global();
    } else {
        ClearMailGroupFields();
    }
}
async function CreateMailGroup_Global() {
    await dxLoadPanel.show();
    const _mailGroupDTO = GetMailGroupDTO();
    const _validation_ResultDTO = await CreateMailGroup(_mailGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupFields();
        await ReloadMailGroupSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateMailGroup_Global() {
    await dxLoadPanel.show();
    const _mailGroupDTO = GetMailGroupDTO();
    const _validation_ResultDTO = await UpdateMailGroup(_mailGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupFields();
        await ReloadMailGroupSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteMailGroup_Global() {
    await dxLoadPanel.show();
    const _mailGroupDTO = GetMailGroupDTO();
    const _validation_ResultDTO = await DeleteMailGroup(_mailGroupDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupFields();
        await ReloadMailGroupSelectBox();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

//#endregion

//#region Mail Group Member
async function InitializeMailGroupMemberCatalogControls() {
    $("#dxMailGroupMemberUserSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxMailGroupMemberMailGroupSelectBox").dxSelectBox({
        dataSource: await GetDXMailGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxMailGroupMemberIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxMailGroupMemberGrid").dxDataGrid({
        dataSource: await GetDXMailGroupMemberDataSource(),
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
            fileName: "MailGroupMemberCatalog",
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
        }, headerFilter: {
            visible: true
        },
        onSelectionChanged: function (data) {
            let _mailGroupMemberData = data.selectedRowsData[0];
            if (_mailGroupMemberData != null) {
                MailGroupMemberActionButtons("Update");
                PopulateMailGroupMemberFields(_mailGroupMemberData);
            }
        },
        columns:
            [
                {
                    caption: "Options",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,                 
                    width: "auto",
                    cellTemplate: function (container, options) {
                        container.height(30);
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#MailGroupMemberModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenMailGroupMemberID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenMailGroupMemberID").val(options.data.ID);
                                ShowMailGroupMemberDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "Is Active", dataField: "IsActive" },
                { caption: "ID", dataField: "ID", visible: false },
                { caption: "Mail Group", dataField: "MailGroupDTO.Name" },
                { caption: "User", dataField: "UserDTO.Name" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By", dataField: "AddedByName" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "LastUpdateByName" },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },


            ],
    });
    MailGroupMemberActionButtons("Save");
}
function MailGroupMemberActionButtons(Action) {
    $("#MailGroupMemberActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("MailGroupMemberActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateMailGroupMemberButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateMailGroupMemberButton").addEventListener("click", CreateMailGroupMember_Global);
    }
    else {
        // Update
        document.getElementById("MailGroupMemberActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-secondary float-end" id="ClearMailGroupMemberButton" type="button">Cancel</button>' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateMailGroupMemberButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("ClearMailGroupMemberButton").addEventListener("click", ClearMailGroupMemberFields);
        document.getElementById("UpdateMailGroupMemberButton").addEventListener("click", UpdateMailGroupMember_Global);
    }
}
function ClearMailGroupMemberFields() {
    $("#MailGroupMemberModal").modal("hide");

    MailGroupMemberActionButtons("Save");
    $('#hiddenMailGroupMemberID').val("");
    $("#dxMailGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxMailGroupMemberMailGroupSelectBox").dxSelectBox("instance").reset();
    $("#dxMailGroupMemberUserSelectBox").dxSelectBox("instance").reset();

    let keys = $("#dxMailGroupMemberGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxMailGroupMemberGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxMailGroupMemberGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxMailGroupMemberGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function PopulateMailGroupMemberFields(data) {
    $('#hiddenMailGroupMemberID').val(data.ID);
    $("#dxMailGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxMailGroupMemberMailGroupSelectBox").dxSelectBox("instance").option("value", data.MailGroupDTO.ID);
    $("#dxMailGroupMemberUserSelectBox").dxSelectBox("instance").option("value", data.UserDTO.ID);
}

function GetMailGroupMemberDTO() {
    let _mailGroupDTO = {
        ID: $('#hiddenMailGroupMemberID').val(),
        MailGroupDTO: {
            ID: $("#dxMailGroupMemberMailGroupSelectBox").dxSelectBox("instance").option("value")
        },
        UserDTO: {
            ID: $("#dxMailGroupMemberUserSelectBox").dxSelectBox("instance").option("value")
        },
        IsActive: $("#dxMailGroupMemberIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _mailGroupDTO;
}
async function ShowMailGroupMemberDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will remove this member from the mail group',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteMailGroupMember_Global();
    } else {
        ClearMailGroupMemberFields();
    }
}

async function CreateMailGroupMember_Global() {
    await dxLoadPanel.show();
    const _mailGroupMemberDTO = GetMailGroupMemberDTO();
    const _validation_ResultDTO = await CreateMailGroupMember(_mailGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupMemberFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateMailGroupMember_Global() {
    await dxLoadPanel.show();
    const _mailGroupMemberDTO = GetMailGroupMemberDTO();
    const _validation_ResultDTO = await UpdateMailGroupMember(_mailGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupMemberFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteMailGroupMember_Global() {
    await dxLoadPanel.show();
    const _mailGroupMemberDTO = GetMailGroupMemberDTO();
    const _validation_ResultDTO = await DeleteMailGroupMember(_mailGroupMemberDTO);
    if (_validation_ResultDTO.Result) {
        ClearMailGroupMemberFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion

//Reload select box for related fields
async function ReloadMailGroupSelectBox() {
    $("#dxMailGroupMemberMailGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXMailGroupDataSource());
}