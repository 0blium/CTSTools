import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'

export async function CreateUser_Permission(User_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Permission/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUser_Permission(User_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Permission/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUser_Permission(User_PermissionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Permission/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_PermissionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetUser_PermissionInformation(User_PermissionDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/User_Permission/GetUser_PermissionList?` + new URLSearchParams(User_PermissionDTO), {
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
export async function GetDXUser_PermissionDataSource(UserPermissionDTO) {
    let params = await BuildSearchParams(UserPermissionDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/User_Permission/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {

        },
    });
    let _user_PermissionDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });
    return _user_PermissionDataSource;
}