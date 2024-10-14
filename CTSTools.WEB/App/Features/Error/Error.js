import { GetURLParameter } from '../../Common/Utils/Utils.js'

document.addEventListener("DOMContentLoaded", async () => {
    let _errorParameter = GetURLParameter("Error");
    await BuildErrorMessage(_errorParameter);
});

function BuildErrorMessage(ErrorParameter) {
    switch (ErrorParameter) {
        case "AD_401":
            document.getElementById("ErrorMessage").innerText = "Action Denied :(";
            document.getElementById("ErrorDescription").innerText = "Please contact the System administration if this is an error";
            break;
        case "NA_@403":
            document.getElementById("ErrorMessage").innerText = "Access denied for this screen :(";
            document.getElementById("ErrorDescription").innerText = "Please contact the System administration if this is an error";
            break;
        case "NEA_E401":
            document.getElementById("ErrorMessage").innerText = "Access denied :(";
            document.getElementById("ErrorDescription").innerText = "Please contact the System administration if this is an error";
            break;
    }
}