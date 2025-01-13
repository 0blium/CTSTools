using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.MailGroupManagement.MailGroup;

public class MailGroupController : ApiController
{
    [HttpGet]
    [Route("api/MailGroup/GetPagedList")]
    public IHttpActionResult GetMailGroupPagedList(DataSourceLoadOptions loadOptions, [FromUri] MailGroupDTO MailGroupDTO)
    {
        var _pagedMailGroupDTO = new PagedResultDTO<MailGroupDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = MailGroupDTO
        };
        _pagedMailGroupDTO.DataList = MailGroup_Service.GetMailGroupList_Global(MailGroupDTO, _pagedMailGroupDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedMailGroupDTO.DataList, loadOptions);
        _dsLoader.totalCount = MailGroup_Service.GetMailGroupTotalCount(_pagedMailGroupDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/MailGroup/GetList")]
    public IHttpActionResult GetMailGroupList([FromUri] MailGroupDTO MailGroupDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = MailGroup_Service.GetMailGroupList_Global(MailGroupDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/MailGroup/Create")]
    public IHttpActionResult CreateMailGroup([FromBody] MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroup), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            MailGroupDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = MailGroup_Service.CreateMailGroup_Global(MailGroupDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/MailGroup/Update")]
    public IHttpActionResult UpdateMailGroup([FromBody] MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroup), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            MailGroupDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = MailGroup_Service.UpdateMailGroup_Global(MailGroupDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/MailGroup/Delete")]
    public IHttpActionResult DeleteMailGroup([FromBody] MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroup), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = MailGroup_Service.DeleteMailGroup_Global(MailGroupDTO);
        }
        return Json(_validationResultDTO);
    }
}