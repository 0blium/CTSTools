import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'

export async function CreateUserDefined(UserDefinedDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefined/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUserDefined(UserDefinedDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefined/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUserDefined(UserDefinedDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefined/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetUserDefinedInformation(userDefinedDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/UserDefined/GetUserDefinedList?` + new URLSearchParams(userDefinedDTO), {
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
export async function GetDXUserDefinedDataSource(UserDefinedDTO) {
    let params = await BuildSearchParams(UserDefinedDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/UserDefined/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _userDefinedDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _userDefinedDataSource;
}