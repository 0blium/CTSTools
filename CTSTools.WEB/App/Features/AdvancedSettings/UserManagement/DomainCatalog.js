import { GetDXDomainDataSource, CreateDomain, UpdateDomain, DeleteDomain } from './Domain/Domain_Service.js'
import { GetDXFacilityDataSource } from '../../AdvancedSettings/LocationManagement/Facility/Facility_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js';


//#region Domain Behavior Functions
document.addEventListener("DOMContentLoaded", () => {
    InitializeDomainCatalogControls();
});
async function InitializeDomainCatalogControls() {
    $("#dxDomainFacilityLookup").dxLookup({
        dataSource: await GetFacilityDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnable: true
    });
    $("#dxDomainIPTextBox").dxTextBox({
        placeholder: 'Type IP...'
    });
    $("#dxDomainDescriptionTextArea").dxTextArea({
        placeholder: 'Type description...'
    });
    $("#dxDomainIsActiveCheckBox").dxCheckBox({
        value: true
    });
    $("#dxDomainGrid").dxDataGrid({
        dataSource: await GetDXDomainDataSource(),
        remoteOperations: true,
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
            fileName: "DomainCatalog",
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
            let _domainData = data.selectedRowsData[0];
            if (_domainData != null) {
                DomainActionButtons("Update");
                PopulateDomainFields(_domainData);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#DomainModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenDomainID").val(options.data.ID);

                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenDomainID").val(options.data.ID);
                                ShowDomainDeleteQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "IP", dataField: "IP" },
                { caption: "Facility", dataField: "FacilityName" },
                { caption: "FacilityID", dataField: "FacilityID", visible: false },
                { caption: "Description", dataField: "Description" },
                { caption: "Added Date", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "Last Update", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Is Active", dataField: "IsActive" }
            ],
    });
    DomainActionButtons("Save");
}
async function PopulateDomainFields(DomainDTO) {
    $("#hiddenDomainID").val(DomainDTO.ID);
    $("#dxDomainFacilityLookup").dxLookup("instance").option("value", DomainDTO.FacilityID);
    $("#dxDomainDescriptionTextArea").dxTextArea("instance").option("value",DomainDTO.Description);
    $("#dxDomainIsActiveCheckBox").dxCheckBox("instance").option("value", DomainDTO.IsActive);
    $("#dxDomainIPTextBox").dxTextBox("instance").option("value", DomainDTO.IP);
}
async function ShowDomainDeleteQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove this domain, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        DeleteDomain_Global();
    } else {
        ClearDomainFields();
    }
}
function DomainActionButtons(Action) {
    $("#DomainActionButtons").empty();
    document.getElementById("DomainModalCloseButton").addEventListener("click", ClearDomainFields);

    if (Action == "Save") {
        document.getElementById("DomainActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateDomainButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateDomainButton").addEventListener("click", CreateDomain_Global);
    }
    else {
        // Update
        document.getElementById("DomainActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success me-1 m-b-15 float-end" id="UpdateDomainButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateDomainButton").addEventListener("click", UpdateDomain_Global);
    }
}
function ClearDomainFields() {
    DomainActionButtons("Save");
    $("#hiddenDomainID").val("");
    $("#dxDomainIPTextBox").dxTextBox("instance").reset();
    $("#dxDomainFacilityLookup").dxLookup("instance").reset();
    $("#dxDomainDescriptionTextArea").dxTextArea("instance").reset();
    let keys = $("#dxDomainGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDomainGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDomainIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    ClearErrorFeedback();
}
function GetDomainDTO() {
    let _domainDTO = {
        ID: $("#hiddenDomainID").val(),
        IP: $("#dxDomainIPTextBox").dxTextBox("instance").option("value"),
        Description: $("#dxDomainDescriptionTextArea").dxTextArea("instance").option("value"),
        FacilityID: $("#dxDomainFacilityLookup").dxLookup("instance").option("value"),
        IsActive: $("#dxDomainIsActiveCheckBox").dxCheckBox("instance").option("value"),
    }
    return _domainDTO;
}


//#region Domain CRUD Functions
async function CreateDomain_Global() {
    await dxLoadPanel.show();
    const _domainDTO = GetDomainDTO();
    const _validation_resultDTO = await CreateDomain(_domainDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDomainGrid").dxDataGrid("instance").refresh();
        ClearDomainFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateDomain_Global() {
    await dxLoadPanel.show();
    const _domainDTO = GetDomainDTO();
    const _validation_resultDTO = await UpdateDomain(_domainDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDomainGrid").dxDataGrid("instance").refresh();
        ClearDomainFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function DeleteDomain_Global() {
    await dxLoadPanel.show();
    const _domainDTO = GetDomainDTO();
    const _validation_resultDTO = await DeleteDomain(_domainDTO)
    if (_validation_resultDTO.Result) {
        $("#dxDomainGrid").dxDataGrid("instance").refresh();
        ClearDomainFields();
    }
    HostResponse(_validation_resultDTO);
    ClearDomainFields();
    dxLoadPanel.hide();
}
async function GetFacilityDataSource_Global() {
    let _facilityDTO = {
        IsActive: true
    }
    return await GetDXFacilityDataSource(_facilityDTO)
}


//#endregion