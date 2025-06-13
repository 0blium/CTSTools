import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'

export async function CreateBrand(BrandDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Brand/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', BrandDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateBrand(BrandDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Brand/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', BrandDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteBrand(BrandDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Brand/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', BrandDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetBrandInformation(BrandDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Brand/GetBrandList?` + new URLSearchParams(BrandDTO), {
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
export async function GetDXBrandDataSource(BrandDTO) {
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Brand/GetPagedList?` + new URLSearchParams(BrandDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _brandDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _brandDataSource;
}