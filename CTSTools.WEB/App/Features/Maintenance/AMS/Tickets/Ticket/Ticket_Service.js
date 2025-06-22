import { APIURL } from '../../../../../Common/Utils/Environment.js';
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js';
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../..//Common/Utils/SearchParamsService.js'

export async function CreateTicket(TicketDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Ticket/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TicketDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateTicket(TicketDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Ticket/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TicketDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteTicket(TicketDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Ticket/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', TicketDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetTicketInformation(TicketDTO) {
    let _params = await BuildSearchParams(TicketDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Ticket/GetList?` + _params.toString(), {
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
export async function GetDXTicketDataSource(TicketDTO) {
    console.log("service")
    let _params = await BuildSearchParams(TicketDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Ticket/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _TicketDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _TicketDataSource;
}




//#region Attachments

export async function GetTicketFilesInformation(TicketDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Ticket/GetFileList?` + new URLSearchParams(TicketDTO), {
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

export async function DeleteTicketFile(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Ticket/FileDelete`;
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

export async function UploadTicketFile(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Ticket/UploadFile`;
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
//#endregion