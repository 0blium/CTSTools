import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';

export async function CreateValueType(ValueTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateValueType(ValueTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteValueType(ValueTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetValueTypeInformation(ValueTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/ValueType/GetValueTypeList?` + new URLSearchParams(ValueTypeDTO), {
            method: 'GET',
            headers: { 'Content-Type': 'application/json; charset= UTF-8' }
        });
        const _data = await _response.json();
        _validation_resultDTO = _data.Data;
    }
    catch (e) {
        _validation_resultDTO.Result = false;
        _validation_resultDTO.Description = "";
        _validation_resultDTO.Message = "";

    }
    return _validation_resultDTO;
}

//DX DataSource
export async function GetDXValueTypeDataSource(ValueTypeDTO) {
    let params = await BuildSearchParams(ValueTypeDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ValueType/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _valueTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _valueTypeDataSource;
}