export const GetURLParameter = (sParam) => {
    let _sPageURL = window.location.search.substring(1);
    let _sURLVariables = _sPageURL.split('&');
    let _sParameterName;

    for (let i = 0; i < _sURLVariables.length; i++) {
        _sParameterName = _sURLVariables[i].split('=');

        if (_sParameterName[0] === sParam) {
            return _sParameterName[1] === undefined ? true : decodeURIComponent(_sParameterName[1]);
        }
    }
};

