////export function HostResponse(_validation_ResultDTO) {
////    if (_validation_ResultDTO.Result === true) {

////        Swal.fire(_validation_ResultDTO.Message, _validation_ResultDTO.Description, "success");
////    }
////    if (_validation_ResultDTO.Result === false && (_validation_ResultDTO.Validation_ResultList == null || _validation_ResultDTO.Validation_ResultList.length == 0)) {
////        toastr["error"](_validation_ResultDTO.Description, _validation_ResultDTO.Message);
////    }
////    if (_validation_ResultDTO.Result === false && _validation_ResultDTO.Validation_ResultList != null) {
////        for (let _property in _validation_ResultDTO.Validation_ResultList) {
////            toastr["error"](_validation_ResultDTO.Validation_ResultList[_property].Description, _validation_ResultDTO.Validation_ResultList[_property].Message);
////        }
////    }

////}
export function HostResponse(_validation_ResultDTO) {
    /*debugger;*/
    if (_validation_ResultDTO.Result === true) {

        Swal.fire(_validation_ResultDTO.Message, _validation_ResultDTO.Description, "success");
    }
    if (_validation_ResultDTO.Result === false && (_validation_ResultDTO.ValidationResultList == null || _validation_ResultDTO.ValidationResultList.length == 0)) {
        Swal.fire(_validation_ResultDTO.Message, _validation_ResultDTO.Description, "error");
        //toastr["error"](_validation_ResultDTO.Description, _validation_ResultDTO.Message);
    }
    if (_validation_ResultDTO.Result === false && _validation_ResultDTO.ValidationResultList.length > 0) {
        ClearErrorFeedback();
        ShowErrorFeedback(_validation_ResultDTO);
    }

}


export const ErrorResponse = (_validation_ResultDTO) => {
    if (_validationResult.Result === false) {
        Swal.fire(_validation_ResultDTO.Message, _validation_ResultDTO.Description, "error");
    }
}

function ShowErrorFeedback(Validation_resultDTO) {
    for (let _index in Validation_resultDTO.ValidationResultList) {
        const _validation_resultDTO = Validation_resultDTO.ValidationResultList[_index];
        let _propertyName;
        let _propertyValidate;
        if (_validation_resultDTO.Data != null) {
            if (_validation_resultDTO.Data.includes("IDArray")) {
                _propertyName = _validation_resultDTO.Data.split("IDArray")[0];
                _propertyValidate = `${_propertyName}Validation`;
                const _dxPropetyTagBox = `#dx${_propertyName}TagBox`;
                if ($(_dxPropetyTagBox).dxTagBox("instance")) {
                    $(_dxPropetyTagBox).dxTagBox("instance").option("isValid", false);
                }
            }
            else if (_validation_resultDTO.Data.includes("DTO")) {
                _propertyName = _validation_resultDTO.Data.split("DTO")[0];
                _propertyValidate = `${_propertyName}Validation`;
                const _dxPropetySelectBox = `#dx${_propertyName}SelectBox`;
                if ($(_dxPropetySelectBox).dxSelectBox("instance")) {
                    $(_dxPropetySelectBox).dxSelectBox("instance").option("isValid", false);
                }
            }
            else {
                _propertyName = _validation_resultDTO.Data;
                _propertyName = _propertyName.split("ID")[0];
                _propertyValidate = `${_propertyName}Validation`;

                //const _dxPropetySelectBox = `#dx${_propertyName}TextBox`;
                //$(_dxPropetySelectBox).dxTextBox("instance").option("isValid", false);
                const _dxPropetySelectBox = $(`#dx${_propertyName}SelectBox`).dxSelectBox("instance");
                if (_dxPropetySelectBox) {
                    $(`#dx${_propertyName}SelectBox`).dxSelectBox("instance").option("isValid", false);
                }
                const _dxPropetyLookup = $(`#dx${_propertyName}Lookup`).dxLookup("instance");
                if (_dxPropetyLookup) {
                    $(`#dx${_propertyName}Lookup`).dxLookup("instance").option("isValid", false);
                    toastr["error"](_validation_resultDTO.Description, _validation_resultDTO.Message);
                }
                const _dxPropetyComponentTextBox = $(`#dx${_propertyName}TextBox`).dxTextBox("instance");
                if (_dxPropetyComponentTextBox) {
                    $(`#dx${_propertyName}TextBox`).dxTextBox("instance").option("isValid", false);
                }

                const _dxPropetyComponentNumberBox = $(`#dx${_propertyName}NumberBox`).dxNumberBox("instance");
                if (_dxPropetyComponentNumberBox) {
                    $(`#dx${_propertyName}NumberBox`).dxNumberBox("instance").option("isValid", false);
                }

                const _dxPropetyComponentTextArea = $(`#dx${_propertyName}TextArea`).dxTextArea("instance");
                if (_dxPropetyComponentTextArea) {
                    $(`#dx${_propertyName}TextArea`).dxTextArea("instance").option("isValid", false);
                }
                //document.querySelector(`input[id*=${_propertyName}]`).classList.add("is-invalid");
                //document.getElementById(_propertyValidate).classList.add("is-invalid");
            }
            if (document.getElementById(_propertyValidate)) {
                document.getElementById(_propertyValidate).innerHTML = _validation_resultDTO.Message;
                document.getElementById(_propertyValidate).style.display = 'block';
            }

        }
        else {
            toastr["error"](Validation_resultDTO.ValidationResultList[_index].Description, Validation_resultDTO.ValidationResultList[_index].Message);
        }
    }
}

