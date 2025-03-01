import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../Common/Utils/SearchParamsService.js'

export async function CreateSupportGroupMember(SupportGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroupMember/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSupportGroupMember(SupportGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroupMember/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSupportGroupMember(SupportGroupMemberDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SupportGroupMember/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupportGroupMemberDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSupportGroupMemberInformation(SupportGroupMemberDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SupportGroupMember/GetSupportGroupMemberList?` + new URLSearchParams(SupportGroupMemberDTO), {
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
export async function GetDXSupportGroupMemberDataSource(SupportGroupMemberDTO) {
    let params = await BuildSearchParams(SupportGroupMemberDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SupportGroupMember/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _supportGroupMemberDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _supportGroupMemberDataSource;
}