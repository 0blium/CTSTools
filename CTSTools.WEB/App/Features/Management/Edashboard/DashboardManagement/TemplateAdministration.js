import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { GetDXDashboardMetricDataSource, CreateDashboardMetric, UpdateDashboardMetricOrder, DeleteDashboardMetric,GetDashboardMetricInformation,CreateDashboardMetricFromMetricList } from '../TemplateAdministration/DashboardMetric/DashboardMetric_Service.js'

import { GetDXDashboardDataSource } from './Dashboard/Dashboard_Service.js'
import { GetDXDashboardCategoryDataSource } from '../Settings/DashboardCategory/DashboardCategory_Service.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js'
import { GetDXMetricDataSource } from '../KPIManagement/Metric/Metric_Service.js'


document.addEventListener("DOMContentLoaded", async function () {
    await InitializeTemplateAdministrationControls();
    document.getElementById('AddMetricButton').addEventListener('click', CreateDashboardMetric_Global);
    document.getElementById('AddMetricBtn').addEventListener('click', function () {
        if ($("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value") != null &&
            document.getElementById('hiddenDashboardID').value != 0) {
            $('#AddMetricsModal').modal('show');
        } else {
            toastr["error"]("Please, select a dashboard before adding new metrics", "Dashboard Not selected");
        }
    });
 
    document.getElementById('UpdateOrderMetricInformation').addEventListener('click', UpdateDashboardMetricOrder_Global);
    document.getElementById('XBtnModal').addEventListener('click', ClearDashboardMetricFields);
    document.getElementById('CloseBtnModal').addEventListener('click', ClearDashboardMetricFields);

    //await setTimeout(await GetDashboardIDByURL, 5000);
    await GetDashboardIDByURL();
});


async function InitializeTemplateAdministrationControls() {
    $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox({
        dataSource: await GetDXDashboardDataSource({IsActive:true}),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        readOnly:true,
        placeholder: "Select Dashboard",
        onSelectionChanged:async function () {
            await GetDashboardMetricList();
        }
    });
    $("#dxDashboardMetricNameTextBox").dxTextBox({
        placeholder: '',
        readOnly:true
    });
    $("#dxDashboardMetricOrderNumberBox").dxNumberBox({
        min: 0,
        placeholder: "Enter the Order",
        format: "#",
    });
    
    $("#dxDashboardMetric_DashboardCategorySelectBox").dxSelectBox({
        dataSource: await GetDXDashboardCategoryDataSource({ IsActive: true }),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        placeholder: "Select Dashboard"
    });
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
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
}
async function GetDashboardIDByURL() {
    let _dashboardID = GetURLParameter("DashboardID");
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {        
        await $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value", Number(_dashboardID));        
    } else {
        $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("readOnly", false)
    }
    dxLoadPanel.hide();
}

