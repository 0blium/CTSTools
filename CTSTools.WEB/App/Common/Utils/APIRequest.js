import { ValidationResultDTO } from "./ValidationResultDTO.js";

export default async function APIRequest(url, method, data) {
    let _validationResultDTO = ValidationResultDTO;
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    const _response = await fetch(url, options);
    if (_response.ok) {
        _validationResultDTO = await _response.json();
    }
    else {
        _validationResultDTO = await _response.json();
        _validationResultDTO.Message = _response.statusText;
    }
    return _validationResultDTO;
}