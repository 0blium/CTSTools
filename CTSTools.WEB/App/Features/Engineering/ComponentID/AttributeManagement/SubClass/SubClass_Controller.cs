using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.SubClass;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Engineering.ComponentID.AttributeManagement.SubClass;

public class SubClass_Controller : ApiController
{

    [HttpGet]
    [Route("api/SubClass/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SubClassDTO SubClassDTO)
    {
        var _classList = SubClass_Service.GetSubClassList_Global(SubClassDTO);
        loadOptions.Skip = 0;
        var _dsLoader = DataSourceLoader.Load(_classList, loadOptions);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/SubClass/GetList")]
    public IHttpActionResult GetSubClassList([FromUri] SubClassDTO SubClassDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SubClass_Service.GetSubClassList_Global(SubClassDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SubClass/Create")]
    public IHttpActionResult CreateSubClass([FromBody] SubClassDTO SubClassDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SubClassDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SubClass_Service.CreateSubClass_Global(SubClassDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SubClass/Update")]
    public IHttpActionResult UpdateSubClass([FromBody] SubClassDTO SubClassDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SubClassDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SubClass_Service.UpdateSubClass_Global(SubClassDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SubClass/Delete")]
    public IHttpActionResult DeleteSubClass([FromBody] SubClassDTO SubClassDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SubClass), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = SubClass_Service.DeleteSubClass_Global(SubClassDTO);
        }
        return Json(_validationResultDTO);
    }
}