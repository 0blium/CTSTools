import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateSubClass(SubClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SubClass/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SubClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function CreateMassiveSubClass(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SubClass/CreateMassive`;
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
export async function UpdateSubClass(SubClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SubClass/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SubClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSubClass(SubClassDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SubClass/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SubClassDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSubClassInformation(SubClassDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _params = await BuildSearchParams(SubClassDTO);
        const _response = await fetch(`${APIURL}/SubClass/GetList?` + _params.toString(), {
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
export async function GetDXSubClassDataSource(SubClassDTO) {
    let params = await BuildSearchParams(SubClassDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SubClass/GetPagedList?` + params.toString(),
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