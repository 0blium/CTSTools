import { APIURL } from '../../../../../Common/Utils/Environment.js';
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js';
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../..//Common/Utils/SearchParamsService.js'


export async function CreateCategory(CategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Category/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateCategory(CategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Category/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteCategory(CategoryDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Category/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', CategoryDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetCategoryInformation(CategoryDTO) {
    let _params = await BuildSearchParams(CategoryDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Category/GetCategoryList?` + _params.toString(), {
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
export async function GetDXCategoryDataSource(CategoryDTO) {
    let _params = await BuildSearchParams(CategoryDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Category/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _CategoryDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _CategoryDataSource;
}