export function ClearErrorFeedback() {
    let _validationList = document.querySelectorAll("[id*='Validation']");
    let _inputList = document.querySelectorAll("input[id*='Name'], input[id*='Email'], input[id*='Login'], input[id*='Endpoint']");
    let _selectBoxList = document.querySelectorAll("[id*='SelectBox']");
    let _lookupList = document.querySelectorAll("[id*='Lookup']");
    let _textBoxList = document.querySelectorAll("[id*='TextBox']");
    let _tagBoxList = document.querySelectorAll("[id*='TagBox']");
    let _textAreaList = document.querySelectorAll("[id*='TextArea']");
    let _numberBoxList = document.querySelectorAll("[id*='NumberBox']");
    if (_validationList != null && _validationList.length > 0) {
        for (let i = 0; i < _validationList.length; i++) {
            _validationList[i].style.display = 'none';

        }
    }
    if (_inputList.length > 0) {
        for (let i = 0; i < _inputList.length; i++) {
            _inputList[i].classList.remove("is-invalid");
        }
    }
    if (_selectBoxList.length > 0) {
        for (var i = 0; i < _selectBoxList.length; i++) {
            var _nameID = _selectBoxList[i].getAttribute('id');
            $(`#${_nameID}`).dxSelectBox("instance").option("isValid", true);
        }
    }
    if (_lookupList.length > 0) {
        for (var i = 0; i < _lookupList.length; i++) {
            var _nameID = _lookupList[i].getAttribute('id');
            $(`#${_nameID}`).dxLookup("instance").option("isValid", true);
        }
    }
    if (_tagBoxList.length > 0) {
        for (let i = 0; i < _tagBoxList.length; i++) {
            var _nameID = _tagBoxList[i].getAttribute('id');
            $(`#${_nameID}`).dxTagBox("instance").option("isValid", true);
        }
    }
    if (_textBoxList.length > 0) {
        for (let i = 0; i < _textBoxList.length; i++) {
            var _nameID = _textBoxList[i].getAttribute('id');
            const _isHidden = document.getElementById(_nameID).hidden;
            if (!_isHidden)
                $(`#${_nameID}`).dxTextBox("instance").option("isValid", true);
        }
    }
    if (_textAreaList.length > 0) {
        for (let i = 0; i < _textAreaList.length; i++) {
            var _nameID = _textAreaList[i].getAttribute('id');
            $(`#${_nameID}`).dxTextArea("instance").option("isValid", true);
        }
    }
    if (_numberBoxList.length > 0) {
        for (let i = 0; i < _numberBoxList.length; i++) {
            var _nameID = _numberBoxList[i].getAttribute('id');
            const _isHidden = document.getElementById(_nameID).hidden;
            if (!_isHidden)
                $(`#${_nameID}`).dxNumberBox("instance").option("isValid", true);
        }
    }
}