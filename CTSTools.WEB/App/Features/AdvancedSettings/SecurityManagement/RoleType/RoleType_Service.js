import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateRoleType(RoleTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/RoleType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateRoleType(RoleTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/RoleType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteRoleType(RoleTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/RoleType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', RoleTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetRoleTypeInformation(roleTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/RoleType/GetRoleTypeList?` + new URLSearchParams(roleTypeDTO), {
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
export async function GetDXRoleTypeDataSource(RoleTypeDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/RoleType/GetPagedList?` + new URLSearchParams(RoleTypeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _roleTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
    });
    return _roleTypeDataSource;
}