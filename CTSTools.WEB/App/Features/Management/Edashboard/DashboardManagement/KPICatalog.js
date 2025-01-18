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
    $("#dxKPIGoalRangeSelectBox").dxSelectBox({
        dataSource: await GetDXGoalRangeDataSource({ IsActive: true }),
        displayExpr: "Value",
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
                    _fileDTO = {
                        FileName: filename,
                        Data: base64File
                    };
                    await dxLoadPanel.show();
                    _validationResultDTO = await CreateMassiveKPI(_fileDTO);
                    ShowKPIValidationResults(_validationResultDTO);
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
    //$("#dxKPISharedCheckBox").dxCheckBox("instance").option("value", data.Shared);
    $("#dxKPIGoalRangeSelectBox").dxSelectBox("instance").option("value", data.GoalRangeID);
    $("#dxKPIFacilitySelectBox").dxSelectBox("instance").option("value", data.FacilityID);
    $("#dxKPIOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerID);
    $("#dxKPIOwnerDepartmentSelectBox").dxSelectBox("instance").option("value", data.OwnerDepartmentID);
    $("#dxKPIResponsibleSelectBox").dxSelectBox("instance").option("value", data.ResponsibleID);
    //$("#dxKPIResponsibleSelectBox").dxSelectBox("instance").option("value", data.ResponsibleID);
    $("#dxKPIResponsibleDepartmentSelectBox").dxSelectBox("instance").option("value", data.ResponsibleDepartmentID);
    $("#dxKPIEquivalenceSelectBox").dxSelectBox("instance").option("value", data.EquivalenceID)
    //  //$("#dxKPIStatusSelectBox").dxSelectBox("instance").option("value", data.StatusID);
    //$("#dxKPIIsParentCheckBox").dxCheckBox("instance").option("value", data.IsParent);
    //$("#dxKPICalculationTypeSelectBox").dxSelectBox("instance").option("value", data.CalculationTypeID);
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
    $("#dxKPIGoalRangeSelectBox").dxSelectBox("instance").reset();
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
function ClearExcelModalFields() {
    $('#successKPIMessage').hide();
    $('#errorKPIMessages').hide();
    var uploader = $("#dxKPIFileUploader").dxFileUploader("instance");
    if (uploader) {
        uploader.option("visible", true);
    }
    if (uploader) {
        uploader.reset();
    }
}
function ShowKPIValidationResults(_validationResultDTO) {
    let successMessage = '';
    let errorMessages = '';
    if (_validationResultDTO.Data != null)
    {
        if (_validationResultDTO.Data.KPIGoodLinesList.length > 0) {
            successMessage = `KPI were created successfully.`;
            $('#successKPIMessage').text(successMessage).show();
            document.getElementById('successKPIMessage').removeAttribute('hidden');
        }
        if (_validationResultDTO.Data.KPIBadLinesList.length > 0) {
            errorMessages = '<strong>Wrong data:</strong><ul>';
            _validationResultDTO.Data.KPIBadLinesList.forEach(function (badLine) {
                errorMessages += `<li>Row error:<br>Name: ${badLine.Name}. Description = ${badLine.Description}. Unit Of Measure = ${badLine.UnitOfMeasureName}. Value Type = ${badLine.ValueTypeName}. Goal = ${badLine.Goal}. Owner = ${badLine.OwnerName}. Responsible = ${badLine.ResponsibleName}. Goal Range = ${badLine.GoalRangeValue}. Facility = ${badLine.FacilityName}. Equivalence = ${badLine.EquivalenceName}. Category = ${badLine.DashboardCategoryName}. Owner Department = ${badLine.OwnerDepartmentName}. Responsible Department = ${badLine.ResponsibleDepartmentName}. Is Active = ${badLine.IsActive}.</li>`;
            });
            errorMessages += '</ul>';
            $('#errorKPIMessages').html(errorMessages).show();
            document.getElementById('errorKPIMessages').removeAttribute('hidden');
        }
    }
    if (_validationResultDTO.Message == "Error") {
        errorMessages = `<li><strong>Column error:</strong><br>${_validationResultDTO.Description}</li>`;
        $('#errorKPIMessages').html(errorMessages).show();
        document.getElementById('errorKPIMessages').removeAttribute('hidden');
    }
    if (_validationResultDTO.Message == "Don't have access to this action.") {
        $('#UploadExcelKPIModal').modal('hide');
        ClearExcelModalFields();
        return HostResponse(_validationResultDTO);
    }
    $("#dxKPIFileUploader").dxFileUploader("instance").option("visible", false);
    $("#dxKPIGrid").dxDataGrid("instance").refresh();
    ClearKPIFields();
}
function GetKPIDTO() {
    let _KPIDTO = {
        ID: $("#hiddenKPIID").val(),
        StatusID: $("#hiddenStatusID").val(),
        Name: $("#dxKPINameTextBox").dxTextBox("instance").option("value"),
        Goal: $("#dxKPIGoalNumberBox").dxNumberBox("instance").option("value"),
        Description: $("#dxKPIDescriptionTextArea").dxTextArea("instance").option("value"),
        UnitOfMeasureID: $("#dxKPIUnitOfMeasureSelectBox").dxSelectBox("instance").option("value"),
        DashboardCategoryID: $("#dxKPICategorySelectBox").dxSelectBox("instance").option("value"),
        ValueTypeID: $("#dxKPIValueTypeSelectBox").dxSelectBox("instance").option("value"),
        //Shared: $("#dxKPISharedCheckBox").dxCheckBox("instance").option("value"),
        GoalRangeID: $("#dxKPIGoalRangeSelectBox").dxSelectBox("instance").option("value"),
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