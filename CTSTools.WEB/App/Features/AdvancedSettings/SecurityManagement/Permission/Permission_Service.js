import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreatePermission(PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Permission/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdatePermission(PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Permission/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeletePermission(PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Permission/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetPermissionInformation(permissionDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Permission/GetPermissionList?` + new URLSearchParams(permissionDTO), {
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
export async function GetDXPermissionDataSource(PermissionDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Permission/GetPagedList?` + new URLSearchParams(PermissionDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _permissionDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 10
    });
    return _permissionDataSource;
}