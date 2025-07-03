import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreatePartType(PartTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/PartType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdatePartType(PartTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/PartType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeletePartType(PartTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/PartType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetPartTypeInformation(PartTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/PartType/GetPartTypeList?` + new URLSearchParams(PartTypeDTO), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

//DX DataSource
export async function GetDXPartTypeDataSource(PartTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/PartType/GetPagedList?` + new URLSearchParams(PartTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _PartTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _PartTypeDataSource;
}