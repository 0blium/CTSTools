import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js'

export async function CreateSparePart_Lot(SparePart_LotDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart_Lot/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePart_LotDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function UpdateSparePart_Lot(SparePart_LotDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart_Lot/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePart_LotDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function DeleteSparePart_Lot(SparePart_LotDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/SparePart_Lot/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', SparePart_LotDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}

export async function GetSparePart_LotInformation(SparePart_LotDTO) {
    let _params = await BuildSearchParams(SparePart_LotDTO);
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/SparePart_Lot/GetList?` + _params.toString(), {
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
export async function GetDXSparePart_LotDataSource(SparePart_LotDTO) {
    let _params = await BuildSearchParams(SparePart_LotDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/SparePart_Lot/GetPagedList?` + _params.toString(),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => {
        },
    });
    let _SparePart_LotDataSource = new DevExpress.data.DataSource({
        store: _store,
        paginate: true,
        pageSize: 15,
        remoteOperations: true,
    });
    return _SparePart_LotDataSource;
}