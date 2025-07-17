import { APIURL } from '../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../Common/Utils/SearchParamsService.js';


export async function CreateProduct(ProductDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Product/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProductDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateProduct(ProductDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Product/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProductDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteProduct(ProductDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Product/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', ProductDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetProductInformation(ProductDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Product/GetProductList?` + new URLSearchParams(ProductDTO), {
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
export async function GetDXProductDataSource(ProductDTO) {
    let params = await BuildSearchParams(ProductDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Product/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _productDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _productDataSource;
}