import { APIURL } from '../../../../common/utils/environment.js'
import { ValidationResultDTO } from '../../../../common/utils/validationresultdto.js'
import APIRequest from '../../../../common/utils/apirequest.js'
import BuildSearchParams from '../../../../common/utils/searchparamsservice.js';



export async function CreateDocumentRevision(DocumentRevisionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentRevision/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentRevisionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDocumentRevision(DocumentRevisionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentRevision/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentRevisionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDocumentRevision(DocumentRevisionDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentRevision/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentRevisionDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDocumentRevisionInformation(DocumentRevisionDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/DocumentRevision/GetDocumentRevisionList?` + new URLSearchParams(DocumentRevisionDTO), {
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
export async function GetDXDocumentRevisionDataSource(DocumentRevisionDTO) {
    let params = await BuildSearchParams(DocumentRevisionDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DocumentRevision/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _DocumentRevisionDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _DocumentRevisionDataSource;
}