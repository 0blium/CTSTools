import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js';


export async function CreateDocumentType(DocumentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentType/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateDocumentType(DocumentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentType/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteDocumentType(DocumentTypeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/DocumentType/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', DocumentTypeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetDocumentTypeInformation(DocumentTypeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/DocumentType/GetDocumentTypeList?` + new URLSearchParams(DocumentTypeDTO), {
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
export async function GetDXDocumentTypeDataSource(DocumentTypeDTO) {
    let params = await BuildSearchParams(DocumentTypeDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/DocumentType/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _documentTypeDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _documentTypeDataSource;
}