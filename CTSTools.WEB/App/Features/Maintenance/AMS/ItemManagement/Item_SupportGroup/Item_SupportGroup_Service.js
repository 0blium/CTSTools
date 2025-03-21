import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'


export async function CreateItem_SupportGroup(Item_SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_SupportGroup/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateItem_SupportGroup(Item_SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_SupportGroup/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteItem_SupportGroup(Item_SupportGroupDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Item_SupportGroup/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', Item_SupportGroupDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetItem_SupportGroupInformation(Item_SupportGroupDTO) {
    let params = await BuildSearchParams(Item_SupportGroupDTO);

    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Item_SupportGroup/GetList?` + params.toString(), {
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
export async function GetDXItem_SupportGroupDataSource(Item_SupportGroupDTO) {
    let params = await BuildSearchParams(Item_SupportGroupDTO);

    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Item_SupportGroup/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _Item_SupportGroupDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 10,
        remoteOperations: true,
    });
    return _Item_SupportGroupDataSource;
}