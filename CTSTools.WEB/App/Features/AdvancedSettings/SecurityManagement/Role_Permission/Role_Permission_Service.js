import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'


//test with generic api request
export async function CreateRole_Permission(Role_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role_Permission/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Role_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateRole_Permission(Role_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role_Permission/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Role_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteRole_Permission(Role_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role_Permission/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Role_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetRole_PermissionInformation(Role_PermissionDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Role_Permission/GetRole_PermissionList?` + new URLSearchParams(Role_PermissionDTO), {
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
export async function GetDXRole_PermissionDataSource(Role_PermissionDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Role_Permission/GetPagedList?` + new URLSearchParams(Role_PermissionDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _role_PermissionDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _role_PermissionDataSource;
}