async function InitializeMetricListControls() {
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
           
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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
    $("#dxCostMetrics").dxDataGrid({
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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
    $("#dxDeliveryMetrics").dxDataGrid({
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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
    $("#dxSafetyMetrics").dxDataGrid({
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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
    $("#dxMoralMetrics").dxDataGrid({
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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
    $("#dxImprovementMetrics").dxDataGrid({
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
            autoExpandAll: false,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        columns: [
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
                                { text: "Edit Order", icon: "fa fa-pen-to-square text-info", value: 1 },
                                { text: "Delete KPI", icon: "fa fa-trash-alt text-danger", value: 2 },
                            ]
                        }],
                        showFirstSubmenuMode: 'onClick',
                        hideSubmenuOnMouseLeave: true,
                        onItemClick: function (e) {
                            if (e.itemData.value == 1) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                $("#DashboardUpdateModal").modal("show");
                                AssignDashboardMetricOrder(options.data);
                            }
                            else if (e.itemData.value == 2) {
                                $("#hiddenDashboardMetricID").val(options.data.ID);
                                ShowDashboardMetricDeleteQuestion(options.data);
                            }
                        },
                    });
                }
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


async function PopulateDashboardMetricList(DashboardMetricDTO) {
    //console.log(_dashboardMetricList);
    //dxLoadPanel.show();
    
    //document.getElementById('GetDashboardRevision').classList.remove('disabled');

    //Start to build Metric list from Dashboard
    await InitializeMetricListControls();
    //Quality
   
    DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Quality
    let _qualityDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_qualityDS, "Quality");
    //Cost
     DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Cost
    let _costDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_costDS, "Cost");
    //Delivery
   
    DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Delivery
    let _deliveryDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_deliveryDS, "Delivery");

    //Safety
    DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Safety
    let _safetyDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_safetyDS, "Safety");
    //Moral
    
    DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Moral
    let _moralDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_moralDS, "Moral")
    //Enviroment
   
    DashboardMetricDTO.DashboardCategoryID = Dashboard_Category_Enum.Improvement
    let _enviromentDS = await GetDXDashboardMetricDataSource(DashboardMetricDTO);
    await SetDataSourceForDashboardCategory(_enviromentDS, "Improvement")

    document.getElementById('hiddenDashboardID').value = $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value");
    document.getElementById('AddMetricBtn').classList.remove('disabled');
    document.getElementById('DashboardMetricList').classList.remove('d-none');
    //dxLoadPanel.hide();
}
function ReloadDashboardMetric() {
    $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    $("#dxCostMetrics").dxDataGrid("instance").refresh();
    $("#dxDeliveryMetrics").dxDataGrid("instance").refresh();
    $("#dxSafetyMetrics").dxDataGrid("instance").refresh();
    $("#dxMoralMetrics").dxDataGrid("instance").refresh();
    $("#dxImprovementMetrics").dxDataGrid("instance").refresh();
}
function AssignDashboardMetricOrder(DashboardMetricDTO) {
    document.getElementById("hiddenDashboardMetricID").value = DashboardMetricDTO.ID;
    $("#dxDashboardMetricOrderNumberBox").dxNumberBox("instance").option("value", DashboardMetricDTO.Order);
    //document.getElementById("DashboardMetricOrder").value = DashboardMetricDTO.Order;
    //document.getElementById("DashboardMetricName").value = DashboardMetricDTO.MetricName;
    $("#dxDashboardMetricNameTextBox").dxTextBox("instance").option("value", DashboardMetricDTO.MetricName);
    document.getElementById("hiddenDashboardID").value = DashboardMetricDTO.DashboardID;
    document.getElementById("hiddenMetricID").value = DashboardMetricDTO.MetricID;
    document.getElementById("hiddenDashboardCategoryID").value = DashboardMetricDTO.DashboardCategoryID;

}
async function SetDataSourceForDashboardCategory(Datasource, DashboardCategory) {
    
    await $("#dx" + DashboardCategory + "Metrics").dxDataGrid("instance").option("dataSource", Datasource);
    await $("#dx" + DashboardCategory + "Metrics").dxDataGrid("instance").refresh();
}
function GetDashboardMetricDTO() {
    let _dashboardMetricDTO = {
        DashboardID: $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value"),
        MetricIDArray: ($("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.ID),
        DashboardCategoryID: ($("#dxDashboardMetric_DashboardCategorySelectBox").dxSelectBox("instance").option("value") != null) ?
                $("#dxDashboardMetric_DashboardCategorySelectBox").dxSelectBox("instance").option("value") : 0,
        GetMetricDTO: true,
        GetDashboardDTO: true,
        GetDashboardCategoryDTO: true,
    }
    return _dashboardMetricDTO;
}
function ClearDashboardMetricFields() {
    $("#dxDashboardMetric_DashboardCategorySelectBox").dxSelectBox("instance").reset();
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
    ReloadDashboardMetric();
    dxLoadPanel.hide();
}
//async function UpdateDashboardMetric_Global(DashboardMetricDTO) {
//    await dxLoadPanel.show();
//    const _dashboardMetricDTO = DashboardMetricDTO;
//    const _validation_resultDTO = await DashboardMetric_Service.UpdateDashboardMetric(_dashboardMetricDTO);
//    //Poner aqui funcion que va a recargar las metricas mostradas en pantalla
//    Response.HostResponse(_validation_resultDTO);
//    ClearDashboardMetricFields();
//    dxLoadPanel.hide();
//}
async function DeleteDashboardMetric_Global(DashboardMetricDTO) {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = DashboardMetricDTO;
    const _validation_ResultDTO = await DeleteDashboardMetric(_dashboardMetricDTO);
    if (_validation_ResultDTO.Result) {
        document.getElementById("hiddenDashboardMetricID").value = 0;       
        //await GetDashboardMetricList();
    }
    HostResponse(_validation_ResultDTO);
    ReloadDashboardMetric();
    dxLoadPanel.hide();
}

//#endregion
//#region Business Logic functions
async function GetDashboardMetricList() {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = GetDashboardMetricDTO();
    //const _dashboardMetricList = await GetDashboardMetricInformation(_dashboardMetricDTO);
    //console.log(_dashboardMetricList);
    await PopulateDashboardMetricList(_dashboardMetricDTO);
    //GetDashboardRevision();
    dxLoadPanel.hide();
}
//#endregion

//#region revision functions
function GetDashboardDTO() {
    let _dashboardDTO = {
        ID: document.getElementById("hiddenDashboardID").value,
        //Revision: document.getElementById('DashboardRevision').value,
        Comment: document.getElementById('DashboardComment').value
    }
    return _dashboardDTO;
}
async function UpdateDashboardRevision_Global() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardDTO();
    const _validation_resultDTO = await UpdateDashboardRevision(_dashboardDTO);
    HostResponse(_validation_resultDTO);
    ClearDashboardRevisionFields();
    dxLoadPanel.hide();
}
function ClearDashboardRevisionFields() {
    document.getElementById('DashboardRevision').value = ""
    document.getElementById('DashboardComment').value = ""
    document.getElementById('DashboardCurrentRevision').value = ""
    document.getElementById('DashboardCurrentComment').value = ""
}
async function GetDashboardRevision() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardDTO();
    const _dashboardInformation = await GetDashboardList(_dashboardDTO);
    document.getElementById('DashboardCurrentRevision').value = _dashboardInformation[0].Revision;
    document.getElementById('DashboardCurrentComment').innerText = _dashboardInformation[0].Comment;
    dxLoadPanel.hide();
}
//#endregion

// #region Change Order Functions
function GetDashboardMetricOrderDTO() {
    let _dashboardMetricDTO = {
        ID: document.getElementById("hiddenDashboardMetricID").value,
        DashboardID: document.getElementById("hiddenDashboardID").value,
        MetricID: document.getElementById("hiddenMetricID").value,
        DashboardCategoryID: document.getElementById("hiddenDashboardCategoryID").value,
        Order: $("#dxDashboardMetricOrderNumberBox").dxNumberBox("instance").option("value") /*document.getElementById("DashboardMetricOrder").value*/
    }
    return _dashboardMetricDTO;
}

async function UpdateDashboardMetricOrder_Global() {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardMetricOrderDTO();
    const _validation_ResultDTO = await UpdateDashboardMetricOrder(_dashboardDTO);
    HostResponse(_validation_ResultDTO);
    if (_validation_ResultDTO.Result) {
        $("#DashboardUpdateModal").modal('hide');
        ReloadDashboardMetric();
    }
    //GetDashboardMetricList();
   
    dxLoadPanel.hide();
}

//#endregion