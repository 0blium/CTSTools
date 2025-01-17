import { dxLoadPanel } from '../../../Common/Components/dxLoadPanel.js'
import { HostResponse, ClearErrorFeedback } from '../../../Common/Utils/Response.js'
import { CreatePartNumber } from '../MassiveManagement/MassivePartNumber_Service.js'

document.addEventListener("DOMContentLoaded", () => {
    InitializeMassivePartNumberCatalogControls();
});

async function InitializeMassivePartNumberCatalogControls() {
    $("#file-uploader").dxFileUploader({
        accept: ".xlsx", // Filtra solo archivos de Excel
        uploadMode: "instantly", // Subida instantánea al seleccionarlos
        onValueChanged: function (e) {
            debugger;
            var file = e.value[0];  // e.value es un array, así que seleccionamos el primer archivo
            let _fileDTO;
            if (file) {
                // Obtén el nombre del archivo
                var filename = file.name;
                // Convertir el archivo a Base64
                var reader = new FileReader();
                reader.onload = function (readerEvent) {
                    debugger;
                    var base64File = readerEvent.target.result;
                    // Asignar a las propiedades que necesitas
                    _fileDTO = {
                        FileName: filename,  // Nombre del archivo
                        Data: base64File     // El contenido en Base64 (que es un string)
                    };
                    console.log(CreatePartNumber(_fileDTO));
                };
                reader.readAsDataURL(file);  // Convierte el archivo a Base64
            }
        }
    });
}