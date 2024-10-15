//======================== ValueLink_Service.js (API) ================== 
import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function SubmitDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/SubmitDecoder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function EditDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/Edit`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}


export async function GetDecoderStructureFromDecoder(DecoderDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Decoder/GetDecoderStructureFromDecoder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetDecoderInformation(DecoderDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Decoder/GetDecoderList?` + new URLSearchParams(DecoderDTO), {
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
export async function GetDXDecoderDataSource(DecoderDTO) {
    let params = await BuildSearchParams(DecoderDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Decoder/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _decoderDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });
    return _decoderDataSource;
}