import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../../Common/Utils/Response.js'
import { GetDXCategoryDataSource, CreateCategory, UpdateCategory, DeleteCategory } from './Category_Service.js'
import { GetDXSupportGroupDataSource } from '../../SupportGroupManagement/SupportGroup/SupportGroup_Service.js'
import { GetUserInformation } from '../../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { Role_Enum } from '../../../../AdvancedSettings/SecurityManagement/Role/Role_Enum.js'

document.addEventListener("DOMContentLoaded", async () => {
    await InitializeCategoryCatalogControls();
    await GetUserInformationbyID();
    EventHandler();
});

async function EventHandler() {
    document.getElementById("AddNewCategoryButton").addEventListener("click", ClearCategoryFields);
    document.getElementById("btnCloseCategoryModal").addEventListener("click", ClearCategoryFields);
}

async function GetUserInformationbyID() {
    await dxLoadPanel.show();
    const _userDTO = GetUserDTO();
    const _userInformation = await GetUserInformation(_userDTO);
    await DisabledAdministrationFields(_userInformation[0]);
    await FilterDataSourceBySupportGroups(_userInformation[0]);
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
                $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray }));
                $("#dxCategoryGrid").dxDataGrid("instance").option("dataSource", await GetDXCategoryDataSource({ SupportGroupIDArray: UserDTO.SupportGroupIDArray, GetSupportGroupDTO: true, HasParent: false }));
            }
        }
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin)) {
            $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("dataSource", await GetDXSupportGroupDataSource());
            $("#dxCategoryGrid").dxDataGrid("instance").option("dataSource", await GetDXCategoryDataSource({ HasParent: false }));
        }
    }
}

function DisabledAdministrationFields(UserDTO) {
    if (UserDTO.RoleIDArray != null) {
        if (UserDTO.RoleIDArray.includes(Role_Enum.System_Admin) || UserDTO.RoleIDArray.includes(Role_Enum.Administrator)) {
            $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("readOnly", false);
        }
    }
}

