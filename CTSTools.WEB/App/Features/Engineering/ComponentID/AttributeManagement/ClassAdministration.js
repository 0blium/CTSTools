import { GetDXClassDataSource, CreateClass, CreateMassiveClass, DeleteClass, UpdateClass } from './Class/Class_Service.js'
import { GetDXSubClassDataSource, CreateSubClass, DeleteSubClass, UpdateSubClass } from './SubClass/SubClass_Service.js'
import { GetDXValueDataSource } from './Value/Value_Service.js'
import { GetDXValueLinkDataSource, GetValueLinkInformation } from './ValueLink/ValueLink_Service.js'
import { Attributes } from './Attribute/Attribute_Enum.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { Value_Enum } from './Value/Value_Enum.js'

//#region Decoder Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeClassCatalogControls();
    InitializeSubClassCatalogControls();
});

//#region Class

//Behavior
async function InitializeClassCatalogControls() {
    $("#dxClassComponentTypeLookup").dxLookup({
        dataSource: await GetComponentTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
    });
    $("#dxClassPartTypeLookup").dxLookup({
        dataSource: await GetPartTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        onValueChanged: async function (e) {
            await $("#dxClassComponentTypeLookup").dxLookup("instance").reset();

            if (e.value != 0 && e.value != null) {
                if (e.value == Value_Enum.Manufactured) {
                    document.getElementById("ComponentTypeGroup").hidden = false;
                    let _componentTypeList = await GetFilteredComponentTypeDataSource_Global();
                    $("#dxClassComponentTypeLookup").dxLookup("instance").option("dataSource", _componentTypeList);
                }
                else {
                    document.getElementById("ComponentTypeGroup").hidden = true;
                    $("#dxClassComponentTypeLookup").dxLookup("instance").reset();
                    $("#dxClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);
                }
            }
            else {
                document.getElementById("ComponentTypeGroup").hidden = true;
                $("#dxClassComponentTypeLookup").dxLookup("instance").reset();
                $("#dxClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxClassFileUploader").dxFileUploader({
        accept: ".xlsx",
        selectButtonText: "Select Excel File",
        labelText: "or Drop here",
        uploadMode: "instantly",
        onValueChanged: function (e) {
            var file = e.value[0];
            let _fileDTO;
            let _validationResultDTO;
            if (file) {
                var filename = file.name;
                var reader = new FileReader();
                reader.onload = async function (readerEvent) {
                    var base64File = readerEvent.target.result;
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveClass(_fileDTO);
                    ShowClassValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
    });
    $("#dxClassDescriptionTextArea").dxTextArea({
        placeholder: 'Type a description...'
    });
    $("#dxClassNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxClassCodeTextBox").dxTextBox({
        placeholder: 'Type code...'
    });
    $("#dxClassIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxClassGrid").dxDataGrid({
        dataSource: await GetDXClassDataSource(),
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: "auto",
        showRowLines: true,
        showColumnLines: false,
        showBorders: true,
        focusedRowEnabled: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: false,
        allowColumnDragging: true,
        columnAutoWidth: true,
        groupPanel: {
            visible: "auto",

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
            width: "auto"
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
            let _classData = data.selectedRowsData[0];
            if (_classData != null) {
                ClassActionButtons("Update");
                PopulateClassFields(_classData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#ClassModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenClassID").val(options.data.ClassValueDTO.ID);
                                $("#hiddenClassValueLinkID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenClassValueLinkID").val(options.data.ID);
                                $("#hiddenClassID").val(options.data.ClassValueDTO.ID);
                                ShowDeleteClassQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "ClassValueDTO.Name" },
                { caption: "Code", dataField: "ClassValueDTO.Code" },
                { caption: "Part Type", dataField: "PartTypeDTO.Name" },
                { caption: "Component Type", dataField: "ComponentTypeDTO.Name" },
                { caption: "Description", dataField: "ClassValueDTO.Description" },
                { caption: "Added Date", dataField: "ClassValueDTO.AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "ClassValueDTO.LastUpdate", dataType: 'datetime' },
                { caption: "Added By ID", dataField: "ClassValueDTO.AddedByID", visible: false },
                { caption: "Added By", dataField: "ClassValueDTO.AddedByName", visible: false },
                { caption: "Last Update By ID", dataField: "ClassValueDTO.LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "ClassValueDTO.LastUpdateByName", visible: false },
                { caption: "Is Active", dataField: "ClassValueDTO.IsActive", width: 200 },


            ],
    });
    document.getElementById("ClassButton").addEventListener("click", ClearClassFields);
    document.getElementById("UploadExcelClassCloseModalButton").addEventListener("click", ClearExcelClassModalFields);
    document.getElementById("ClearClassExcelModalButton").addEventListener("click", ClearExcelClassModalFields);
}
function ClassActionButtons(Action) {
    $("#ClassActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("ClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateClassButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateClassButton").addEventListener("click", CreateClass_Global);
        document.getElementById("ClassCloseModalButton").addEventListener("click", ClearClassFields);
    }
    else {
        // Update
        document.getElementById("ClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateClassButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateClassButton").addEventListener("click", UpdateClass_Global);
        document.getElementById("ClassCloseModalButton").addEventListener("click", ClearClassFields);
    }
}
function ClearClassFields() {
    ClassActionButtons("Save");
    $("#hiddenClassID").val("");
    $("#dxClassCodeTextBox").dxTextBox("instance").option("value", "");
    $("#dxClassNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxClassPartTypeLookup").dxLookup("instance").reset();

    document.getElementById("ComponentTypeGroup").hidden = true;
    $("#dxClassComponentTypeLookup").dxLookup("instance").reset();
    //$("#dxClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);

    let keys = $("#dxClassGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxClassGrid").dxDataGrid("instance").deselectRows(keys);
    //    ClearErrorFeedback();
}
function ClearExcelClassModalFields() {
    $('#successClassMessage').hide();
    $('#errorClassMessages').hide();
    var uploader = $("#dxClassFileUploader").dxFileUploader("instance");
    if (uploader) {
        uploader.option("visible", true);
    }
    if (uploader) {
        uploader.reset();
    }
}
function ShowClassValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        if (_validationResultDTO.Data.ClassGoodLinesList.length > 0) {
            successMessage = `Class were created successfully.`;
            $('#successClassMessage').text(successMessage).show();
            document.getElementById('successClassMessage').removeAttribute('hidden');
        }
        if (_validationResultDTO.Data.ClassBadLinesList.length > 0) {
            errorMessages = '<strong>Wrong data:</strong><ul>';
            _validationResultDTO.Data.ClassBadLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row error:<br>Name: ${badLine.ClassValueDTO.Name}. Code: ${badLine.ClassValueDTO.Code}. Description = ${badLine.ClassValueDTO.Description}. Attribute: ${badLine.ClassValueDTO.AttributeName}. Parent Attribute: ${badLine.ParentAttributeName}. Parent Value: ${badLine.ParentValueName}. Child Attribute: ${badLine.ChildAttributeName}. Child Value: ${badLine.ChildValueName}. Is Active = ${badLine.IsActive}.</li>`;
            });
            errorMessages += '</ul>';
            $('#errorClassMessages').html(errorMessages).show();
            document.getElementById('errorClassMessages').removeAttribute('hidden');
        }
    }
    if (_validationResultDTO.Message == "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        $('#errorClassMessages').html(errorMessages).show();
        document.getElementById('errorClassMessages').removeAttribute('hidden');
    }
    if (_validationResultDTO.Message == "Don't have access to this action.") {
        $('#UploadExcelClassModal').modal('hide');
        ClearExcelClassModalFields();
        return HostResponse(_validationResultDTO);
    }
    $("#dxClassFileUploader").dxFileUploader("instance").option("visible", false);
    $("#dxClassGrid").dxDataGrid("instance").refresh();
    ClearClassFields();
}
function PopulateClassFields(ClassDTO) {
    $("#hiddenClassValueLinkID").val(ClassDTO.ID);
    $("#hiddenClassID").val(ClassDTO.ClassValueDTO.ID);
    $("#dxClassPartTypeLookup").dxLookup("instance").option("value", ClassDTO.PartTypeDTO.ID);
    $("#dxClassNameTextBox").dxTextBox("instance").option("value", ClassDTO.ClassValueDTO.Name);
    $("#dxClassCodeTextBox").dxTextBox("instance").option("value", ClassDTO.ClassValueDTO.Code);
    $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value", ClassDTO.ClassValueDTO.Description);
    $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value", ClassDTO.ClassValueDTO.IsActive);
    $("#dxClassComponentTypeLookup").dxLookup("instance").option("value", ClassDTO.ComponentTypeDTO.ID);

}
async function ShowDeleteClassQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove this class, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteClass_Global();
    }
}
function GetClassValueLinkDTO() {
    return {
        ID: $("#hiddenClassValueLinkID").val(),
        ParentAttributeID: $("#dxClassPartTypeLookup").dxLookup("instance").option("value") != Value_Enum.Manufactured ?
            Attributes.PartType :
            Attributes.ComponentType,
        ParentValueID: $("#dxClassPartTypeLookup").dxLookup("instance").option("value") != Value_Enum.Manufactured ?
            $("#dxClassPartTypeLookup").dxLookup("instance").option("value") :
            $("#dxClassComponentTypeLookup").dxLookup("instance").option("value"),
        ChildAttributeID: Attributes.Class,
        ChildValueID: $("#hiddenClassID").val(),
        IsActive: $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
        ClassValueDTO: GetClassAttributeValueDTO()
    }
}
function GetClassAttributeValueDTO() {
    return {
        ID: $("#hiddenClassID").val(),
        Code: $("#dxClassCodeTextBox").dxTextBox("instance").option("value"),
        Name: $("#dxClassNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxClassDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
        AttributeID: Attributes.Class
    }
}

//CRUD
async function CreateClass_Global() {
    await dxLoadPanel.show();
    let _classValueDTO = GetClassValueLinkDTO();
    const _validation_resultDTO = await CreateClass(_classValueDTO)
    if (_validation_resultDTO.Result) {
        $("#dxClassGrid").dxDataGrid("instance").refresh();
        ClearClassFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateClass_Global() {
    await dxLoadPanel.show();
    const _classValueDTO = GetClassValueLinkDTO();
    const _validation_resultDTO = await UpdateClass(_classValueDTO)
    if (_validation_resultDTO.Result) {
        $("#dxClassGrid").dxDataGrid("instance").refresh();
        ClearClassFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function DeleteClass_Global() {
    await dxLoadPanel.show();
    let _classValueDTO = GetClassValueLinkDTO();
    const _validation_resultDTO = await DeleteClass(_classValueDTO)
    if (_validation_resultDTO.Result) {
        $("#dxClassGrid").dxDataGrid("instance").refresh();
        ClearClassFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}

//Data Source
async function GetPartTypeDataSource_Global() {
    const _partTypeDTO = {
        IsActive: true,
        AttributeID: Attributes.PartType
    }
    return await GetDXValueDataSource(_partTypeDTO)
}
async function GetComponentTypeDataSource_Global() {
    const _componentTypeDTO = {
        AttributeID: Attributes.ComponentType,
        IsActive: true,
    }
    return await GetDXValueDataSource(_componentTypeDTO);
}
async function GetFilteredComponentTypeDataSource_Global() {
    let _componentTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxClassPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.ComponentType,
        GetChildValueDTO: true,
    }
    const _valueLinkList = await GetValueLinkInformation(_componentTypeDTO);
    let _componentTypeList = _valueLinkList.map(m => m.ChildValueDTO);
    return _componentTypeList;
}

//#endregion

//#region Sub Class

//Behavior
async function InitializeSubClassCatalogControls() {
    $("#dxSubClassPartTypeLookup").dxLookup({
        dataSource: await GetPartTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                if (e.value == Value_Enum.Manufactured) {
                    document.getElementById("SubClassComponentTypeGroup").hidden = false;
                    let _componentTypeList = await GetSubClassComponentTypeFromPartTypeDataSource_Global();
                    $("#dxSubClassComponentTypeLookup").dxLookup("instance").option("dataSource", _componentTypeList);
                }
                else {
                    $("#dxSubClassClassLookup").dxLookup("instance").reset();
                    $("#dxSubClassClassLookup").dxLookup("instance").option("dataSource", []);
                    document.getElementById("SubClassComponentTypeGroup").hidden = true;
                    $("#dxSubClassComponentTypeLookup").dxLookup("instance").reset();
                    $("#dxSubClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);
                    let _classList = await GetClassFromParentAttributeDataSource_Global(e.value);
                    $("#dxSubClassClassLookup").dxLookup("instance").option("dataSource", _classList);
                }
            }
            else {
                document.getElementById("SubClassComponentTypeGroup").hidden = true;
                $("#dxSubClassComponentTypeLookup").dxLookup("instance").reset();
                $("#dxSubClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);
                $("#dxSubClassClassLookup").dxLookup("instance").reset();
                $("#dxSubClassClassLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxSubClassComponentTypeLookup").dxLookup({
        dataSource: await GetComponentTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                let _classList = await GetClassFromParentAttributeDataSource_Global(e.value);
                $("#dxSubClassClassLookup").dxLookup("instance").option("dataSource", _classList);
            }
            else {
                $("#dxSubClassClassLookup").dxLookup("instance").reset();
                $("#dxSubClassClassLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxSubClassClassLookup").dxLookup({
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,

    });
    $("#dxSubClassDescriptionTextArea").dxTextArea({
        placeholder: 'Type a description...'
    });
    $("#dxSubClassNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxSubClassCodeTextBox").dxTextBox({
        placeholder: 'Type code...'
    });
    $("#dxSubClassIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxSubClassGrid").dxDataGrid({
        dataSource: await GetDXSubClassDataSource(),
        keyExpr: "ID",
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        columnMinWidth: "auto",
        showRowLines: true,
        showColumnLines: false,
        showBorders: true,
        focusedRowEnabled: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: false,
        allowColumnDragging: true,
        columnAutoWidth: true,
        groupPanel: {
            visible: "auto",

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
            width: "auto"
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
            let _subClassData = data.selectedRowsData[0];
            if (_subClassData != null) {
                SubClassActionButtons("Update");
                PopulateSubClassFields(_subClassData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#SubClassModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenSubClassValueLinkID").val(options.data.ID);
                                $("#hiddenSubClassID").val(options.data.SubClassValueDTO.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {

                                $("#hiddenSubClassValueLinkID").val(options.data.ID);
                                $("#hiddenSubClassID").val(options.data.SubClassValueDTO.ID);
                                ShowDeleteSubClassQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "SubClassValueDTO.Name" },
                { caption: "Code", dataField: "SubClassValueDTO.Code" },
                { caption: "Class", dataField: "ClassDTO.Name" },
                { caption: "Component Type", dataField: "ComponentTypeDTO.Name" },
                { caption: "Part Type", dataField: "PartTypeDTO.Name" },

                { caption: "Description", dataField: "SubClassValueDTO.Description" },
                { caption: "Added Date", dataField: "SubClassValueDTO.AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "SubClassValueDTO.LastUpdate", dataType: 'datetime' },
                { caption: "Added By ID", dataField: "SubClassValueDTO.AddedByID", visible: false },
                { caption: "Added By", dataField: "SubClassValueDTO.AddedByName", visible: false },
                { caption: "Last Update By ID", dataField: "SubClassValueDTO.LastUpdateByID", visible: false },
                { caption: "Last Update By", dataField: "SubClassValueDTO.LastUpdateByName", visible: false },
                { caption: "Is Active", dataField: "SubClassValueDTO.IsActive", width: 200 },


            ],
    });
    document.getElementById("SubClassButton").addEventListener("click", ClearSubClassFields);
    document.getElementById("SubClassButton").addEventListener("click", RefreshSubClassGrid);

   
}
function SubClassActionButtons(Action) {
    $("#SubClassActionButtons").empty();
    if (Action == "Save") {
        document.getElementById("SubClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateSubClassButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateSubClassButton").addEventListener("click", CreateSubClass_Global);
        document.getElementById("SubClassCloseModalButton").addEventListener("click", ClearSubClassFields);
    }
    else {
        // Update
        document.getElementById("SubClassActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateSubClassButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateSubClassButton").addEventListener("click", UpdateSubClass_Global);
        document.getElementById("SubClassCloseModalButton").addEventListener("click", ClearSubClassFields);
    }
}
async function ShowDeleteSubClassQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove this sub class, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteSubClass_Global();
    }
}
function ClearSubClassFields() {
    SubClassActionButtons("Save");
    $("#hiddenSubClassID").val("");
    $("#dxSubClassCodeTextBox").dxTextBox("instance").option("value", "");
    $("#dxSubClassNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxSubClassPartTypeLookup").dxLookup("instance").reset();
    document.getElementById("ComponentTypeGroup").hidden = true;
    $("#dxSubClassComponentTypeLookup").dxLookup("instance").reset();
    $("#dxSubClassComponentTypeLookup").dxLookup("instance").option("dataSource", []);
    let keys = $("#dxSubClassGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxSubClassGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxSubClassClassLookup").dxLookup("instance").reset();
    //    ClearErrorFeedback();

}
function GetSubClassValueLinkDTO() {
    return {
        ID: $("#hiddenSubClassValueLinkID").val(),
        ParentAttributeID: Attributes.Class,
        ParentValueID: $("#dxSubClassClassLookup").dxLookup("instance").option("value"),
        ChildAttributeID: Attributes.SubClass,
        ChildValueID: $("#hiddenSubClassID").val(),
        IsActive: $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
        SubClassValueDTO: GetSubClassAttributeValueDTO()
    }
}
function GetSubClassAttributeValueDTO() {
    return {
        ID: $("#hiddenSubClassID").val(),
        Code: $("#dxSubClassCodeTextBox").dxTextBox("instance").option("value"),
        Name: $("#dxSubClassNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value"),
        IsActive: $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value"),
        AttributeID: Attributes.SubClass
    }
}
function PopulateSubClassFields(SubClassDTO) {
    console.log(SubClassDTO)
    $("#hiddenSubClassValueLinkID").val(SubClassDTO.ID);
    $("#hiddenSubClassID").val(SubClassDTO.SubClassValueDTO.ID);
    $("#dxSubClassNameTextBox").dxTextBox("instance").option("value", SubClassDTO.SubClassValueDTO.Name);
    $("#dxSubClassCodeTextBox").dxTextBox("instance").option("value", SubClassDTO.SubClassValueDTO.Code);
    $("#dxSubClassDescriptionTextArea").dxTextArea("instance").option("value", SubClassDTO.SubClassValueDTO.Description);
    $("#dxSubClassPartTypeLookup").dxLookup("instance").option("value", SubClassDTO.PartTypeDTO.ID);
    $("#dxSubClassComponentTypeLookup").dxLookup("instance").option("value", SubClassDTO.ComponentTypeDTO == null ? null : SubClassDTO.ComponentTypeDTO.ID);
    $("#dxSubClassIsActiveCheckBox").dxCheckBox("instance").option("value", SubClassDTO.SubClassValueDTO.IsActive);
    $("#dxSubClassClassLookup").dxLookup("instance").option("value", SubClassDTO.ClassDTO.ID);
}
function RefreshSubClassGrid() {
    $("#dxSubClassGrid").dxDataGrid("instance").refresh();
}
// CRUD
async function CreateSubClass_Global() {
    await dxLoadPanel.show();
    let _subClassValueDTO = GetSubClassValueLinkDTO();
    const _validation_resultDTO = await CreateSubClass(_subClassValueDTO)
    if (_validation_resultDTO.Result) {
        RefreshSubClassGrid();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateSubClass_Global() {
    await dxLoadPanel.show();
    const _subClassValueDTO = GetSubClassValueLinkDTO();
    const _validation_resultDTO = await UpdateSubClass(_subClassValueDTO)
    if (_validation_resultDTO.Result) {
        RefreshSubClassGrid();
        ClearSubClassFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function DeleteSubClass_Global() {
    await dxLoadPanel.show();
    let _subClassValueDTO = GetSubClassValueLinkDTO();
    const _validation_resultDTO = await DeleteSubClass(_subClassValueDTO)
    if (_validation_resultDTO.Result) {
        RefreshSubClassGrid();
        ClearSubClassFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}

//Data Source
async function GetClassFromParentAttributeDataSource_Global(ParentValueID) {
    let _classDTO = {
        IsActive: true,
        ParentAttributeID: ParentValueID == Value_Enum.Purchased ? Attributes.PartType : Attributes.ComponentType,
        ParentValueID: ParentValueID,
        ChildAttribute: Attributes.Class,
        GetChildValueDTO: true
    }
    let _valueLinkList = await GetValueLinkInformation(_classDTO);
    let _classList = _valueLinkList.map(m => m.ChildValueDTO);
    return _classList;
}
async function GetSubClassComponentTypeFromPartTypeDataSource_Global() {
    let _componentTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxSubClassPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.ComponentType,
        GetChildValueDTO: true,
    }
    const _valueLinkList = await GetValueLinkInformation(_componentTypeDTO);
    let _componentTypeList = _valueLinkList.map(m => m.ChildValueDTO);
    return _componentTypeList;
}

//#endregion