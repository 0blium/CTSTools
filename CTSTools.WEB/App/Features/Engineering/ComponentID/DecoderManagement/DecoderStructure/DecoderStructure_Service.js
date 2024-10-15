//======================== ValueLink_Service.js (API) ================== 
import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateDecoderStructure(DecoderStructureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DecoderStructure/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderStructureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDecoderStructure(DecoderStructureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DecoderStructure/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderStructureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateDescriptionOrder(DecoderStructureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DecoderStructure/UpdateDescriptionOrder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderStructureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateNumberOrder(DecoderStructureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DecoderStructure/UpdateNumberOrder`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderStructureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteDecoderStructure(DecoderStructureDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DecoderStructure/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DecoderStructureDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;


}

export async function GetDecoderStructureInformation(DecoderStructureDTO) {
    let _validation_resultDTO = new Object();
    let params = await BuildSearchParams(DecoderStructureDTO);
    try {
        const _response = await fetch(`${APIURL}/DecoderStructure/GetList?` + params.toString(), {
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
export async function GetDXDecoderStructureDataSource(DecoderStructureDTO) {
    let params = await BuildSearchParams(DecoderStructureDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DecoderStructure/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });

    let _decoderStructureDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 20,
    });
    return _decoderStructureDataSource;
}