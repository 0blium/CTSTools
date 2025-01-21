import { APIURL } from '../../../../common/utils/environment.js'
import { ValidationResultDTO } from '../../../../common/utils/validationresultdto.js'
import APIRequest from '../../../../common/utils/apirequest.js'
import BuildSearchParams from '../../../../common/utils/searchparamsservice.js';



export async function CreateDocument(DocumentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Document/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDocument(DocumentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Document/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDocument(DocumentDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Document/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDocumentInformation(DocumentDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Document/GetDocumentList?` + new URLSearchParams(DocumentDTO), {
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
export async function GetDXDocumentDataSource(DocumentDTO) {
    let params = await BuildSearchParams(DocumentDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Document/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _DocumentDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _DocumentDataSource;
}