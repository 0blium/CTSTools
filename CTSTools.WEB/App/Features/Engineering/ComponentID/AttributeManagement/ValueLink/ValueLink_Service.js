import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'


export async function CreateValueLink(ValueLinkDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueLink/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueLinkDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function CreateMultipleValueLink(ValueLinkDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueLink/CreateMultiple`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueLinkDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateValueLink(ValueLinkDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueLink/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueLinkDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteValueLink(ValueLinkDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/ValueLink/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ValueLinkDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetValueLinkInformation(ValueLinkDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _params = await BuildSearchParams(ValueLinkDTO);
        const _response = await fetch(`${APIURL}/ValueLink/GetList?` + _params.toString(), {
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
export async function GetDXValueLinkDataSource(ValueLinkDTO) {
    let params = await BuildSearchParams(ValueLinkDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/ValueLink/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {

        },
    });
    let _valueLinkDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });

    return _valueLinkDataSource;
}