async function InitializeCategoryCatalogControls() {
    $("#dxCategoryNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxCategoryDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxCategoryIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxCategorySupportGroupSelectBox").dxSelectBox({
        dataSource: await GetDXSupportGroupDataSource(),
        valueExpr: "ID",
        displayExpr: "Names",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
        searchExpr: ["Name"],
        searchMode: 'contains',
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _categoryDTO = {
                    SupportGroupDTO: {
                        ID: e.value
                    },
                    IsActive: true
                };
                await GetDataSourceParentSelectBox();
                $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("readOnly", false);
            } else {
                $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("dataSource", []);
                $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("readOnly", true);
            }
        }
    });
    $("#dxCategoryParentSelectBox").dxSelectBox({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "NameWithParent",
        deferRendering: false,
        searchEnabled: true,
        readOnly: true,
        searchExpr: ["Name"],
        searchMode: 'contains'
    });
    $("#dxCategoryGrid").dxDataGrid({
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
            fileName: "CategoryCatalog",
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
            let _CategoryData = data.selectedRowsData[0];
            if (_CategoryData != null) {
            }
        },
        columns:
            [
                {
                    caption: "Actions",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Add", icon: "fa fa-plus-circle text-primary", value: 1 },
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 2 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 3 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: async function (e) {
                                if (e.itemData.value == 1) {
                                    CategoryActionButtons("Save");
                                    $("#hiddenParentCategoryID").val(options.data.ID);
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    await GetDataSourceParentSelectBox(options.data.ParentID);
                                    await PopulateNewChildCategoryFields(options.data);
                                    $('#AddNewCategoryModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {

                                    CategoryActionButtons("Update");
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    PopulateCategoryFields(options.data);
                                    $('#AddNewCategoryModal').modal('show');
                                }
                                else if (e.itemData.value == 3) {

                                    $("#hiddenCategoryID").val(options.data.ID);
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    ShowCategoryDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "Name",
                    dataField: "Name"
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
                    dataField: "SupportGroupName",
                    //groupIndex: 0
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
                },


            ],
        masterDetail: {
            enabled: true,
            template: MasterDetailSubCategory
        },
        onRowExpanded: function (e) {
            $('#dxCategoryGrid').dxDataGrid("instance").updateDimensions();
        },
    });
    document.getElementById("btnCloseCategoryModal").addEventListener("click", ClearCategoryFields);
    CategoryActionButtons("Save");
}
function CategoryActionButtons(Action) {
    $("#CategoryActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("AddNewCategoryButton").addEventListener("click", ClearCategoryFields);
        document.getElementById('CategoryModalTitle').innerText = 'Add Category';
        document.getElementById("CategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateCategoryButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCategoryButton").addEventListener("click", ClearCategoryFields);
        document.getElementById("CreateCategoryButton").addEventListener("click", CreateCategory_Global);
    }
    else {
        // Update
        document.getElementById('CategoryModalTitle').innerText = 'Update Category';
        document.getElementById("CategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateCategoryButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearCategoryButton").addEventListener("click", ClearCategoryFields);
        document.getElementById("UpdateCategoryButton").addEventListener("click", UpdateCategory_Global);
    }
}
function ClearCategoryFields() {
    $('#AddNewCategoryModal').modal('hide');
    CategoryActionButtons("Save");
    $('#hiddenCategoryID').val("0");
    $('#hiddenParentCategoryID').val("0");
    $("#dxCategoryIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("readOnly", false);
    $("#dxCategoryNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCategoryDescriptionTextArea").dxTextArea("instance").option("value", '');
    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", '');
    $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("value", '');
    let keys = $("#dxCategoryGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxCategoryGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxCategoryGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxCategoryGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

async function PopulateCategoryFields(data) {
    $('#hiddenCategoryID').val(data.ID);
    $("#dxCategoryIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxCategoryNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCategoryDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    if (data.ParentID != null && data.ParentID > 0) {
        await $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("value", data.ParentID);
    }
}

async function PopulateNewChildCategoryFields(data) {
    $('#hiddenCategoryID').val("0");
    $("#dxCategoryIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    await $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("value", data.ID);
}


function GetCategoryDTO() {
    let _CategoryDTO = {
        ID: $('#hiddenCategoryID').val(),
        Name: $("#dxCategoryNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxCategoryDescriptionTextArea").dxTextArea("instance").option("value"),
        SupportGroupID: $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value"),
        ParentID: $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxCategoryIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _CategoryDTO;
}
async function ShowCategoryDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this Category',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteCategory_Global();
    } else {
        ClearCategoryFields();
    }
}
async function CreateCategory_Global() {
    await dxLoadPanel.show();
    const _CategoryDTO = GetCategoryDTO();
    const _validation_ResultDTO = await CreateCategory(_CategoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearCategoryFields();
        $('#AddNewCategoryModal').modal('hide');
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateCategory_Global() {
    await dxLoadPanel.show();
    const _CategoryDTO = GetCategoryDTO();
    const _validation_ResultDTO = await UpdateCategory(_CategoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearCategoryFields();
        $('#AddNewCategoryModal').modal('hide');
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteCategory_Global() {
    await dxLoadPanel.show();
    const _CategoryDTO = GetCategoryDTO();
    const _validation_ResultDTO = await DeleteCategory(_CategoryDTO);
    if (_validation_ResultDTO.Result) {
        ClearCategoryFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}

async function GetDataSourceParentSelectBox(ParentID) {
    let _categoryDTO = ParentID > 0 ?
        { ParentID: ParentID, IsActive: true } :
        { SupportGroupID: $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value"), IsActive: true, HasParent: false }
    $("#dxCategoryParentSelectBox").dxSelectBox("instance").reset();
    $("#dxCategoryParentSelectBox").dxSelectBox("instance").option("dataSource", await GetDXCategoryDataSource(_categoryDTO));
}

//#region Subcategory MasterDetail
async function MasterDetailSubCategory(container, masterDetailOptions) {
    await dxLoadPanel.show()
    const _categoryDTO = { ParentID: masterDetailOptions.data.ID };
    const _categoryList = await GetDXCategoryDataSource(_categoryDTO);
    await BuildSubCateogryGrid(container, _categoryList, masterDetailOptions)
    dxLoadPanel.hide()
}

async function BuildSubCateogryGrid(container, CategoryList, masterDeatilOptions) {
    $(`<div id="dxSubCategoryList${masterDeatilOptions.data.ID}">`).dxDataGrid({
        dataSource: CategoryList,
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
            fileName: "CategoryCatalog",
            allowExportSelectedData: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: false,
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
            let _CategoryData = data.selectedRowsData[0];
            if (_CategoryData != null) {
            }
        },
        columns:
            [
                {
                    caption: "Actions",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
                    cellTemplate: function (container, options) {
                        $('<div style="text-align: center;">').appendTo(container).dxMenu({
                            items: [{
                                icon: "fa-solid fa-ellipsis-vertical text-dark",
                                items: [
                                    { text: "Add", icon: "fa fa-plus-circle text-primary", value: 1 },
                                    { text: "Edit", icon: "fa fa-pen-to-square text-success", value: 2 },
                                    { text: "Delete", icon: "fa fa-trash-alt text-danger", value: 3 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: async function (e) {
                                if (e.itemData.value == 1) {
                                    CategoryActionButtons("Save");
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("readOnly", true);
                                    $("#hiddenParentCategoryID").val(options.data.ID);
                                    await GetDataSourceParentSelectBox(options.data.ParentID);
                                    await PopulateNewChildCategoryFields(options.data);
                                    $('#AddNewCategoryModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {

                                    CategoryActionButtons("Update");
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("readOnly", true);
                                    PopulateCategoryFields(options.data);
                                    $('#AddNewCategoryModal').modal('show');
                                }
                                else if (e.itemData.value == 3) {

                                    $("#hiddenCategoryID").val(options.data.ID);
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    ShowCategoryDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "Name",
                    dataField: "Name"
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
                    dataField: "SupportGroupName",
                    //groupIndex: 0
                    visible: false
                },

                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Parent",
                    dataField: "ParentName"
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
                },
            ],
        masterDetail: {
            enabled: true,
            template: MasterDetailThirdCategory
        },
        onRowExpanded: function (e) {
            $('#dxCategoryGrid').dxDataGrid("instance").updateDimensions();
        },
    }).appendTo(container);
    $('#dxCategoryGrid').dxDataGrid("instance").updateDimensions();
}
//#endregion

//#region Third Level Catagory
async function MasterDetailThirdCategory(container, masterDetailOptions) {
    await dxLoadPanel.show()
    const _categoryDTO = { ParentID: masterDetailOptions.data.ID };
    const _categoryList = await GetDXCategoryDataSource(_categoryDTO);
    await BuildThirdCateogryGrid(container, _categoryList, masterDetailOptions, masterDetailOptions.data.ParentID)
    dxLoadPanel.hide()
}

async function BuildThirdCateogryGrid(container, CategoryList, masterDeatilOptions, OldParentID) {
    $(`<div id="dxThirdCategoryList${masterDeatilOptions.data.ID}">`).dxDataGrid({
        dataSource: CategoryList,
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
            fileName: "CategoryCatalog",
            allowExportSelectedData: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: false,
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
            let _CategoryData = data.selectedRowsData[0];
            if (_CategoryData != null) {
            }
        },
        columns:
            [
                {
                    caption: "Actions",
                    alignment: "center",
                    allowFiltering: false,
                    allowSorting: false,
                    width: 'auto',
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
                            onItemClick: async function (e) {
                                if (e.itemData.value == 1) {
                                    CategoryActionButtons("Update");
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("readOnly", true);
                                    await GetDataSourceParentSelectBox(OldParentID);
                                    PopulateCategoryFields(options.data);
                                    $('#AddNewCategoryModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    $("#hiddenCategoryID").val(options.data.ID);
                                    $("#dxCategorySupportGroupSelectBox").dxSelectBox("instance").option("value", options.data.SupportGroupID)
                                    ShowCategoryDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                {
                    caption: "Name",
                    dataField: "Name"
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
                    dataField: "SupportGroupName",
                    //groupIndex: 0
                    visible: false
                },
                {
                    caption: "Description",
                    dataField: "Description"
                },
                {
                    caption: "Parent",
                    dataField: "ParentName"
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
                },


            ],
        onRowExpanded: function (e) {
            $('#dxCategoryGrid').dxDataGrid("instance").updateDimensions();
            $('#dxSubCategoryList' + OldParentID).dxDataGrid("instance").updateDimensions();
        },
    }).appendTo(container);
    $('#dxCategoryGrid').dxDataGrid("instance").updateDimensions();
    $('#dxSubCategoryList' + OldParentID).dxDataGrid("instance").updateDimensions();
}

//#endregion 