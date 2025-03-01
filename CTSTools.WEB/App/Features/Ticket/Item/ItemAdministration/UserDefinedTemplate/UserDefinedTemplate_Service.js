import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateUserDefinedTemplate(UserDefinedTemplateDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefinedTemplate/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedTemplateDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateUserDefinedTemplate(UserDefinedTemplateDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefinedTemplate/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedTemplateDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteUserDefinedTemplate(UserDefinedTemplateDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/UserDefinedTemplate/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', UserDefinedTemplateDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetUserDefinedTemplateInformation(userDefinedTemplateDTO) {
    let params = await BuildSearchParams(userDefinedTemplateDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/UserDefinedTemplate/GetList?` + params.toString(), {
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
export async function GetDXUserDefinedTemplateDataSource(UserDefinedTemplateDTO) {
    let params = await BuildSearchParams(UserDefinedTemplateDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/UserDefinedTemplate/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _userDefinedTemplateDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _userDefinedTemplateDataSource;
}