import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateMailGroup(MailGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroup/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateMailGroup(MailGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroup/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteMailGroup(MailGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroup/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetMailGroupInformation(mailGroupDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/MailGroup/GetMailGroupList?` + new URLSearchParams(mailGroupDTO), {
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
//DX DataSource
export async function GetDXMailGroupDataSource(MailGroupDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/MailGroup/GetPagedList?` + new URLSearchParams(MailGroupDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _mailGroupDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _mailGroupDataSource;
}