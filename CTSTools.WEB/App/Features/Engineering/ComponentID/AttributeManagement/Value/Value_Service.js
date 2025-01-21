import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'


export async function CreateValue(ValueDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Value/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function CreateMassiveValue(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Value/CreateMassive`;
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
export async function UpdateValue(ValueDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Value/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteValue(ValueDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Value/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetValueInformation(ValueDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _params = await BuildSearchParams(ValueDTO);
        const _response = await fetch(`${APIURL}/Value/GetList?` + _params.toString(), {
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
export async function GetDXValueDataSource(ValueDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Value/GetPagedList?` + new URLSearchParams(ValueDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _valueDTOSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15
        
    });
    return _valueDTOSource;
}
