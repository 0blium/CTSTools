import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { GetDXDashboardMetricDataSource, CreateDashboardMetric, UpdateDashboardMetricOrder, DeleteDashboardMetric, GetDashboardMetricInformation, CreateDashboardMetricFromMetricList } from './DashboardMetric/DashboardMetric_Service.js';

import { GetDXDashboardDataSource, GetDashboardInformation } from './Dashboard/Dashboard_Service.js'
import { GetDXDashboardCategoryDataSource } from '../Settings/DashboardCategory/DashboardCategory_Service.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js'
import { GetDXMetricDataSource } from '../KPIManagement/Metric/Metric_Service.js'


document.addEventListener("DOMContentLoaded", async function () {
    await InitializeTemplateAdministrationControls();
    document.getElementById('AddMetricButton').addEventListener('click', CreateDashboardMetric_Global);
    document.getElementById('AddMetricBtn').addEventListener('click', function () {
            $('#AddMetricsModal').modal('show');        
    }); 
    document.getElementById('XBtnModal').addEventListener('click', ClearDashboardMetricFields);
    document.getElementById('CloseBtnModal').addEventListener('click', ClearDashboardMetricFields);
    await GetDashboardIDByURL();
});


async function InitializeTemplateAdministrationControls() {
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid({
        dataSource: await GetDXMetricDataSource({IsActive:true}),
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        showBorders: true,
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        columnMinWidth: 250,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        hoverStateEnabled: true,
        remoteOperations: true,
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 15,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100, 200],
            showInfo: true
        },
        searchPanel: {
            visible: false,
            highlightCaseSensitive: true,
        },
        grouping: {
            autoExpandAll: true,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        groupPanel: {
            visible: true,

        },
        selection: {
            mode: 'multiple',
            selectAllMode: 'page',
            showCheckBoxesMode: 'always'
        },
        columns: [
            {
                dataField: 'Name',
                caption: 'Metric',
            },
            {
                dataField: 'DashboardCategoryName',
                caption: 'Category',
                groupIndex: 0,
            },
            {
                dataField: 'OwnerDepartmentName',
                caption: 'Owner Department',
            },
            {
                dataField: 'ResponsibleDepartmentName',
                caption: 'Responsible Department'
            },
            {
                dataField: 'UnitOfMeasureName',
                caption: 'Unit of measure'
            },
            {
                dataField: 'ValueTypeName',
                caption: 'Value Type'
            },
            {
                dataField: 'Goal',
                caption: 'Goal'
            },
            {
                dataField: 'GoalRangeValue',
                caption: 'Goal Range'
            },
            {
                dataField: 'CalculationTypeName',
                caption: 'Calculation Type'
            }
        ]
    });
    $("#dxQualityMetrics").dxDataGrid({
        dataSource: [],
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        showBorders: true,
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        columnMinWidth: 130,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        hoverStateEnabled: true,
        remoteOperations: true,
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 10,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100, 200],
            showInfo: true
        },
        searchPanel: {
            visible: false,
            highlightCaseSensitive: true,
        },
        grouping: {
            autoExpandAll: true,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        groupPanel: {
            visible: true,

        },
        rowDragging: {
            allowReordering: true,
            dropFeedbackMode: 'push',
            async onReorder(e) {
                debugger;
                let visibleRows = e.component.getVisibleRows();
                // Filter only rows with rowType: 'data'
                let dataRows = visibleRows.filter(row => row.rowType === 'data');
                // Adjust the index to work only with rows of type 'data'
                let adjustedIndex = dataRows.findIndex(row => row === visibleRows[e.toIndex]);
                // Check if the index is valid
                if (adjustedIndex !== -1) {
                    // Get the data of the row of type 'data' that corresponds to the adjusted index
                    let newOrderStructureDTO = dataRows[adjustedIndex].data;
                    var _data = e.itemData;
                    // Change the origin Order to the destination Order
                    _data.Order = newOrderStructureDTO.Order;
                    UpdateDashboardMetricOrder_Global(_data);
                }
            },
        },

        columns: [
            {
                caption: "Delete",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                width: "auto",
                cellTemplate: function (container, options) {
                    container.height(30);
                    $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                        'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                        + '</span></button>')
                        .height(30)
                        .on('dxclick', function () {
                            $("#hiddenDashboardMetricID").val(options.data.ID);
                            ShowDashboardMetricDeleteQuestion(options.data);
                        }).appendTo(container);
                },
            },
            {
                dataField: 'Order',
                caption: 'Order',
                sortIndex: 0,
                sortOrder: "asc",
                width: 100,
            },
            {
                dataField: 'MetricDTO.Name',
                caption: 'KPI',
            },
            {
                dataField: 'MetricDTO.Description',
                caption: 'Description',
            },
            {
                dataField: 'MetricDTO.ValueTypeName',
                caption: 'Value Type',
            },
            {
                dataField: 'MetricDTO.OwnerName',
                caption: 'KPI Owner',
            },
            {
                dataField: 'MetricDTO.OwnerDepartmentName',
                caption: 'Department',
            },
            {
                dataField: 'MetricDTO.ResponsibleName',
                caption: 'Responsible',
            },
            {
                dataField: 'DashboardCategoryName',
                caption: 'Category',
            },
            {
                dataField: 'MetricDTO.ResponsibleDepartmentName',
                caption: 'Responsible Department',
            },
            {
                dataField: 'MetricDTO.UnitOfMeasureName',
                caption: 'Unit Of Measure',
            },

            {
                dataField: 'MetricDTO.Goal',
                caption: 'Goal',
            },
            {
                dataField: 'MetricDTO.GoalRangeValue',
                caption: 'Goal Range',
            },
            {
                dataField: 'MetricDTO.FacilityName',
                caption: 'Facility',
            },
            {
                dataField: 'MetricDTO.EquivalenceName',
                caption: 'Equivalence',
            },
            {
                dataField: 'MetricDTO.LastUpdateByName',
                caption: 'Last Update By',
            },
            {
                dataField: 'MetricDTO.LastUpdate',
                caption: 'Last Update Date',
            }

        ]
    });

}
async function GetDashboardIDByURL() {
    let _dashboardID = GetURLParameter("DashboardID");
    let DashboardDTO = await GetDashboardInformation({ ID: _dashboardID })
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {   
        await GetDashboardMetricList(DashboardDTO);
    }
    dxLoadPanel.hide();
}
function AssignDashboardMetricOrder(DashboardMetricDTO) {
    document.getElementById("hiddenDashboardMetricID").value = DashboardMetricDTO.ID;
    document.getElementById("hiddenDashboardID").value = DashboardMetricDTO.DashboardID;
    document.getElementById("hiddenMetricID").value = DashboardMetricDTO.MetricID;
    document.getElementById("hiddenDashboardCategoryID").value = DashboardMetricDTO.DashboardCategoryID;

}
function GetDashboardMetricDTO(DashboardDTO) {
    let _dashboardMetricDTO = {
        DashboardID: GetURLParameter("DashboardID"),
        MetricIDArray: ($("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.ID),
        DashboardCategoryIDArray: ($("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.DashboardCategoryID),
        GetMetricDTO: true,
        GetDashboardDTO: true,
        GetDashboardCategoryDTO: true,
        IsActive: true,
    }
    return _dashboardMetricDTO;
}
function ClearDashboardMetricFields() {
    let keys = $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").refresh();
}
async function ShowDashboardMetricDeleteQuestion(DashboardMetricDTO) {
    const _alert = await Swal.fire({
        text: 'You will remove this metric from current dashboard, are you sure?',
        title: 'Warning',
        confirmButtonText: `Delete`,
        icon: 'warning',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _dashboardMetricDTO = DashboardMetricDTO;
        DeleteDashboardMetric_Global(_dashboardMetricDTO);
    }
}


//#region CRUD Functions
async function CreateDashboardMetric_Global() {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = GetDashboardMetricDTO();
    const _validation_ResultDTO = await CreateDashboardMetricFromMetricList(_dashboardMetricDTO);
    //Poner aqui funcion que va a recargar las metricas mostradas en pantalla
    if (_validation_ResultDTO.Result) {
        $('#AddMetricsModal').modal('hide');
    }
    ClearDashboardMetricFields();
    //GetDashboardMetricList();
    HostResponse(_validation_ResultDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    dxLoadPanel.hide();
}
async function DeleteDashboardMetric_Global(DashboardMetricDTO) {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = DashboardMetricDTO;
    const _validation_ResultDTO = await DeleteDashboardMetric(_dashboardMetricDTO);
    if (_validation_ResultDTO.Result) {
        document.getElementById("hiddenDashboardMetricID").value = 0;       
        //await GetDashboardMetricList();
    }
    HostResponse(_validation_ResultDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    dxLoadPanel.hide();
}
//#endregion

//#region Business Logic functions
async function GetDashboardMetricList(DashboardDTO) {
    await dxLoadPanel.show();
    document.getElementById("dashboardtitle").innerHTML = DashboardDTO[0].Name;
    const _dashboardMetricDTO = GetDashboardMetricDTO(DashboardDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").option("dataSource", await GetDXDashboardMetricDataSource(_dashboardMetricDTO));
    document.getElementById('hiddenDashboardID').value = DashboardDTO.DashboardID;
    document.getElementById('AddMetricBtn').classList.remove('disabled');
    dxLoadPanel.hide();
}
//#endregion


// #region Change Order Functions
function GetDashboardMetricOrderDTO(Data) {
    let _dashboardMetricDTO = {
        ID: Data.ID,
        DashboardID: Data.DashboardID,
        MetricID: Data.MetricID,
        DashboardCategoryID: Data.DashboardCategoryID,
        Order: Data.Order
    }
    return _dashboardMetricDTO;
}
async function UpdateDashboardMetricOrder_Global(Data) {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardMetricOrderDTO(Data);
    const _validation_ResultDTO = await UpdateDashboardMetricOrder(_dashboardDTO);
    HostResponse(_validation_ResultDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    }   
    dxLoadPanel.hide();
}

//#endregion