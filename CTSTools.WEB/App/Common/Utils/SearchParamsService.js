export default async function BuildSearchParams(ObjectDTO) {
    let params = new URLSearchParams();
    if (ObjectDTO != null || ObjectDTO != "") {
        for (let key in ObjectDTO) {
            //console.log("loop")
            //console.log(key);
            if (Array.isArray(ObjectDTO[key])) {
                // Si es un arreglo, agregar cada elemento con el mismo nombre de parámetro,Array
                //console.log("Array")
                ObjectDTO[key].forEach(value => {
                    params.append(`${key}`, value);
                });
            } else if (typeof ObjectDTO[key] === 'object') {
                // Si es un objeto, iterar sobre sus propiedades y agregarlas con un prefijo,propiedad anidada
                for (let nestedKey in ObjectDTO[key]) {
                    // Si es un arreglo, agregar cada elemento con el mismo nombre de parámetro,Array
                    if (Array.isArray(ObjectDTO[key][nestedKey])) {
                        ObjectDTO[key][nestedKey].forEach(value => {
                            params.append(`${key}.${nestedKey}`, value);
                        });
                    } else {
                        params.append(`${key}.${nestedKey}`, ObjectDTO[key][nestedKey]);
                    }
                }
            } else {
                //console.log("Prop")
                // Si no es un objeto o arreglo, agregar el valor con el nombre de parámetro correspondiente,propiedad plana
                params.append(`${key}`, ObjectDTO[key]);
            }
        }
    }
    return params;
}