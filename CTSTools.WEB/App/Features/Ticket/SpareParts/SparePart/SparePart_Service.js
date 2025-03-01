import { APIURL } from '../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../Common/Utils/SearchParamsService.js'

export async function CreateSparePart(SparePartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSparePart(SparePartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSparePart(SparePartDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePartDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSparePartInformation(SparePartDTO) {
    let _params = await BuildSearchParams(SparePartDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SparePart/GetSparePartList?` + _params.toString(), {
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
export async function GetDXSparePartDataSource(SparePartDTO) {
    let _params = await BuildSearchParams(SparePartDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SparePart/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _SparePartDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _SparePartDataSource;
}