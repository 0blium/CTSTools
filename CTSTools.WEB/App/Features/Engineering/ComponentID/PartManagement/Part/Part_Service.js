import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'
export async function CreatePart(PartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Part/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdatePart(PartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Part/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeletePart(PartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Part/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', PartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetPartInformation(PartDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Part/GetPartList?` + new URLSearchParams(PartDTO), {
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
export async function GetDXPartDataSource(PartDTO) {
    let params = await BuildSearchParams(PartDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Part/GetPagedList?` + new URLSearchParams(PartDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => { },
    });
    let _PartDTOSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,

    });

    return _PartDTOSource;
}