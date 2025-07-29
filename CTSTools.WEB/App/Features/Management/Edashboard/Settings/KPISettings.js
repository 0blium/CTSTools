import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { Catalog_Enum } from "./Catalog_Enum.js"
import { GetDXEquivalenceDataSource, CreateEquivalence, UpdateEquivalence, DeleteEquivalence } from './Equivalence/Equivalence_Service.js'
import { GetDXCalculationTypeDataSource, CreateCalculationType, UpdateCalculationType, DeleteCalculationType } from './CalculationType/CalculationType_Service.js'
import { GetDXLevelDataSource, CreateLevel, UpdateLevel, DeleteLevel } from './Level/Level_Service.js'
import { GetDXValueTypeDataSource, CreateValueType, UpdateValueType, DeleteValueType } from './ValueType/ValueType_Service.js'
import { GetDXGoalRangeDataSource, CreateGoalRange, UpdateGoalRange, DeleteGoalRange } from './GoalRange/GoalRange_Service.js'


//#region DashboardCategory Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeDashboardCategoryCatalogControls();
    document.getElementById('NewCatalogItemBtn').classList.add('disabled');
});
let _catalogDatasource = [{ ID: Catalog_Enum.ValueType, Name: "Value Type" }, { ID: Catalog_Enum.CalculationType, Name: "Calculation Type" }, { ID: Catalog_Enum.Equivalence, Name: "Equivalence" },
{ ID: Catalog_Enum.Level, Name: "Level" }, { ID: Catalog_Enum.GoalRange, Name: "Goal Range" }];
let _lastCatalogSelected = 0;

let _DatagridConfiguration = {
    GenericCatalogVersion: {
        onSelectionChanged: function (data) {
            let _dashboardCategoryData = data.selectedRowsData[0];
            if (_dashboardCategoryData != null) {
                DashboardCategoryActionButtons("Update");
                PopulateCatalogItemFields(_dashboardCategoryData);
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
                                    $('#SaveDashboardCategoryRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenCatalogItemID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Name", dataField: "Name" },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By ID", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By ID", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ]
    },
    GoalRangeVersion: {
        onSelectionChanged: function (data) {
            let _dashboardCategoryData = data.selectedRowsData[0];
            if (_dashboardCategoryData != null) {
                DashboardCategoryActionButtons("Update");
                PopulateCatalogItemFields(_dashboardCategoryData);
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
                                    $('#SaveDashboardCategoryRecordModal').modal('show');
                                }
                                else if (e.itemData.value == 2) {
                                    document.getElementById('hiddenCatalogItemID').value = options.data.ID;
                                    ShowDeleteQuestion();
                                }
                            },
                        });
                    }
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                {
                    caption: "Value", dataField: "Value", format: {
                        type: "fixedPoint",
                        precision: 2 // número de decimales
                    }
                },
                { caption: "Description", dataField: "Description" },
                { caption: "Added By I D", dataField: "AddedByID", visible: false },
                { caption: "Added By Name", dataField: "AddedByName" },
                { caption: "Last Update By I D", dataField: "LastUpdateByID", visible: false },
                { caption: "Last Update By Name", dataField: "LastUpdateByName" },
                { caption: "Is Active", dataField: "IsActive" },
            ]
    }
}
async function InitializeDashboardCategoryCatalogControls() {

    $("#dxCatalogSelectBox").dxSelectBox({
        dataSource: _catalogDatasource,
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        placeholder: "Select Catalog",
        onSelectionChanged: async function (e) {
            await PopulateCatalogDataGrid();
            ShowGenericFields();
            if ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value") != null) {
                document.getElementById('NewCatalogItemBtn').classList.remove('disabled');
                document.getElementById('MessageAlert').classList.add('d-none');
            } else {
                document.getElementById('NewCatalogItemBtn').classList.add('disabled');
                document.getElementById('MessageAlert').classList.remove('d-none');
            }
        }
    });

    $("#dxGoalRangeValueNumberBox").dxNumberBox({
        min: 0,
        placeholder: "Enter the value",
        format: "#,##0.##",
    });
    $("#dxCatalogItemIsActiveCheckBox").dxCheckBox({
        value: true
    });

    $("#dxCatalogItemNameTextBox").dxTextBox({
        placeholder: 'Type name...'
    });
    $("#dxCatalogItemDescription").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxCatalogGrid").dxDataGrid({
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
            fileName: "SettingCatalog",
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
        }
    });
    document.getElementById("btnCloseDashboardCategoryModal").addEventListener("click", ClearGenericFields);
    DashboardCategoryActionButtons("Save");
}
async function PopulateCatalogDataGrid() {
    if (_lastCatalogSelected != $("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
        ClearGenericFields();
        let _datagridConfig = null
        if ([Catalog_Enum.CalculationType, Catalog_Enum.Equivalence, Catalog_Enum.Level, Catalog_Enum.ValueType].includes($("#dxCatalogSelectBox").dxSelectBox("instance").option("value"))) {
            _datagridConfig = _DatagridConfiguration.GenericCatalogVersion;
        } else if ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value") == Catalog_Enum.GoalRange) {
            _datagridConfig = _DatagridConfiguration.GoalRangeVersion;
        }
        if (_datagridConfig != null) {
            $("#dxCatalogGrid").dxDataGrid("instance").option("columns", _datagridConfig.columns)
            $("#dxCatalogGrid").dxDataGrid("instance").option("onSelectionChanged", _datagridConfig.onSelectionChanged)

            switch ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
                case Catalog_Enum.ValueType:
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", await GetDXValueTypeDataSource({}))
                    break;
                case Catalog_Enum.CalculationType:
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", await GetDXCalculationTypeDataSource({}))
                    break;
                case Catalog_Enum.Equivalence:
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", await GetDXEquivalenceDataSource({}))
                    break;
                case Catalog_Enum.Level:
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", await GetDXLevelDataSource({}))
                    break;
                case Catalog_Enum.GoalRange:
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", await GetDXGoalRangeDataSource({}))
                    break;
                default:
                    // code block
                    $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", [])
            }

        } else {
            $("#dxCatalogGrid").dxDataGrid("instance").option("dataSource", [])
        }


        _lastCatalogSelected = $("#dxCatalogSelectBox").dxSelectBox("instance").option("value")
    }
}


