using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Class;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.Class;
public class Class_Controller : ApiController
{

    [HttpGet]
    [Route("api/Class/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ClassDTO ClassDTO)
    {
        var _classList = Class_Service.GetClassList_Global(ClassDTO);
        loadOptions.Skip = 0;
        var _dsLoader = DataSourceLoader.Load(_classList, loadOptions);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Class/GetList")]
    public IHttpActionResult GetClassList([FromUri] ClassDTO ClassDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Class_Service.GetClassList_Global(ClassDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Class/Create")]
    public IHttpActionResult CreateClass([FromBody] ClassDTO ClassDTO)
    {

        ClassDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Class_Service.CreateClass_Global(ClassDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Class/Update")]
    public IHttpActionResult UpdateClass([FromBody] ClassDTO ClassDTO)
    {
        ClassDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Class_Service.UpdateClass_Global(ClassDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Class/Delete")]
    public IHttpActionResult DeleteClass([FromBody] ClassDTO ClassDTO)
    {
        var _validationResultDTO = Class_Service.DeleteClass_Global(ClassDTO);
        return Json(_validationResultDTO);
    }
}