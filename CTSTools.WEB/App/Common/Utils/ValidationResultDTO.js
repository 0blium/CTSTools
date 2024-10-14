export const ValidationResultDTO = (() => {
    let Result = true;
    let Message = "";
    let Description = "";
    let Data;
    let ValidationResultList = Array(Object);

    return {
        Result: Result,
        Message: Message,
        Description: Description,
        Data: Data,
        ValidationResultList: ValidationResultList
    }
})();