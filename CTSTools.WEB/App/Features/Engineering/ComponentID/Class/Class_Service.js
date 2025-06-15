import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

export async function CreateClass(ClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ClassCatalog/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateClass(ClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ClassCatalog/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteClass(ClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ClassCatalog/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetClassInformation(ClassDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/ClassCatalog/GetClassList?` + new URLSearchParams(ClassDTO), {
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
export async function GetDXClassDataSource(ClassDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ClassCatalog/GetPagedList?` + new URLSearchParams(ClassDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _ClassDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _ClassDataSource;
}