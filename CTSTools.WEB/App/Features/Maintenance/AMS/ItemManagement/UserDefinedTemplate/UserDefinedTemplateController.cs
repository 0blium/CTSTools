using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Item.UserDefinedTemplate;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.UserDefinedTemplate;

public class UserDefinedTemplateController : ApiController
{
    [HttpGet]
    [Route("api/UserDefinedTemplate/GetPagedList")]
    public IHttpActionResult GetUserDefinedTemplatePagedList(DataSourceLoadOptions loadOptions, [FromUri] UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _pagedUserDefinedTemplateDTO = new PagedResultDTO<UserDefinedTemplateDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = UserDefinedTemplateDTO
        };
        _pagedUserDefinedTemplateDTO.DataList = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(UserDefinedTemplateDTO, _pagedUserDefinedTemplateDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedUserDefinedTemplateDTO.DataList, loadOptions);
        _dsLoader.totalCount = UserDefinedTemplate_Service.GetUserDefinedTemplateTotalCount(_pagedUserDefinedTemplateDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/UserDefinedTemplate/GetList")]
    public IHttpActionResult GetUserDefinedTemplateList([FromUri] UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = UserDefinedTemplate_Service.GetUserDefinedTemplateList_Global(UserDefinedTemplateDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefinedTemplate/Create")]
    public IHttpActionResult CreateUserDefinedTemplate([FromBody] UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(UserDefinedTemplate), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            UserDefinedTemplateDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = UserDefinedTemplate_Service.CreateUserDefinedTemplate_Global(UserDefinedTemplateDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefinedTemplate/Update")]
    public IHttpActionResult UpdateUserDefinedTemplate([FromBody] UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(UserDefinedTemplate), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            UserDefinedTemplateDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = UserDefinedTemplate_Service.UpdateUserDefinedTemplate_Global(UserDefinedTemplateDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefinedTemplate/Delete")]
    public IHttpActionResult DeleteUserDefinedTemplate([FromBody] UserDefinedTemplateDTO UserDefinedTemplateDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedTemplateDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(UserDefinedTemplate), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = UserDefinedTemplate_Service.DeleteUserDefinedTemplate_Global(UserDefinedTemplateDTO);
        }
        return Json(_validationResultDTO);
    }
}