async function ShowDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the item, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteCatalogItem_Global();
    } else {
        ClearGenericFields();
    }
}
function DashboardCategoryActionButtons(Action) {
    $("#DashboardCategoryActionButtons").empty();
    document.getElementById('DashboardCategoryModalTitle').innerText = '';
    SetModalTitle(Action);
    if (Action == "Save") {
        document.getElementById("NewCatalogItemBtn").addEventListener("click", ClearGenericFields);
        document.getElementById("DashboardCategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="CreateDashboardCategoryButton" type="button">Save</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearGenericFields);
        document.getElementById("CreateDashboardCategoryButton").addEventListener("click", CreateCatalogItem_Global);
    }
    else {
        // Update
        document.getElementById("DashboardCategoryActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success float-end" id="UpdateDashboardCategoryButton" type="button">Update</button>' +
            '<button class="btn btn-secondary me-1 m-b-15 float-end" id="ClearDashboardCategoryButton" type="button">Cancel</button>' +
            '</div>';
        document.getElementById("ClearDashboardCategoryButton").addEventListener("click", ClearGenericFields);
        document.getElementById("UpdateDashboardCategoryButton").addEventListener("click", UpdateCatalogItem_Global);
    }
}



//#endregion


//#region Builds Generic Logic
function GetGenericDTO() {
    let _GenericDTO = {
        ID: $("#hiddenCatalogItemID").val(),
        Name: $("#dxCatalogItemNameTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxCatalogItemDescription").dxTextArea("instance").option("value"),
        IsActive: $("#dxCatalogItemIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    if ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value") != Catalog_Enum.GoalRange) {
        _GenericDTO.Name = $("#dxCatalogItemNameTextBox").dxTextBox("instance").option("value")
    } else {
        _GenericDTO.Value = $("#dxGoalRangeValueNumberBox").dxNumberBox("instance").option("value")
    }
    return _GenericDTO;
}
async function PopulateCatalogItemFields(data) {
    $("#hiddenCatalogItemID").val(data.ID);
    if ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value") != Catalog_Enum.GoalRange) {
        $("#dxCatalogItemNameTextBox").dxTextBox("instance").option("value", data.Name);
    } else {
        $("#dxGoalRangeValueNumberBox").dxNumberBox("instance").option("value", data.Value);
    }
    $("#dxCatalogItemDescription").dxTextArea("instance").option("value", data.Description);
    $("#dxCatalogItemIsActiveCheckBox").dxCheckBox("instance").option("value", data.IsActive);
}

function ClearGenericFields() {
    $('#SaveDashboardCategoryRecordModal').modal('hide');
    DashboardCategoryActionButtons("Save");
    $("#hiddenCatalogItemID").val("");
    $("#dxCatalogItemNameTextBox").dxTextBox("instance").option("value", '');
    $("#dxCatalogItemDescription").dxTextArea("instance").option("value", '');
    $("#dxCatalogItemIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    $("#dxGoalRangeValueNumberBox").dxNumberBox("instance").option("value", '0');
    let keys = $("#dxCatalogGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxCatalogGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxCatalogGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxCatalogGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
}

function ShowGenericFields() {
    if ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value") != Catalog_Enum.GoalRange) {
        document.getElementById('CatalogItemNameField').classList.remove('d-none');
        document.getElementById('GoalRangeValueField').classList.add('d-none');
    } else {
        document.getElementById('CatalogItemNameField').classList.add('d-none');
        document.getElementById('GoalRangeValueField').classList.remove('d-none');
    }

}

function SetModalTitle(Action) {
    let _modalTitle = Action == "Save" ? "Add" : "Update";
    switch ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
        case Catalog_Enum.ValueType:
            _modalTitle += " Value Type";
            break;
        case Catalog_Enum.CalculationType:
            _modalTitle += " Calculation Type";
            break;
        case Catalog_Enum.Equivalence:
            _modalTitle += " Equivalence";
            break;
        case Catalog_Enum.Level:
            _modalTitle += " Level";
            break;
        case Catalog_Enum.GoalRange:
            _modalTitle += " Goal Range";
            break;
    }
    document.getElementById('DashboardCategoryModalTitle').innerText = _modalTitle;
}

async function CreateCatalogItem(GenericDTO) {
    switch ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
        case Catalog_Enum.ValueType:
            return await CreateValueType(GenericDTO);
            break;
        case Catalog_Enum.CalculationType:
            return await CreateCalculationType(GenericDTO);
            break;
        case Catalog_Enum.Equivalence:
            return await CreateEquivalence(GenericDTO);
            break;
        case Catalog_Enum.Level:
            return await CreateLevel(GenericDTO);
            break;
        case Catalog_Enum.GoalRange:
            return await CreateGoalRange(GenericDTO);
            break;
    }
}


