import { GetDXValueLinkDataSource } from '../AttributeManagement/ValueLink/ValueLink_Service.js'
import { GetDXValueDataSource } from '../AttributeManagement/Value/Value_Service.js'
import { Attributes } from '../AttributeManagement/Attribute/Attribute_Enum.js'
import { GetDecoderStructureFromDecoder } from '../DecoderManagement/Decoder/Decoder_Service.js'
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js';
import { Value_Enum } from '../AttributeManagement/Value/Value_Enum.js'
import { GetDXSubClass_SupplierDataSource } from '../DecoderManagement/SubClass_Supplier/SubClass_Supplier.js'
import { CreatePart, UpdatePart, DeletePart } from '../PartManagement/Part/Part_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeDecoderCatalogControls();
    document.getElementById("CreateButton").addEventListener("click", ShowCreatePartCreatorQuestion);


});

function GetAttributeList() {
    let _attributeList = [];
    let _attributes = document.getElementById("AttributeSection").querySelectorAll("[id*='Lookup']");
    for (let i = 0; i < _attributes.length; i++) {
        let _nameID = _attributes[i].getAttribute('id');
        let _value = $(`#${_nameID}`).dxLookup("instance").option("selectedItem");
        _attributeList.push(_value);
    }
    return _attributeList;
}

function GetPartCreatorDTO() {
    return {
        DecoderID: $("#HiddenPartDecoderID").val(),
        SupplierID: $("#dxDecoderSubClass_SupplierLookup").dxLookup("instance").option("value"),
        MfgPartNumber: $("#dxDecoderManufactureNumberTextBox").dxTextBox("instance").option("value"),
        ValueList: GetAttributeList(),
        IsActive: true,
        Comment: $("#dxDecoderCommentTextArea").dxTextArea("instance").option("value")
    }
}

async function CreatePart_Global() {
    await dxLoadPanel.show();
    let _partDTO = GetPartCreatorDTO();
    const _validationResultDTO = await CreatePart(_partDTO)
    if (_validationResultDTO.Result) {
        _partDTO = _validationResultDTO.Data;

        document.getElementById("PartDescription").innerText = _partDTO.Description;
        document.getElementById("RequestID").innerText = _partDTO.ID;
        document.getElementById("PartInfoCardGroup").hidden = false;
        document.getElementById("PartInfoGroup").hidden = false;
        document.getElementById("CreateButton").hidden = true;
        //    ClearDecoderStructureFields();
    }
    HostResponse(_validationResultDTO);
    dxLoadPanel.hide();
}

