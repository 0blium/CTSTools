import { APIURL } from '../../../../../Common/Utils/Environment.js'
import { ValidationResultDTO } from '../../../../../Common/Utils/ValidationResultDTO.js'
import APIRequest from '../../../../../Common/Utils/APIRequest.js'
import BuildSearchParams from '../../../../../Common/Utils/SearchParamsService.js';


export async function CreateAttribute(AttributeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Attribute/Create`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', AttributeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function CreateMassiveAttribute(FileDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Attribute/CreateMassive`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', FileDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function UpdateAttribute(AttributeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Attribute/Update`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', AttributeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function DeleteAttribute(AttributeDTO) {
    let _validationResultDTO = ValidationResultDTO;
    const _url = `${APIURL}/Attribute/Delete`;
    try {
        _validationResultDTO = await APIRequest(_url, 'POST', AttributeDTO);
    }
    catch (error) {
        _validationResultDTO.Result = false;
        _validationResultDTO.Message = error.Message;
        _validationResultDTO.Description = error.Data;
    }
    return _validationResultDTO;
}
export async function GetAttributeInformation(AttributeDTO) {
    let _validation_resultDTO = new Object();
    try {
        const _response = await fetch(`${APIURL}/Attribute/GetAttributeList?` + new URLSearchParams(AttributeDTO), {
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
export async function GetDXAttributeDataSource(AttributeDTO, Filter) {
    let params = await BuildSearchParams(AttributeDTO);
    let _store = new DevExpress.data.AspNet.createStore({
        loadUrl: `${APIURL}/Attribute/GetPagedList?` + new URLSearchParams(AttributeDTO),
        key: "ID",
        beforeSend: (sender, ajaxSettings) => { },
    });
    let _attributeDTOSource = new DevExpress.data.CustomStore({
        store: _store,
        paginate: true,
        remoteOperations: true,
        pageSize: 15,
        loadMode: "raw",
        load: function (loadOptions) {
            console.log(loadOptions);

            // Validar si se esta buscando un valor en la propiedad Search value y si se esta aplicando un Filter estatico
            if (loadOptions.searchValue && Filter) {
                // Combinar el filtro estático con el dinámico usando "and"
                loadOptions.filter = [
                    Filter,
                    "and",
                    [loadOptions.searchExpr, loadOptions.searchOperation, loadOptions.searchValue]
                ];
                console.log(loadOptions.filter);
            }
            //validar si esta aplicando un filtrado estatico
            if (Filter) {
                console.log(loadOptions);

                // Aplicar solo el filtro estático si no hay dinámico
                loadOptions.filter = Filter;
            }

            // Realizar la carga de datos con los filtros combinados
            return _store.load(loadOptions);
        }
    });

    return _attributeDTOSource;
}
