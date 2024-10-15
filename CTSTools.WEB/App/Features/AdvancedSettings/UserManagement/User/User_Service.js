import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateUser(UserDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUser(UserDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUser(UserDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/User/`;
    try {
        _validationResultDTO = await APIRequest(_url, 'DELETE', UserDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetUserInformation(userDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/User/GetList?` + new URLSearchParams(userDTO), {
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
export async function GetDXUserDataSource(UserDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/User/GetPagedList?` + new URLSearchParams(UserDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _userDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _userDataSource;
}