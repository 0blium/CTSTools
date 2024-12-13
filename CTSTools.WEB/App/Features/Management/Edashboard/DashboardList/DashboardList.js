import { GetDXDashboardDataSource} from '../Settings/Dashboard/Dashboard_Service.js'



document.addEventListener("DOMContentLoaded", () => {
    InitializeDashboardCatalogControls();
});

async function InitializeDashboardCatalogControls() {
    
    $("#dxDashboardGrid").dxDataGrid({
        dataSource: await GetDXDashboardDataSource({ IsActive: true, DepartmentDTO: { GetFacilityDTO: true }, GetDepartmentDTO: true }),
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
            fileName: "DashboardCatalog",
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
            let _dashboardData = data.selectedRowsData[0];
            if (_dashboardData != null) {
                DashboardActionButtons("Update");
                PopulateDashboardFields(_dashboardData);
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
                                    { text: "View Dashboard", icon: "fas fa-chart-line text-primary", value: 1 },
                                    { text: "View KPIs", icon: "fas fa-compass-drafting text-warning", value: 2 },
                                ]
                            }],
                            showFirstSubmenuMode: 'onClick',
                            hideSubmenuOnMouseLeave: true,
                            onItemClick: function (e) {
                                let _data = options.data;
                                if (e.itemData.value == 1) {
                                    window.open("/App/Features/Management/Edashboard/DashboardDataEntry/DashboardDataEntry.aspx?DashboardID=" + _data.ID)
                                }
                                else if (e.itemData.value == 2) {
                                    window.open("/App/Features/Management/Edashboard/TemplateAdministration/TemplateAdministration.aspx?DashboardID=" + _data.ID)
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Revision", dataField: "Revision" },
                { caption: "Owner", dataField: "OwnerDTO.Name" },
                { caption: "Department", dataField: "DepartmentDTO.Name" },
                { caption: "Level", dataField: "LevelDTO.Name" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
}

//#endregion
