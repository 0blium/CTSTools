using CTSTools.BLL.Common.Files;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.MassiveManagement;

public class MassivePartNumberController : ApiController
{
    [HttpPost]
    [Route("api/MassivePartNumber/Create")]
    public IHttpActionResult CreateMassivePartNumber([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = ExcelDataImport_Service.PartNumberFileValidation_Global(FileDTO.Data, FileDTO.FileName);
        return Json(_validationResultDTO);
    }
}