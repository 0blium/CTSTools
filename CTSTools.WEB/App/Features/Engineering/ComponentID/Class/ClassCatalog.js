import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { CreateClass, UpdateClass, DeleteClass, GetDXClassDataSource } from '../Class/Class_Service.js'
import { CreateSubClass, UpdateSubClass, DeleteSubClass, GetDXSubClassDataSource } from './SubClass/SubClass_Service.js'
import { GetDXPartTypeDataSource } from '../PartType/PartType_Service.js'
import { GetDXComponentTypeDataSource } from '../ComponentType/ComponentType_Service.js'
import { Value_Enum } from '../AttributeManagement/Value/Value_Enum.js'


document.addEventListener("DOMContentLoaded", () => {
    InitializeClassCatalogControls();
    InitializeSubClassCatalogControls();
});

//#region Class
async function InitializeClassCatalogControls() {
    $("#dxClassNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxCodeTextBox").dxTextBox({
        placeholder: 'Type code..'
    });
    $("#dxPartTypeSelectBox").dxSelectBox({
        dataSource: await GetDXPartTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: async function (e) {
            await $("#dxComponentTypeSelectBox").dxSelectBox("instance").reset();

            if (e.value != 0 && e.value != null) {
                let _partTypeDTO = e.component.getDataSource().items().find(item => item.ID === e.value);
                
                if (_partTypeDTO.ValueID == Value_Enum.Manufactured) {
                    document.getElementById("ComponentTypeGroup").hidden = false;
                    $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", await GetDXComponentTypeDataSource({IsActive:true,PartTypeID:e.value}));
                }
                else {
                    document.getElementById("ComponentTypeGroup").hidden = true;
                    $("#dxComponentTypeSelectBox").dxSelectBox("instance").reset();
                    $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", []);
                }
            }
            else {
                document.getElementById("ComponentTypeGroup").hidden = true;
                $("#dxComponentTypeSelectBox").dxSelectBox("instance").reset();
                $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        }
        //onValueChanged: function (e) {
        //    // We get the complete item based on the selected value in the SelectBox
        //    let _partTypeDTO = e.component.getDataSource().items().find(item => item.ID === e.value);
        //    console.log(_partTypeDTO.ValueID);
        //    console.log(_partTypeDTO.ID);
        //}
    });
    $("#dxComponentTypeSelectBox").dxSelectBox({
        dataSource: await GetDXComponentTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true
    });
    $("#dxClassDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxClassIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxClassGrid").dxDataGrid({
        dataSource: await GetDXClassDataSource(),
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
            fileName: "ClassCatalog",
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
            let _ClassData = data.selectedRowsData[0];
            if (_ClassData != null) {
                ClassActionButtons("Update");
                PopulateClassFields(_ClassData);
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
                                    $('#SaveClassRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenClassID').value = options.data.ID;
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
                    dataField: "Name"
                },
                {
                    caption: "Code",
                    dataField: "Code"
                },
                {
                    caption: "Part Type",
                    dataField: "PartTypeName"
                },
                {
                    caption: "Component Type",
                    dataField: "ComponentTypeName"
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
    document.getElementById("btnCloseClassModal").addEventListener("click", ClearClassFields);
    ClassActionButtons("Save");
}
function ClassActionButtons(Action) {
    $("#ClassActionButtons").empty();
    document.getElementById('ClassModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewClassBtn").addEventListener("click", ClearClassFields);
        document.getElementById('ClassModalTitle').innerText = 'Add Class';
        document.getElementById("ClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateClassButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearClassButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearClassButton").addEventListener("click", ClearClassFields);
        document.getElementById("CreateClassButton").addEventListener("click", CreateClass_Global);
    }
    else {
        // Update
        document.getElementById('ClassModalTitle').innerText = 'Update Class';
        document.getElementById("ClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateClassButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearClassButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearClassButton").addEventListener("click", ClearClassFields);
        document.getElementById("UpdateClassButton").addEventListener("click", UpdateClass_Global);
    }
}
function ClearClassFields() {
    $('#SaveClassRecordModal').modal('hide');
    ClassActionButtons("Save");
    $('#hiddenClassID').val("");
    $('#hiddenValueID').val("");
    $('#hiddenValueLinkID').val("");
    $("#dxPartTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxComponentTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxClassNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCodeTextBox").dxTextBox("instance").option("value", '');
    $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxClassGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxClassGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxClassGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxClassGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateClassFields(data) {
    $('#hiddenClassID').val(data.ID);
    $('#hiddenValueID').val(data.ValueID);
    $('#hiddenValueLinkID').val(data.ValueLinkID);
    $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxClassNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCodeTextBox").dxTextBox("instance").option("value", data.Code);
    $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxPartTypeSelectBox").dxSelectBox("instance").option("value", data.PartTypeID);
    $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("value", data.ComponentTypeID);
}

function GetClassDTO() {
    let _ClassDTO = {
        ID: $('#hiddenClassID').val(),
        ValueID: $('#hiddenValueID').val(),
        ValueLinkID: $('#hiddenValueLinkID').val(),
        Name: $("#dxClassNameTextBox").dxTextBox("instance").option("value"),
        Code: $("#dxCodeTextBox").dxTextBox("instance").option("value"),
        PartTypeID: $("#dxPartTypeSelectBox").dxSelectBox("instance").option("value"),
        ComponentTypeID: $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("value"),
        Description: $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _ClassDTO;
}

async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this class',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteClass_Global();
    } else {
        ClearClassFields();
    }
}

async function CreateClass_Global() {
    await dxLoadPanel.show();
    const _ClassDTO = GetClassDTO();
    const _validation_ResultDTO = await CreateClass(_ClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateClass_Global() {
    await dxLoadPanel.show();
    const _ClassDTO = GetClassDTO();
    const _validation_ResultDTO = await UpdateClass(_ClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteClass_Global() {
    await dxLoadPanel.show();
    const _ClassDTO = GetClassDTO();
    const _validation_ResultDTO = await DeleteClass(_ClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion

//#region Sub class
async function InitializeSubClassCatalogControls() {
    $("#dxSubClassNameTextBox").dxTextBox({
        placeholder: 'Type name..'
    });
    $("#dxSubClassCodeTextBox").dxTextBox({
        placeholder: 'Type code..'
    });
    $("#dxSubClassPartTypeSelectBox").dxSelectBox({
        dataSource: await GetDXPartTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _partTypeDTO = e.component.getDataSource().items().find(item => item.ID === e.value);

                if (_partTypeDTO.ValueID == Value_Enum.Manufactured) {
                    document.getElementById("SubClassComponentTypeGroup").hidden = false;
                    $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", await GetDXComponentTypeDataSource(
                        {
                            IsActive: true,
                            PartTypeID: _partTypeDTO.ID
                        }));
                }
                else {
                    $("#dxSubClassClassSelectBox").dxSelectBox("instance").reset();
                    $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("dataSource", []);
                    document.getElementById("SubClassComponentTypeGroup").hidden = true;
                    $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").reset();
                    $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", []);
                    $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("dataSource", await GetDXClassDataSource(
                        {
                            IsActive: true,
                            PartTypeID: _partTypeDTO.ID
                        }));
                }
            }
            else {
                document.getElementById("SubClassComponentTypeGroup").hidden = true;
                $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").reset();
                $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").option("dataSource", []);
                $("#dxSubClassClassSelectBox").dxSelectBox("instance").reset();
                $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        }
        //onValueChanged: function (e) {
        //    // We get the complete item based on the selected value in the SelectBox
        //    let _partTypeDTO = e.component.getDataSource().items().find(item => item.ID === e.value);
        //    console.log(_partTypeDTO.ValueID);
        //    console.log(_partTypeDTO.ID);
        //}
    });
    $("#dxSubClassComponentTypeSelectBox").dxSelectBox({
        dataSource: await GetDXComponentTypeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("dataSource", await GetDXClassDataSource(
                    {
                        IsActive: true,
                        PartTypeID: $("#dxSubClassPartTypeSelectBox").dxSelectBox("instance").option("value"),
                        ComponentTypeID: e.value
                    }));
            }
            else {
                $("#dxSubClassClassSelectBox").dxSelectBox("instance").reset();
                $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("dataSource", []);
            }
        }
    });
    $("#dxSubClassClassSelectBox").dxSelectBox({
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,

    });
    $("#dxSubClassDescriptionTextArea").dxTextArea({
        placeholder: 'Type description..'
    });
    $("#dxSubClassIsActiveCheckBox").dxCheckBox({
        value: true,
    });
    $("#dxSubClassGrid").dxDataGrid({
        dataSource: await GetDXSubClassDataSource(),
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
            fileName: "SubClassCatalog",
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
            let _SubClassData = data.selectedRowsData[0];
            if (_SubClassData != null) {
                SubClassActionButtons("Update");
                PopulateSubClassFields(_SubClassData);
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
                                    $('#SubClassModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenSubClassID').value = options.data.ID;
                                    ShowSubClassDeleteQuestion();
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
                    dataField: "Name"
                },
                {
                    caption: "Code",
                    dataField: "Code"
                },
                {
                    caption: "Class",
                    dataField: "ClassName"
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
    document.getElementById("SubClassCloseModalButton").addEventListener("click", ClearSubClassFields);
    SubClassActionButtons("Save");
}
function SubClassActionButtons(Action) {
    $("#SubClassActionButtons").empty();
    document.getElementById('SubClassModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewSubClassBtn").addEventListener("click", ClearSubClassFields);
        document.getElementById('SubClassModalTitle').innerText = 'Add Sub Class';
        document.getElementById("SubClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateSubClassButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSubClassButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSubClassButton").addEventListener("click", ClearSubClassFields);
        document.getElementById("CreateSubClassButton").addEventListener("click", CreateSubClass_Global);
    }
    else {
        // Update
        document.getElementById('SubClassModalTitle').innerText = 'Update SubClass';
        document.getElementById("SubClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateSubClassButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearSubClassButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearSubClassButton").addEventListener("click", ClearSubClassFields);
        document.getElementById("UpdateSubClassButton").addEventListener("click", UpdateSubClass_Global);
    }
}
function ClearSubClassFields() {
    $('#SubClassModal').modal('hide');
    SubClassActionButtons("Save");
    $('#hiddenSubClassID').val("");
    $('#hiddenSubClassValueID').val("");
    $("#dxSubClassPartTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxSubClassComponentTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxSubClassClassSelectBox").dxSelectBox("instance").reset();
    $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSubClassNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxSubClassCodeTextBox").dxTextBox("instance").option("value", '');
    $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value", '');

    let keys = $("#dxSubClassGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSubClassGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSubClassGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxSubClassGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function PopulateSubClassFields(data) {
    $('#hiddenSubClassID').val(data.ID);
    $('#hiddenSubClassValueID').val(data.ValueID);
    $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
    $("#dxSubClassNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxCodeTextBox").dxTextBox("instance").option("value", data.Code);
    $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("value", data.ClassID);
    //$("#dxPartTypeSelectBox").dxSelectBox("instance").option("value", data.PartTypeID);
    //$("#dxComponentTypeSelectBox").dxSelectBox("instance").option("value", data.ComponentTypeID);

}

function GetSubClassDTO() {
    let _SubClassDTO = {
        ID: $('#hiddenSubClassID').val(),
        ValueID: $('#hiddenSubClassValueID').val(),
        Name: $("#dxSubClassNameTextBox").dxTextBox("instance").option("value"),
        Code: $("#dxSubClassCodeTextBox").dxTextBox("instance").option("value"),
        ClassID: $("#dxSubClassClassSelectBox").dxSelectBox("instance").option("value"),
        //PartTypeID: $("#dxPartTypeSelectBox").dxSelectBox("instance").option("value"),
        //ComponentTypeID: $("#dxComponentTypeSelectBox").dxSelectBox("instance").option("value"),
        Description: $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _SubClassDTO;
}

async function ShowSubClassDeleteQuestion() {
    const _alert = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'You will delete this SubClass',
        confirmButtonText: `Delete`,
        confirmButtonColor: '#ea4335',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSubClass_Global();
    } else {
        ClearSubClassFields();
    }
}

async function CreateSubClass_Global() {
    await dxLoadPanel.show();
    const _SubClassDTO = GetSubClassDTO();
    const _validation_ResultDTO = await CreateSubClass(_SubClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearSubClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateSubClass_Global() {
    await dxLoadPanel.show();
    const _SubClassDTO = GetSubClassDTO();
    const _validation_ResultDTO = await UpdateSubClass(_SubClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearSubClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteSubClass_Global() {
    await dxLoadPanel.show();
    const _SubClassDTO = GetSubClassDTO();
    const _validation_ResultDTO = await DeleteSubClass(_SubClassDTO);
    if (_validation_ResultDTO.Result) {
        ClearSubClassFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
//#endregion