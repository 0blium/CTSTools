export const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
});

export async function GetFileDTO(file) {
    let base65 = await toBase64(file)
    let FileDTO = {
        Data: base65,
        Name: file.name
    }
    return FileDTO;
}