import { GetDXMetricDataSource, CreateMetric, UpdateMetric, DeleteMetric } from './Metric/Metric_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../../Common/Components/dxLoadPanel.js'
import { GetDXDepartmentDataSource } from '../../../../AdvancedSettings/LocationManagement/Department/Department_Service.js'
import { GetDXFacilityDataSource } from '../../../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { GetDXUserDataSource } from '../../../../AdvancedSettings/UserManagement/User/User_Service.js'
import { GetDXUnitOfMeasureDataSource } from '../../Settings/KPISettings/UnitOfMeasure/UnitOfMeasure_Service.js'
import { GetDXValueTypeDataSource } from '../../Settings/KPISettings/ValueType/ValueType_Service.js'
import { GetDXGoalRangeDataSource } from '../../Settings/KPISettings/GoalRange/GoalRange_Service.js'
import { GetDXEquivalenceDataSource } from '../../Settings/KPISettings/Equivalence/Equivalence_Service.js'

//#region Metric Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeMetricCatalogControls();
});
async function InitializeMetricCatalogControls() {
    $("#dxMetricNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxMetricDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...',
        height: 110
    });
    $("#dxMetricGoalNumberBox").dxNumberBox({
        min: 0,
        placeholder: "Enter the goal",
        format: "#,##0.##",
    });
    $("#dxMetricUnitOfMeasureSelectBox").dxSelectBox({
        dataSource: await GetDXUnitOfMeasureDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricOwnerSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricOwnerDepartmentSelectBox").dxSelectBox({
        dataSource: await GetDXDepartmentDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricResponsibleSelectBox").dxSelectBox({
        dataSource: await GetDXUserDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricResponsibleDepartmentSelectBox").dxSelectBox({
        dataSource: await GetDXDepartmentDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricValueTypeSelectBox").dxSelectBox({
        dataSource: await GetDXValueTypeDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricGoalRangeSelectBox").dxSelectBox({
        dataSource: await GetDXGoalRangeDataSource({ IsActive: true }),
        displayExpr: "Value",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricFacilitySelectBox").dxSelectBox({
        dataSource: await GetDXFacilityDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });
    $("#dxMetricEquivalenceSelectBox").dxSelectBox({
        dataSource: await GetDXEquivalenceDataSource({ IsActive: true }),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true,
        popupWidth: 450,
    });

    //$("#dxMetricCalculationTypeSelectBox").dxSelectBox({
    //    dataSource: GetDXCalculationTypeDataSource({ IsActive: true }),
    //    displayExpr: "Name",
    //    valueExpr: "ID",
    //    searchEnable: true,
    //    popupWidth: 450,
    //});
    $("#dxMetricIsActiveCheckBox").dxCheckBox({
        value: true,
        visible: false
    });

    $("#dxMetricGrid").dxDataGrid({
        dataSource: await GetDXMetricDataSource({ IsActive: true }),
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
            let _metricData = data.selectedRowsData[0];
            if (_metricData != null) {
                MetricActionButtons("Update");
                PopulateMetricFields(_metricData);
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
                                    document.getElementById('hiddenMetricID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Unit Of Measure", dataField: "UnitOfMeasureDTO.Name" },
                { caption: "Value Type", dataField: "ValueTypeDTO.Name" },
                { caption: "Goal", dataField: "Goal" },
                { caption: "Owner", dataField: "OwnerDTO.Name" },
                { caption: "Responsible", dataField: "ResponsibleDTO.Name" },
                //{ caption: "Shared", dataField: "Shared",  },
                { caption: "Goal Range", dataField: "GoalRangeDTO.Value" },
                { caption: "Facility", dataField: "FacilityDTO.Name" },
                { caption: "Equivalence", dataField: "EquivalenceDTO.Name" },
                { caption: "Owner Department", dataField: "OwnerDepartmentDTO.Name" },
                { caption: "Responsible Department", dataField: "ResponsibleDepartmentDTO.Name" },
                { caption: "Status", dataField: "StatusDTO.Name" },
                //{ caption: "Is Parent", dataField: "IsParent",  },
                //{ caption: "Calculation Type", dataField: "CalculationTypeDTO.Name",  },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    document.getElementById("btnCloseKPICategoryModal").addEventListener("click", ClearMetricFields);
    MetricActionButtons("Save");
}
async function PopulateMetricFields(data) {
    $("#hiddenMetricID").val(data.ID);
    $("#hiddenStatusID").val(data.StatusDTO.ID);
    $("#dxMetricNameTextBox").dxTextBox("instance").option("value", data.Name);
    $("#dxMetricGoalNumberBox").dxNumberBox("instance").option("value", data.Goal);
    $("#dxMetricDescriptionTextArea").dxTextArea("instance").option("value", data.Description);
    $("#dxMetricUnitOfMeasureSelectBox").dxSelectBox("instance").option("value", data.UnitOfMeasureDTO.ID);
    $("#dxMetricValueTypeSelectBox").dxSelectBox("instance").option("value", data.ValueTypeDTO.ID);
    //$("#dxMetricSharedCheckBox").dxCheckBox("instance").option("value", data.Shared);
    $("#dxMetricGoalRangeSelectBox").dxSelectBox("instance").option("value", data.GoalRangeDTO.ID);
    $("#dxMetricFacilitySelectBox").dxSelectBox("instance").option("value", data.FacilityDTO.ID);
    $("#dxMetricOwnerSelectBox").dxSelectBox("instance").option("value", data.OwnerDTO.ID);
    $("#dxMetricOwnerDepartmentSelectBox").dxSelectBox("instance").option("value", data.OwnerDepartmentDTO.ID);
    $("#dxMetricResponsibleSelectBox").dxSelectBox("instance").option("value", data.ResponsibleDTO.ID);
    $("#dxMetricResponsibleSelectBox").dxSelectBox("instance").option("value", data.ResponsibleDTO.ID);
    $("#dxMetricResponsibleDepartmentSelectBox").dxSelectBox("instance").option("value", data.ResponsibleDepartmentDTO.ID);
    $("#dxMetricEquivalenceSelectBox").dxSelectBox("instance").option("value", data.EquivalenceDTO.ID)
    //  //$("#dxMetricStatusSelectBox").dxSelectBox("instance").option("value", data.StatusDTO.ID);
    //$("#dxMetricIsParentCheckBox").dxCheckBox("instance").option("value", data.IsParent);
    //$("#dxMetricCalculationTypeSelectBox").dxSelectBox("instance").option("value", data.CalculationTypeDTO.ID);
    $("#dxMetricIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);

}
async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the metric, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteMetric_Global();
    } else {
        ClearMetricFields();
    }

}
function MetricActionButtons(Action) {
    $("#MetricActionButtons").empty();
    document.getElementById('KPICategoryModalTitle').innerText = '';
    if (Action == "Save") {
        document.getElementById("NewKPICategoryBtn").addEventListener("click", ClearMetricFields);
        document.getElementById('KPICategoryModalTitle').innerText = 'Add KPI Category'
        document.getElementById("MetricActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateMetricButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearMetricFields);
        document.getElementById("CreateMetricButton").addEventListener("click", CreateMetric_Global);
    }
    else {
        // Update
        document.getElementById('KPICategoryModalTitle').innerText = 'Update KPI Category'
        document.getElementById("MetricActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateMetricButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearMetricButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearMetricButton").addEventListener("click", ClearMetricFields);
        document.getElementById("UpdateMetricButton").addEventListener("click", UpdateMetric_Global);
    }
}
function ClearMetricFields() {
    $('#SaveKPICategoryRecordModal').modal('hide');
    MetricActionButtons("Save");
    $("#hiddenMetricID").val("");
    $("#hiddenStatusID").val("");
    $("#dxMetricNameTextBox").dxTextBox("instance").option("value", "");
    $("#dxMetricGoalNumberBox").dxNumberBox("instance").option("value", "0");
    $("#dxMetricDescriptionTextArea").dxTextArea("instance").option("value", "");
    $("#dxMetricUnitOfMeasureSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricValueTypeSelectBox").dxSelectBox("instance").reset();
    //$("#dxMetricSharedCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxMetricGoalRangeSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricFacilitySelectBox").dxSelectBox("instance").reset();
    $("#dxMetricEquivalenceSelectBox").dxSelectBox("instance").reset();
    //$("#dxMetricStatusSelectBox").dxSelectBox("instance").reset();
    //$("#dxMetricIsParentCheckBox").dxCheckBox("instance").option("value", true);
    //$("#dxMetricCalculationTypeSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricOwnerDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricOwnerSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricResponsibleSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricResponsibleSelectBox").dxSelectBox("instance").reset()
    $("#dxMetricResponsibleDepartmentSelectBox").dxSelectBox("instance").reset();
    $("#dxMetricIsActiveCheckBox").dxCheckBox("instance").option("value", true);

    let keys = $("#dxMetricGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxMetricGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxMetricGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxMetricGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}
function GetMetricDTO() {
    let _metricDTO = {
        ID: $("#hiddenMetricID").val(),
        StatusDTO: { ID: $("#hiddenStatusID").val() },
        Name: $("#dxMetricNameTextBox").dxTextBox("instance").option("value"),
        Goal: $("#dxMetricGoalNumberBox").dxNumberBox("instance").option("value"),
        Description: $("#dxMetricDescriptionTextArea").dxTextArea("instance").option("value"),
        UnitOfMeasureDTO: { ID: $("#dxMetricUnitOfMeasureSelectBox").dxSelectBox("instance").option("value") },
        ValueTypeDTO: { ID: $("#dxMetricValueTypeSelectBox").dxSelectBox("instance").option("value") },
        //Shared: $("#dxMetricSharedCheckBox").dxCheckBox("instance").option("value"),
        GoalRangeDTO: { ID: $("#dxMetricGoalRangeSelectBox").dxSelectBox("instance").option("value") },
        FacilityDTO: { ID: $("#dxMetricFacilitySelectBox").dxSelectBox("instance").option("value") },
        EquivalenceDTO: { ID: $("#dxMetricEquivalenceSelectBox").dxSelectBox("instance").option("value") },
        //IsParent: $("#dxMetricIsParentCheckBox").dxCheckBox("instance").option("value"),
        //CalculationTypeDTO: { ID: $("#dxMetricCalculationTypeSelectBox").dxSelectBox("instance").option("value") },
        OwnerDTO: { ID: $("#dxMetricOwnerSelectBox").dxSelectBox("instance").option("value") },
        OwnerDepartmentDTO: { ID: $("#dxMetricOwnerDepartmentSelectBox").dxSelectBox("instance").option("value") },
        ResponsibleDTO: { ID: $("#dxMetricResponsibleSelectBox").dxSelectBox("instance").option("value") },
        ResponsibleDepartmentDTO: { ID: $("#dxMetricResponsibleDepartmentSelectBox").dxSelectBox("instance").option("value") },
        IsActive: $("#dxMetricIsActiveCheckBox").dxCheckBox("instance").option("value"),

    }
    return _metricDTO;
}
//#endregion

//#region Metric CRUD Functions
async function CreateMetric_Global() {
    await dxLoadPanel.show();
    const _metricDTO = GetMetricDTO();
    const _validation_ResultDTO = await CreateMetric(_metricDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxMetricGrid").dxDataGrid("instance").refresh();
        ClearMetricFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateMetric_Global() {
    await dxLoadPanel.show();
    const _metricDTO = GetMetricDTO();
    const _validation_ResultDTO = await UpdateMetric(_metricDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxMetricGrid").dxDataGrid("instance").refresh();
        ClearMetricFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteMetric_Global() {
    await dxLoadPanel.show();
    const _metricDTO = GetMetricDTO();
    const _validation_ResultDTO = await DeleteMetric(_metricDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxMetricGrid").dxDataGrid("instance").refresh();
        ClearMetricFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearMetricFields();
    dxLoadPanel.hide();
}