function ClearPartCreatorFields() {
    document.getElementById("PartDescription").innerText = "";
    document.getElementById("RequestID").innerText = "";
    document.getElementById("PartInfoCardGroup").hidden = true;
    document.getElementById("PartInfoGroup").hidden = true;
    document.getElementById("CreateButton").hidden = false;    
    $("#HiddenPartDecoderID").val(0);
    $('#AttributeSection').empty();
    document.getElementById("MfgGroup").hidden = true;
    document.getElementById("CommentGroup").hidden = true;
    document.getElementById("CommentCardGroup").hidden = true;
    document.getElementById("MfgAttributesGroup").hidden = true;
    document.getElementById("AttributeCardGroup").hidden = true;
    document.getElementById("AttributesGroup").hidden = true;
    $("#dxDecoderCommentTextArea").dxTextArea("instance").reset();
}
async function ShowCreatePartCreatorQuestion() {
    const _alert = await Swal.fire({
        icon: 'info',
        title: 'Are you sure?',
        text: 'Make sure all attributes are correct. ',
        confirmButtonText: `Confirm`,
        confirmButtonColor: '#3085d6',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        CreatePart_Global();

    }
}
async function InitializeDecoderCatalogControls() {
    $("#dxDecoderCommentTextArea").dxTextArea({
        placeholder: 'Type a comment...'
    });
    $("#dxDecoderPartTypeLookup").dxLookup({
        dataSource: await GetPartTypeDataSource_Global(),
        displayExpr: "Name",
        valueExpr: "ID",
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                ClearPartCreatorFields();
                $("#dxDecoderComponentTypeLookup").dxLookup("instance").reset();
                $("#dxDecoderClassLookup").dxLookup("instance").reset();
                $("#dxDecoderSubClassLookup").dxLookup("instance").reset();

                if (e.value == Value_Enum.Manufactured) {

                    document.getElementById("ComponentTypeGroup").hidden = false;
                    await $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("dataSource", await GetComponentTypeDataSource_Global());
                }
                else {

                    document.getElementById("ComponentTypeGroup").hidden = true;
                    await $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", await GetClassFromPartTypeDataSource_Global());
                }
            }
            else {
                $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxDecoderComponentTypeLookup").dxLookup({
        //dataSource: GetPartTypeDataSource_Global(),
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnabled: true,
        onValueChanged: async function (e) {
            $("#dxDecoderClassLookup").dxLookup("instance").reset();
            if (e.value != 0 && e.value != null) {
                ClearPartCreatorFields();
                $("#dxDecoderClassLookup").dxLookup("instance").reset();
                $("#dxDecoderSubClassLookup").dxLookup("instance").reset();

                await $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", await GetClassFromComponentTypeDataSource_Global());
            }
            else {
                $("#dxDecoderClassLookup").dxLookup("instance").option("dataSource", []);
            }
        }
    });
    $("#dxDecoderClassLookup").dxLookup({
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnabled: true,
        onValueChanged: async function (e) {
            ClearPartCreatorFields();
            $("#dxDecoderSubClassLookup").dxLookup("instance").reset();
            if (e.value != 0 && e.value != null) {
                await $("#dxDecoderSubClassLookup").dxLookup("instance").option("dataSource", await GetSubClassDataSource_Global());
            }
            else {
                $("#dxDecoderSubClassLookup").dxLookup("instance").option("dataSource", []);
            }

        }
    });
    $("#dxDecoderSubClassLookup").dxLookup({
        displayExpr: "ChildValueName",
        valueExpr: "ChildValueID",
        searchEnabled: true,
        onValueChanged: async function (e) {
            if (e.value != 0 && e.value != null) {
                await GetDecoder(e.value)
            }
        }
    });
    $("#dxDecoderManufactureNumberTextBox").dxTextBox({
        placeholder: 'Type MFG #...'
    });
    $("#dxDecoderSubClass_SupplierLookup").dxLookup({
        displayExpr: "SupplierName",
        valueExpr: "SupplierID",
        searchEnabled: true,
    });
}
async function GetDecoder(SubClassID) {
    let decoderDTO = {
        SubClassID: SubClassID,
        IsActive: true
    }
    let _validationResultDTO = await GetDecoderStructureFromDecoder(decoderDTO);
    if (!_validationResultDTO.Result) {
        HostResponse(_validationResultDTO);
        return true;
    }
    document.getElementById("MfgGroup").hidden = false;
    document.getElementById("MfgAttributesGroup").hidden = false;
    document.getElementById("AttributeCardGroup").hidden = false;
    document.getElementById("AttributesGroup").hidden = false;
    document.getElementById("CommentGroup").hidden = false;
    document.getElementById("CommentCardGroup").hidden = false;

    let _decoderDTO = _validationResultDTO.Data;
    $("#HiddenPartDecoderID").val(_decoderDTO.ID);
    $('#AttributeSection').empty();
    for (const i in _decoderDTO.DecoderStructureList) {
        const attributeValues = [Attributes.Class, Attributes.SubClass, Attributes.ComponentType, Attributes.PartType, Attributes.Symbol, Attributes.ClassID, Attributes.Variant];
        if (!attributeValues.includes(_decoderDTO.DecoderStructureList[i].AttributeID)) {
            $('#AttributeSection').append(
                '<div class="row col-lg-3 mb-15px" id="field' + _decoderDTO.DecoderStructureList[i].AttributeID + '">' +
                '<label class="form-label col-xl-12 col-md-12">' + _decoderDTO.DecoderStructureList[i].AttributeName + '(<span class="text-danger">*</span>)</label>' +
                '<div class="col-xl-12 col-md-12">' +
                '<div id="Attribute' + _decoderDTO.DecoderStructureList[i].AttributeID + 'Lookup"></div>' +
                '<div class="invalid-feedback" id="ChildValueArrayValidation"></div>' +
                '</div>' +
                '</div>');
            //Select box initialization for each of the attributes of the subclass
            $('#Attribute' + _decoderDTO.DecoderStructureList[i].AttributeID + 'Lookup').dxLookup({
                dataSource: _decoderDTO.DecoderStructureList[i].ValueList,
                valueExpr: "ID",
                displayExpr: "Name",
                searchEnabled: true

            });
        }
    }

    $("#dxDecoderSubClass_SupplierLookup").dxLookup("instance").option("dataSource", await GetDXSubClass_SupplierDataSource_Global());
}
async function GetPartTypeDataSource_Global() {
    let _partTypeDTO = {
        IsActive: true,
        AttributeID: Attributes.PartType
    }
    return await GetDXValueDataSource(_partTypeDTO)
}
async function GetComponentTypeDataSource_Global() {
    let _componentTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxDecoderPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.ComponentType
    }
    return await GetDXValueLinkDataSource(_componentTypeDTO)
}
async function GetClassFromComponentTypeDataSource_Global() {
    let _classTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.ComponentType,
        ParentValueID: $("#dxDecoderComponentTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.Class
    }
    return await GetDXValueLinkDataSource(_classTypeDTO)
}
async function GetClassFromPartTypeDataSource_Global() {
    let _classTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.PartType,
        ParentValueID: $("#dxDecoderPartTypeLookup").dxLookup("instance").option("value"),
        ChildAttribute: Attributes.Class
    }
    return await GetDXValueLinkDataSource(_classTypeDTO)
}
async function GetSubClassDataSource_Global() {
    let _subClassTypeDTO = {
        IsActive: true,
        ParentAttributeID: Attributes.Class,
        ParentValueID: $("#dxDecoderClassLookup").dxLookup("instance").option("value"),
        ChildAttributeID: Attributes.SubClass
    }
    return await GetDXValueLinkDataSource(_subClassTypeDTO)
}
async function GetDXSubClass_SupplierDataSource_Global() {
    let _subClass_SupplierDTO = { SubClassID: $("#dxDecoderSubClassLookup").dxLookup("instance").option("value") }
    return await GetDXSubClass_SupplierDataSource(_subClass_SupplierDTO);
}
