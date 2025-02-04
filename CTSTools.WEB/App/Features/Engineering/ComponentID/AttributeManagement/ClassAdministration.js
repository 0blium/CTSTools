import { GetDXClassDataSource, CreateClass, CreateMassiveClass, DeleteClass, UpdateClass } from './Class/Class_Service.js'
import { GetDXSubClassDataSource, CreateSubClass, CreateMassiveSubClass, DeleteSubClass, UpdateSubClass } from './SubClass/SubClass_Service.js'
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
                    var _propetieNameArray = ClassPropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
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
    document.getElementById("ExcelClassFormatButton").addEventListener("click", ExportClassExcelFormat);
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
//#region Class Excel functions
function ExportClassExcelFormat() {
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Code', key: 'code', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
        { header: 'Part Type', key: 'parttype', width: 30 },
        { header: 'Component Type', key: 'componenttype', width: 30 }
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'ClassFormat.xlsx';
        link.click();
    });
}
function ClearExcelClassModalFields() {
    $('#successClassMessage').hide();
    $('#errorClassMessages').hide();
    var uploader = $("#dxClassFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowClassSuccessMessageExcelModal(Message) {
    $('#successClassMessage').text(Message).show();
    $('#successClassMessage').removeAttr('hidden');
}
function ShowClassErrorMessagesExcelModal(Message) {
    $('#errorClassMessages').html(Message).show();
    $('#errorClassMessages').removeAttr('hidden');
}
function ShowClassValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "Class were created successfully.";
            ShowClassSuccessMessageExcelModal(successMessage);
            $("#dxClassGrid").dxDataGrid("instance").refresh();
            ClearClassFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "Class created: Some were skipped due to missing or invalid data.";
            ShowClassSuccessMessageExcelModal(successMessage);
            $("#dxClassGrid").dxDataGrid("instance").refresh();
            ClearClassFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Name: ${badLine.ClassValueDTO.Name}, Code: ${badLine.ClassValueDTO.Code}, Description = ${badLine.ClassValueDTO.Description}, Part Type: ${badLine.PartTypeDTO.Name}, Component Type: ${badLine.ComponentTypeDTO.Name}.</li>`;
            });
            errorMessages += '</ul>';
            ShowClassErrorMessagesExcelModal(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowClassErrorMessagesExcelModal(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelClassModal').modal('hide');
        ClearExcelClassModalFields();
        return HostResponse(_validationResultDTO);
    }
    // Hide FileUploader to show messages
    $("#dxClassFileUploader").dxFileUploader("instance").option("visible", false);
}
function ClassPropertyNameArray() {
    // With Object.keys we create an array of properties of the ClassDTO object
    var _propertyNameArray = Object.keys(GetClassAttributeValueDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    // In this case of the Class we will change the name of AttributeID to Part Type
    // this so that it matches the names of the excel column
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive")
        .map(PropertyName => PropertyName.includes("AttributeID") ? "PartType" : PropertyName);
    // In this case we add the missing columns
    _propertyNameArray.push("ComponentType");
    return _propertyNameArray;
}
//#endregion
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
    $("#dxSubClassFileUploader").dxFileUploader({
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
                    var _propetieNameArray = SubClassPropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveSubClass(_fileDTO);
                    ShowSubClassValidationResults(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
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
    document.getElementById("UploadExcelSubClassCloseModalButton").addEventListener("click", ClearExcelSubClassModalFields);
    document.getElementById("ClearSubClassExcelModalButton").addEventListener("click", ClearExcelSubClassModalFields);
    document.getElementById("ExcelSubClassFormatButton").addEventListener("click", ExportSubClassExcelFormat);
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
//#region SubClass Excel functions
function ExportSubClassExcelFormat() {
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Code', key: 'code', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
        { header: 'Class', key: 'class', width: 30 },
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'SubClassFormat.xlsx';
        link.click();
    });
}
function ClearExcelSubClassModalFields() {
    $('#successSubClassMessage').hide();
    $('#errorSubClassMessages').hide();
    var uploader = $("#dxSubClassFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (uploader) {
        uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (uploader) {
        uploader.reset();
    }
}
function ShowSubClassSuccessMessage(Message) {
    $('#successSubClassMessage').text(Message).show();
    $('#successSubClassMessage').removeAttr('hidden');
}

function ShowSubClassErrorMessages(Message) {
    $('#errorSubClassMessages').html(Message).show();
    $('#errorSubClassMessages').removeAttr('hidden');
}

function ShowSubClassValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "SubClass were created successfully.";
            ShowSubClassSuccessMessage(successMessage);
            RefreshSubClassGrid();
            ClearSubClassFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "SubClass created: Some were skipped due to missing or invalid data.";
            ShowSubClassSuccessMessage(successMessage);
            RefreshSubClassGrid();
            ClearSubClassFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row ${badLine.ID}:<br>Name: ${badLine.SubClassValueDTO.Name}, Code: ${badLine.SubClassValueDTO.Code}, Description = ${badLine.SubClassValueDTO.Description}, Class = ${badLine.ParentValueName}.</li>`;
            });
            errorMessages += '</ul>';
            ShowSubClassErrorMessages(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowSubClassErrorMessages(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelSubClassModal').modal('hide');
        ClearExcelSubClassModalFields();
        return HostResponse(_validationResultDTO);
    }
    // Hide FileUploader to show messages
    $("#dxSubClassFileUploader").dxFileUploader("instance").option("visible", false);
}
function SubClassPropertyNameArray() {
    // With Object.keys we create an array of properties of the SubClassDTO object
    var _propertyNameArray = Object.keys(GetSubClassAttributeValueDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    // In this case of the Class we will change the name of AttributeID to Class
    // this so that it matches the names of the excel column
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive")
        .map(PropertyName => PropertyName.includes("AttributeID") ? "Class" : PropertyName);
    return _propertyNameArray;
}
//#endregion
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