async function UpdateCatalogItem(GenericDTO) {
    switch ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
        case Catalog_Enum.ValueType:
            return await UpdateValueType(GenericDTO);
            break;
        case Catalog_Enum.CalculationType:
            return await UpdateCalculationType(GenericDTO);
            break;
        case Catalog_Enum.Equivalence:
            return await UpdateEquivalence(GenericDTO);
            break;
        case Catalog_Enum.Level:
            return await UpdateLevel(GenericDTO);
            break;
        case Catalog_Enum.GoalRange:
            return await UpdateGoalRange(GenericDTO);
            break;
    }
}
async function DeleteCatalogItem(GenericDTO) {
    switch ($("#dxCatalogSelectBox").dxSelectBox("instance").option("value")) {
        case Catalog_Enum.ValueType:
            return await DeleteValueType(GenericDTO);
            break;
        case Catalog_Enum.CalculationType:
            return await DeleteCalculationType(GenericDTO);
            break;
        case Catalog_Enum.Equivalence:
            return await DeleteEquivalence(GenericDTO);
            break;
        case Catalog_Enum.Level:
            return await DeleteLevel(GenericDTO);
            break;
        case Catalog_Enum.GoalRange:
            return await DeleteGoalRange(GenericDTO);
            break;
    }
}
//#endregion



//#region Generic CRUD Functions
async function CreateCatalogItem_Global() {
    await dxLoadPanel.show();
    const _genericDTO = GetGenericDTO();
    const _validation_ResultDTO = await CreateCatalogItem(_genericDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCatalogGrid").dxDataGrid("instance").refresh();
        ClearGenericFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function UpdateCatalogItem_Global() {
    await dxLoadPanel.show();
    const _genericDTO = GetGenericDTO();
    const _validation_ResultDTO = await UpdateCatalogItem(_genericDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCatalogGrid").dxDataGrid("instance").refresh();
        ClearGenericFields();
    }
    HostResponse(_validation_ResultDTO);
    dxLoadPanel.hide();
}
async function DeleteCatalogItem_Global() {
    await dxLoadPanel.show();
    const _genericDTO = GetGenericDTO();
    const _validation_ResultDTO = await DeleteCatalogItem(_genericDTO)
    if (_validation_ResultDTO.Result) {
        $("#dxCatalogGrid").dxDataGrid("instance").refresh();
        ClearGenericFields();
    }
    HostResponse(_validation_ResultDTO);
    ClearGenericFields();
    dxLoadPanel.hide();
}
//#endregion