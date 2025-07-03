import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateComponentType(ComponentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ComponentType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ComponentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateComponentType(ComponentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ComponentType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ComponentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteComponentType(ComponentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ComponentType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ComponentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetComponentTypeInformation(ComponentTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/ComponentType/GetComponentTypeList?` + new URLSearchParams(ComponentTypeDTO), {
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
export async function GetDXComponentTypeDataSource(ComponentTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ComponentType/GetPagedList?` + new URLSearchParams(ComponentTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _ComponentTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _ComponentTypeDataSource;
}