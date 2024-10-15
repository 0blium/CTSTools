//======================== ValueLink_Service.js (API) ================== 
import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateSupplier(SupplierDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Supplier/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplierDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSupplier(SupplierDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Supplier/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplierDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSupplier(SupplierDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Supplier/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SupplierDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}



export async function GetSupplierInformation(SupplierDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Supplier/GetSupplierList?` + new URLSearchParams(SupplierDTO), {
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
export async function GetDXSupplierDataSource(SupplierDTO) {
    let params = await BuildSearchParams(SupplierDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Supplier/GetPagedList?` + params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _supplierDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
    });
    return _supplierDataSource;
}