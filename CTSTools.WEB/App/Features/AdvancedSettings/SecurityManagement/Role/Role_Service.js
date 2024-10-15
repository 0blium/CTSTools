import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'


//test with generic api request
export async function CreateRole(RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateRole(RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteRole(RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Role/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetRoleInformation(roleDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Role/GetRoleList?` + new URLSearchParams(roleDTO), {
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
export async function GetDXRoleDataSource(RoleDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Role/GetPagedList?` + new URLSearchParams(RoleDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _roleDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _roleDataSource;
}