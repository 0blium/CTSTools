import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateClass(ClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Class/Create`;
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
export async function CreateMassiveClass(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Class/CreateMassive`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FileDTO);
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
    const _url = `${APIURL}/Class/Update`;
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
    const _url = `${APIURL}/Class/Delete`;
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
        const _params = await BuildSearchParams(ClassDTO);
        const _response = await fetch(`${APIURL}/Class/GetList?` + _params.toString(), {
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
export async function GetDXClassDataSource(ClassDTO) {
    let params = await BuildSearchParams(ClassDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Class/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {

        },
    });
    let _classDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });
    return _classDataSource;
}