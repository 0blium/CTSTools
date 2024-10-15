import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'

//test with generic api request
export async function CreateMailGroupMember(MailGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroupMember/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateMailGroupMember(MailGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroupMember/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteMailGroupMember(MailGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/MailGroupMember/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', MailGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
//Read
export async function GetMailGroupMemberInformation(mailGroupMemberDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/MailGroupMember/GetMailGroupMemberList?` + new URLSearchParams(mailGroupMemberDTO), {
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
export async function GetDXMailGroupMemberDataSource(MailGroupMemberDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/MailGroupMember/GetPagedList?` + new URLSearchParams(MailGroupMemberDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _mailGroupMemberDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _mailGroupMemberDataSource;
}