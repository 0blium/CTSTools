import { CreateAttribute, UpdateAttribute, DeleteAttribute, GetDXAttributeDataSource } from './Attribute/Attribute_Service.js';
import { CreateValue, UpdateValue, DeleteValue, GetDXValueDataSource } from './Value/Value_Service.js'
import { CreateMultipleValueLink, UpdateValueLink, DeleteValueLink, GetDXValueLinkDataSource } from './ValueLink/ValueLink_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';



//#region Attribute Catalog
document.addEventListener("DOMContentLoaded", () => {
    InitializeValueLinkerControls();
    document.getElementById("ChildValueGroup").hidden = true;

});
async function InitializeValueLinkerControls() {

    $("#dxParentAttributeLookup").dxLookup({
        dataSource: await GetDXAttributeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {               
                $("#dxParentValueLookup").dxLookup("instance").reset();
                await $("#dxParentValueLookup").dxLookup("instance").option("dataSource", await GetDXParentValue_Global());
            }
            else {
                $("#dxParentValueLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxParentValueLookup").dxLookup({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        deferRendering: false,
        searchEnabled: true,
    });
    $("#dxChildAttributeLookup").dxLookup({
        dataSource: await GetDXAttributeDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                $("#dxChildValueLookup").dxLookup("instance").reset();
                $("#dxChildValueTagBox").dxTagBox("instance").reset();
                await $("#dxChildValueLookup").dxLookup("instance").option("dataSource", await GetDXChildValue_Global());
                await $("#dxChildValueTagBox").dxTagBox("instance").option("dataSource", await GetDXChildValue_Global());
            }
            else {
                $("#dxChildValueLookup").dxLookup("instance").option("dataSource", []);
                $("#dxChildValueTagBox").dxTagBox("instance").option("dataSource", []);
            }
        }
    });
    $("#dxChildValueLookup").dxLookup({
        dataSource: [],
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,

    });
    $("#dxChildValueTagBox").dxTagBox({
        dataSource: await GetDXValueDataSource(),
        valueExpr: "ID",
        displayExpr: "Name",
        multiline: false,
        deferRendering: false,
        applyValueMode: "useButtons",
        searchEnabled: true,
        onSelectionChanged: function (e) {

        }
    });
    $("#dxValueLinkGrid").dxDataGrid({
        dataSource: await GetDXValueLinkDataSource(),
        remoteOperations: true,
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 50, 100],
            showInfo: true
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        showRowLines: true,
        showColumnLines: true,
        showBorders: true,
        hoverStateEnabled: true,
        rowAlternationEnabled: true,
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
            fileName: "ValueLinkCatalog",
            allowExportSelectedData: true
        },
        filterRow:
        {
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
            let _valueLinkData = data.selectedRowsData[0];
            if (_valueLinkData != null) {
                ValueLinkActionButtons("Update");


                PopulateValueLinkFields(_valueLinkData);
                $("#dxParentAttributeLookup").dxLookup("instance").option("readOnly", true);
                $("#dxParentValueLookup").dxLookup("instance").option("readOnly", true);
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
                        $('<button type="button" data-bs-toggle="modal" data-bs-target="#ValueLinkModal" class="btn btn-success" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-pen-to-square"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenValueLinkID").val(options.data.ID);
                                document.getElementById("ChildValueGroup").hidden = false;
                                document.getElementById("ChildValuesGroup").hidden = true;
                            }).appendTo(container);
                        $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                            'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                            + '</span></button>')
                            .height(30)
                            .on('dxclick', function () {
                                $("#hiddenValueLinkID").val(options.data.ID);
                                ShowDeleteValueLinkQuestion();
                            }).appendTo(container);
                    },
                },
                { caption: "ID", dataField: "ID", visible: false, width: "auto" },
                { caption: "Parent Attribute", dataField: "ParentAttributeName", groupIndex: 0 },
                { caption: "Parent Attribute ID", dataField: "ParentAttributeID", visible: false },
                { caption: "Parent Value", dataField: "ParentValueName", groupIndex: 1 },
                { caption: "Parent Value ID", dataField: "ParentValueID", visible: false },
                { caption: "Child Value ID", dataField: "ChildValueID", visible: false },
                { caption: "Child Value", dataField: "ChildValueName" },
                { caption: "Child Attribute", dataField: "ChildAttributeName" },
                { caption: "Child Attribute ID", dataField: "ChildAttributeID", visible: false },

                { caption: "Description", dataField: "Description" },
                { caption: "AddedDate", dataField: "AddedDate", dataType: 'datetime' },
                { caption: "LastUpdate", dataField: "LastUpdate", dataType: 'datetime' },
                { caption: "Is Active", dataField: "IsActive" },
            ],
    });
    $("#dxValueLinkIsActiveCheckBox").dxCheckBox({
        value: true
    });
    ValueLinkActionButtons("Save");
}
function ValueLinkActionButtons(Action) {
    $("#ValueLinkActionButtons").empty();
    document.getElementById("ValueLinkModalCloseButton").addEventListener("click", ClearValueLinkFields);
    if (Action == "Save") {
        document.getElementById("ValueLinkActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="CreateValueLinkButton" type="button">Save</button>' +
            '</div>';
        document.getElementById("CreateValueLinkButton").addEventListener("click", CreateMultipleValueLink_Global);
    }
    else {
        // Update
        document.getElementById("ValueLinkActionButtons").innerHTML =
            '<div class="col-md-12">' +
            '<button class="btn btn-success m-b-15 float-end" id="UpdateValueLinkButton" type="button">Update</button>' +
            '</div>';
        document.getElementById("UpdateValueLinkButton").addEventListener("click", UpdateValueLink_Global);
    }
}
async function PopulateValueLinkFields(ValueLinkDTO) {
    $("#hiddenValueLinkID").val(ValueLinkDTO.ID);
    await $("#dxParentAttributeLookup").dxLookup("instance").option("value", ValueLinkDTO.ParentAttributeID);
    await $("#dxChildAttributeLookup").dxLookup("instance").option("value", ValueLinkDTO.ChildAttributeID);
    await $("#dxChildValueLookup").dxLookup("instance").option("value", ValueLinkDTO.ChildValueID);
    $("#dxValueLinkIsActiveCheckBox").dxCheckBox("instance").option("value", ValueLinkDTO.IsActive);
    await $("#dxParentValueLookup").dxLookup("instance").option("value", ValueLinkDTO.ParentValueID);


}
async function ShowDeleteValueLinkQuestion() {
    const _alert = await Swal.fire({
        title: 'You will remove the value, are you sure?',
        confirmButtonText: `Delete`,
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _valueLinkDTO = GetValueLinkDTO();
        DeleteValueLink_Global(_valueLinkDTO);
    }
}
function GetValueLinkDTO() {
    let _valueLinkDTO = {
        ID: $("#hiddenValueLinkID").val(),
        ParentValueID: $("#dxParentValueLookup").dxLookup("instance").option("value"),
        ParentAttributeID: $("#dxParentAttributeLookup").dxLookup("instance").option("value"),
        ChildAttributeID: $("#dxChildAttributeLookup").dxLookup("instance").option("value"),
        ChildValueIDArray: $("#dxChildValueTagBox").dxTagBox("instance").option("value"),
        ChildValueID: $("#dxChildValueLookup").dxLookup("instance").option("value"),
        IsActive: $("#dxValueLinkIsActiveCheckBox").dxCheckBox("instance").option("value")
    }
    return _valueLinkDTO;
}
function ClearValueLinkFields() {


    ValueLinkActionButtons("Save");
    $('#hiddenValueLinkID').val("");
    $("#dxParentAttributeLookup").dxLookup("instance").reset();
    $("#dxChildAttributeLookup").dxLookup("instance").reset();
    $("#dxParentValueLookup").dxLookup("instance").reset();
    $("#dxChildValueTagBox").dxTagBox("instance").reset();
    $("#dxChildValueLookup").dxLookup("instance").reset();
    $("#dxValueLinkIsActiveCheckBox").dxCheckBox("instance").option("value", true);
    let keys = $("#dxValueLinkGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxValueLinkGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxValueLinkGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxValueLinkGrid").dxDataGrid("instance").refresh();
    ClearErrorFeedback();
    document.getElementById("ChildValueGroup").hidden = true;
    document.getElementById("ChildValuesGroup").hidden = false;
    $("#dxParentAttributeLookup").dxLookup("instance").option("readOnly", false);
    $("#dxParentValueLookup").dxLookup("instance").option("readOnly", false);
}

//#region ValueLink CRUD Functions
async function CreateMultipleValueLink_Global() {
    await dxLoadPanel.show();
    const _valuelinkDTO = GetValueLinkDTO();
    const _validation_resultDTO = await CreateMultipleValueLink(_valuelinkDTO)
    if (_validation_resultDTO.Result) {
        $("#dxValueLinkGrid").dxDataGrid("instance").refresh();
        ClearValueLinkFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function UpdateValueLink_Global() {
    await dxLoadPanel.show();
    const _valuelinkDTO = GetValueLinkDTO();
    const _validation_resultDTO = await UpdateValueLink(_valuelinkDTO)
    if (_validation_resultDTO.Result) {
        $("#dxValueLinkGrid").dxDataGrid("instance").refresh();
        ClearValueLinkFields();
    }
    HostResponse(_validation_resultDTO);
    dxLoadPanel.hide();
}
async function DeleteValueLink_Global() {
    await dxLoadPanel.show();
    const _valuelinkDTO = GetValueLinkDTO();
    const _validation_resultDTO = await DeleteValueLink(_valuelinkDTO)
    if (_validation_resultDTO.Result) {
        $("#dxValueLinkGrid").dxDataGrid("instance").refresh();
        ClearValueLinkFields();
    }
    HostResponse(_validation_resultDTO);
    ClearValueLinkFields();
    dxLoadPanel.hide();
}

const GetDXValueLinkDataSource_Global = () => {
    return GetDXValueLinkDataSource()
}


async function GetDXParentValue_Global() {
    const _parentValueDTO = {
        IsActive: true,
        AttributeID: $("#dxParentAttributeLookup").dxLookup("instance").option("value")
    }
    let _result = await GetDXValueDataSource(_parentValueDTO);
    return _result;
}
async function GetDXChildValue_Global() {
    let _childValueDTO = {
        IsActive: true,
        AttributeID: $("#dxChildAttributeLookup").dxLookup("instance").option("value")
    }
    let _result = await GetDXValueDataSource(_childValueDTO);
    return _result;
}
//#endregion