import { APIURL } from '../../../../../App/Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../App/Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../App/Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateUser_Role(User_RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Role/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUser_Role(User_RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Role/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUser_Role(User_RoleDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User_Role/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', User_RoleDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetUser_RoleInformation(User_RoleDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/User_Role/GetUser_RoleList?` + new URLSearchParams(User_RoleDTO), {
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
export async function GetDXUser_RoleDataSource(User_RoleDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/User_Role/GetPagedList?` + new URLSearchParams(User_RoleDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _user_RoleDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _user_RoleDataSource;
}