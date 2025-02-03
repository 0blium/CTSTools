import { GetDXKPIDataSource, CreateKPI, CreateMassiveKPI, UpdateKPI, DeleteKPI } from './KPI/KPI_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetDXDepartmentDataSource } from '../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'
import { GetDXFacilityDataSource } from '../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXUserDataSource } from '../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXUnitOfMeasureDataSource } from '../../../AdvancedSettings/UnitOfMeasure/UnitOfMeasure_Service.js'
import { GetDXValueTypeDataSource } from '../Settings/ValueType/ValueType_Service.js'
import { GetDXGoalRangeDataSource } from '../Settings/GoalRange/GoalRange_Service.js'
import { GetDXEquivalenceDataSource } from '../Settings/Equivalence/Equivalence_Service.js'
import { GetDXDashboardCategoryDataSource } from '../Settings/DashboardCategory/DashboardCategory_Service.js'

//#region KPI Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeKPICatalogControls();
});
async function InitializeKPICatalogControls() {
    $("#dxKPINameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxKPIDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...',
        height: 110
    });
    $("#dxKPIGoalNumberBox").dxNumberBox({
        min: 0,
        placeholder: "Enter the goal",
        format: "#,##0.##",
    });
    $("#dxKPIUnitOfMeasureSelectBox").dxSelectBox({
        dataSource: await GetDXUnitOfMeasureDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPICategorySelectBox").dxSelectBox({
        dataSource: await GetDXDashboardCategoryDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIOwnerDepartmentSelectBox").dxSelectBox({
        dataSource: await GetDXDepartmentDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIResponsibleSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIResponsibleDepartmentSelectBox").dxSelectBox({
        dataSource: await GetDXDepartmentDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIValueTypeSelectBox").dxSelectBox({
        dataSource: await GetDXValueTypeDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxKPIEquivalenceSelectBox").dxSelectBox({
        dataSource: await GetDXEquivalenceDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });

    //$("#dxKPICalculationTypeSelectBox").dxSelectBox({
    //    dataSource: GetDXCalculationTypeDataSource({ IsActive: true }),
    //    displayExpr: "Name",
    //    valueExpr: "ID",
    //    searchEnable: true,
    //    popupWidth: 450,
    //});
    $("#dxKPIIsActiveCheckBox").dxCheckBox({
        value: true,
        visible: false
    });
    $("#dxKPIFileUploader").dxFileUploader({
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
                    var _propetieNameArray = KPIPropertyNameArray();
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File,
                        DirectoryArray: _propetieNameArray
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveKPI(_fileDTO);
                    ShowMessagesKPIExcelModal(_validationResultDTO);
                    dxLoadPanel.hide();
                };
                reader.readAsDataURL(file);
            }
        }
    });
    $("#dxKPIGrid").dxDataGrid({
        dataSource: await GetDXKPIDataSource({ IsActive: true }),
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
            fileName: "KPICatalog",
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
            let _KPIData = data.selectedRowsData[0];
            if (_KPIData != null) {
                KPIActionButtons("Update");
                PopulateKPIFields(_KPIData);
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
                                    { text: "Edit item", icon: "fa fa-pen-to-square text-info", value: 1 },
                                    { text: "Delete item", icon: "fa fa-trash-alt text-danger", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                if (e.itemData.value == 1) {
                                    $('#SaveKPICategoryRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenKPIID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Unit Of Measure", dataField: "UnitOfMeasureName" },
                { caption: "Value Type", dataField: "ValueTypeName" },
                { caption: "Goal", dataField: "Goal" },
                { caption: "Owner", dataField: "OwnerName" },
                { caption: "Responsible", dataField: "ResponsibleName" },
                //{ caption: "Shared", dataField: "Shared",  },
                { caption: "Goal Range", dataField: "GoalRangeValue" },
                { caption: "Facility", dataField: "FacilityName" },
                { caption: "Equivalence", dataField: "EquivalenceName" },
                { caption: "Category", dataField: "DashboardCategoryName" },
                { caption: "Owner Department", dataField: "OwnerDepartmentName" },
                { caption: "Responsible Department", dataField: "ResponsibleDepartmentName" },
                { caption: "Status", dataField: "StatusName" },
                //{ caption: "Is Parent", dataField: "IsParent",  },
                //{ caption: "Calculation Type", dataField: "CalculationTypeName",  },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseKPICategoryModal").addEventListener("click", ClearKPIFields);
    document.getElementById("UploadExcelKPICloseModalButton").addEventListener("click", ClearExcelModalFields);
    document.getElementById("ClearExcelKPIButton").addEventListener("click", ClearExcelModalFields);
    document.getElementById("ExcelKPIFormatButton").addEventListener("click", ExportKPIExcelFormat);
    KPIActionButtons("Save");
}
async function PopulateKPIFields(data) {
    $("#hiddenKPIID").val(data.ID);
    $("#hiddenStatusID").val(data.StatusID);
    $("#dxKPINameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxKPIGoalNumberBox").dxNumberBox("instance").option("value", data.Goal);
    $("#dxKPIDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxKPIUnitOfMeasureSelectBox").dxSelectBox("instance").option("value", data.UnitOfMeasureID);
    $("#dxKPICategorySelectBox").dxSelectBox("instance").option("value", data.DashboardCategoryID);
    $("#dxKPIValueTypeSelectBox").dxSelectBox("instance").option("value", data.ValueTypeID);
    $("#dxKPIFacilitySelectBox").dxSelectBox("instance").option("value", data.FacilityID);
    $("#dxKPIOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerID);
    $("#dxKPIOwnerDepartmentSelectBox").dxSelectBox("instance").option("value", data.OwnerDepartmentID);
    $("#dxKPIResponsibleSelectBox").dxSelectBox("instance").option("value", data.ResponsibleID);
    $("#dxKPIResponsibleDepartmentSelectBox").dxSelectBox("instance").option("value", data.ResponsibleDepartmentID);
    $("#dxKPIEquivalenceSelectBox").dxSelectBox("instance").option("value", data.EquivalenceID)
    $("#dxKPIIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);

}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the KPI, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteKPI_Global();
    } else {
        ClearKPIFields();
    }

}
function KPIActionButtons(Action) {
    $("#KPIActionButtons").empty();
    document.getElementById('KPICategoryModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewKPICategoryBtn").addEventListener("click", ClearKPIFields);
        document.getElementById('KPICategoryModalTitle').innerText = 'Add KPI'
        document.getElementById("KPIActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateKPIButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearKPIFields);
        document.getElementById("CreateKPIButton").addEventListener("click", CreateKPI_Global);
    }
    else {
        // Update
        document.getElementById('KPICategoryModalTitle').innerText = 'Update KPI'
        document.getElementById("KPIActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateKPIButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearKPIButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearKPIButton").addEventListener("click", ClearKPIFields);
        document.getElementById("UpdateKPIButton").addEventListener("click", UpdateKPI_Global);
    }
}
function ClearKPIFields() {
    $('#SaveKPICategoryRecordModal').modal('hide');
    KPIActionButtons("Save");
    $("#hiddenKPIID").val("");
    $("#hiddenStatusID").val("");
    $("#dxKPINameTextBox").dxTextBox("instance").option("value", "");
    $("#dxKPIGoalNumberBox").dxNumberBox("instance").option("value", "0");
    $("#dxKPIDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxKPIUnitOfMeasureSelectBox").dxSelectBox("instance").reset();
    $("#dxKPICategorySelectBox").dxSelectBox("instance").reset();
    $("#dxKPIValueTypeSelectBox").dxSelectBox("instance").reset();
    //$("#dxKPISharedCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxKPIFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxKPIEquivalenceSelectBox").dxSelectBox("instance").reset();
    //$("#dxKPIStatusSelectBox").dxSelectBox("instance").reset();
    //$("#dxKPIIsParentCheckBox").dxCheckBox("instance").option("value", true);
    //$("#dxKPICalculationTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxKPIOwnerDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxKPIOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxKPIResponsibleSelectBox").dxSelectBox("instance").reset();
    $("#dxKPIResponsibleSelectBox").dxSelectBox("instance").reset()
    $("#dxKPIResponsibleDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxKPIIsActiveCheckBox").dxCheckBox("instance").option("value", true);

    let keys = $("#dxKPIGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxKPIGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxKPIGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxKPIGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function GetKPIDTO() {
    let _KPIDTO = {
        ID: $("#hiddenKPIID").val(),
        //StatusID: $("#hiddenStatusID").val(),
        Name: $("#dxKPINameTextBox").dxTextBox("instance").option("value"),
        Goal: $("#dxKPIGoalNumberBox").dxNumberBox("instance").option("value"),
        Description: $("#dxKPIDescriptionTextArea").dxTextArea("instance").option("value"),
        UnitOfMeasureID: $("#dxKPIUnitOfMeasureSelectBox").dxSelectBox("instance").option("value"),
        DashboardCategoryID: $("#dxKPICategorySelectBox").dxSelectBox("instance").option("value"),
        ValueTypeID: $("#dxKPIValueTypeSelectBox").dxSelectBox("instance").option("value"),
        //Shared: $("#dxKPISharedCheckBox").dxCheckBox("instance").option("value"),
        FacilityID: $("#dxKPIFacilitySelectBox").dxSelectBox("instance").option("value"),
        EquivalenceID: $("#dxKPIEquivalenceSelectBox").dxSelectBox("instance").option("value"),
        //IsParent: $("#dxKPIIsParentCheckBox").dxCheckBox("instance").option("value"),
        //CalculationTypeID: $("#dxKPICalculationTypeSelectBox").dxSelectBox("instance").option("value"),
        OwnerID: $("#dxKPIOwnerSelectBox").dxSelectBox("instance").option("value"),
        OwnerDepartmentID: $("#dxKPIOwnerDepartmentSelectBox").dxSelectBox("instance").option("value"),
        ResponsibleID: $("#dxKPIResponsibleSelectBox").dxSelectBox("instance").option("value"),
        ResponsibleDepartmentID: $("#dxKPIResponsibleDepartmentSelectBox").dxSelectBox("instance").option("value"),
        IsActive: $("#dxKPIIsActiveCheckBox").dxCheckBox("instance").option("value"),

    }
    return _KPIDTO;
}

//#region KPI Excel functions
function ExportKPIExcelFormat() {
    // Create the Excel workbook and sheet
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Sheet');
    // Define the columns of the sheet
    worksheet.columns = [
        { header: 'Name', key: 'name', width: 30 },
        { header: 'Description', key: 'description', width: 30 },
        { header: 'Unit Of Measure', key: 'unitofmeasure', width: 25 },
        { header: 'Value Type', key: 'valuetype', width: 25 },
        { header: 'Goal', key: 'goal', width: 20 },
        { header: 'Owner', key: 'owner', width: 25 },
        { header: 'Responsible', key: 'responsible', width: 25 },
        { header: 'Facility', key: 'facility', width: 25 },
        { header: 'Equivalence', key: 'equivalence', width: 25 },
        { header: 'Category', key: 'category', width: 25 },
        { header: 'Owner Department', key: 'ownerdepartment', width: 25 },
        { header: 'Responsible Department', key: 'responsibledepartment', width: 30 }
    ];
    // Set the header style to bold
    worksheet.getRow(1).font = { bold: true };
    // Create the Excel file and download it
    workbook.xlsx.writeBuffer().then(function (buffer) {
        var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'KPIFormat.xlsx';
        link.click();
    });
}
function ShowKPISuccessMessageExcelModal(Message) {
    $('#successKPIMessage').text(Message).show();
    $('#successKPIMessage').removeAttr('hidden');
}
function ShowKPIErrorMessagesExcelModal(Message) {
    $('#errorKPIMessages').html(Message).show();
    $('#errorKPIMessages').removeAttr('hidden');
}
function ShowMessagesKPIExcelModal(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null) {
        const _goodLinesList = _validationResultDTO.Data.GoodRowLinesList.length > 0;
        const _badLinesList = _validationResultDTO.Data.BadRowLinesList.length > 0;
        if (_goodLinesList && !_badLinesList) {
            // If there are no bad lines, a message is sent that all the data was created.
            successMessage = "KPIs were created successfully.";
            ShowKPISuccessMessageExcelModal(successMessage);
            ClearKPIFields();
        }
        else if (_goodLinesList && _badLinesList) {
            // If there are good and bad lines, a message is sent that there was missing data to save.
            successMessage = "KPIs created: Some were skipped due to missing or invalid data.";
            ShowKPISuccessMessageExcelModal(successMessage);
            ClearKPIFields();
        }
        if (_badLinesList) {
            // If there are bad lines, add each one in the message
            errorMessages = '<strong>The following rows contain invalid data:</strong><ul>';
            _validationResultDTO.Data.BadRowLinesList.forEach(function (BadLine) {
                var _goal = BadLine.Goal == -1 ? "Error, The Goal is null or different of numbers" : BadLine.Goal;
                errorMessages += `<li>Row ${BadLine.ID}:<br>Name: ${BadLine.Name}, Description = ${BadLine.Description}, Unit Of Measure = ${BadLine.UnitOfMeasureName}, Value Type = ${BadLine.ValueTypeName}, Goal = ${_goal}, Owner = ${BadLine.OwnerName}, Responsible = ${BadLine.ResponsibleName}, Facility = ${BadLine.FacilityName}, Equivalence = ${BadLine.EquivalenceName}, Category = ${BadLine.DashboardCategoryName}, Owner Department = ${BadLine.OwnerDepartmentName}, Responsible Department = ${BadLine.ResponsibleDepartmentName}.</li>`;
            });
            errorMessages += '</ul>';
            ShowKPIErrorMessagesExcelModal(errorMessages);
        }
    }
    if (_validationResultDTO.Message === "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        ShowKPIErrorMessagesExcelModal(errorMessages);
    }
    if (_validationResultDTO.Message === "Don't have access to this action.") {
        $('#UploadExcelKPIModal').modal('hide');
        ClearExcelModalFields();
        return HostResponse(_validationResultDTO);
    }
    // Hide FileUploader to show messages
    $("#dxKPIFileUploader").dxFileUploader("instance").option("visible", false);
}
function ClearExcelModalFields() {
    $('#successKPIMessage').hide();
    $('#errorKPIMessages').hide();
    var _uploader = $("#dxKPIFileUploader").dxFileUploader("instance");
    // Show FileUploader
    if (_uploader) {
        _uploader.option("visible", true);
    }
    // reset FileUploader to upload another file
    if (_uploader) {
        _uploader.reset();
    }
}
function KPIPropertyNameArray() {
    // With Object.keys we create an array of properties of the KPIDTO object
    var _propertyNameArray = Object.keys(GetKPIDTO());
    // We filter the properties of the array that we are not going to use ID and IsActive note: in normal catalogs it is necessary up to this point
    // In this case of the KPI we will change the name of DashboardCategoryID to Category
    // this so that it matches the names of the excel column
    _propertyNameArray = _propertyNameArray.filter(PropertyName => PropertyName !== "ID" && PropertyName !== "IsActive")
        .map(PropertyName => PropertyName.includes("DashboardCategoryID") ? "Category" : PropertyName);
    return _propertyNameArray;
}
//#endregion

//#endregion

//#region KPI CRUD Functions
async function CreateKPI_Global() {
    await dxLoadPanel.show();
    const _KPIDTO = GetKPIDTO();
    const _validation_ResultDTO = await CreateKPI(_KPIDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxKPIGrid").dxDataGrid("instance").refresh();
        ClearKPIFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateKPI_Global() {
    await dxLoadPanel.show();
    const _KPIDTO = GetKPIDTO();
    const _validation_ResultDTO = await UpdateKPI(_KPIDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxKPIGrid").dxDataGrid("instance").refresh();
        ClearKPIFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteKPI_Global() {
    await dxLoadPanel.show();
    const _KPIDTO = GetKPIDTO();
    const _validation_ResultDTO = await DeleteKPI(_KPIDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxKPIGrid").dxDataGrid("instance").refresh();
        ClearKPIFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearKPIFields();
    dxLoadPanel.hide();
}