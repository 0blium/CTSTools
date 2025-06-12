using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.MailGroups.MailGroupMember;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.MailGroupManagement.MailGroupMember;

public class MailGroupMemberController : ApiController
{
    [HttpGet]
    [Route("api/MailGroupMember/GetPagedList")]
    public IHttpActionResult GetMailGroupMemberPagedList(DataSourceLoadOptions loadOptions, [FromUri] MailGroupMemberDTO MailGroupMemberDTO)
    {
        var _pagedMailGroupMemberDTO = new PagedResultDTO<MailGroupMemberDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = MailGroupMemberDTO
        };
        _pagedMailGroupMemberDTO.DataList = MailGroupMember_Service.GetMailGroupMemberList_Global(MailGroupMemberDTO, _pagedMailGroupMemberDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedMailGroupMemberDTO.DataList, loadOptions);
        _dsLoader.totalCount = MailGroupMember_Service.GetMailGroupMemberTotalCount(_pagedMailGroupMemberDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/MailGroupMember/GetList")]
    public IHttpActionResult GetMailGroupMemberList([FromUri] MailGroupMemberDTO MailGroupMemberDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = MailGroupMember_Service.GetMailGroupMemberList_Global(MailGroupMemberDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/MailGroupMember/Create")]
    public IHttpActionResult CreateMailGroupMember([FromBody] MailGroupMemberDTO MailGroupMemberDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroupMember), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            MailGroupMemberDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = MailGroupMember_Service.CreateMailGroupMember_Global(MailGroupMemberDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/MailGroupMember/CreateByGroups")]
    public IHttpActionResult CreateMailGroupMemberByGroups([FromBody] MailGroupMemberDTO MailGroupMemberDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroupMember), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            MailGroupMemberDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = MailGroupMember_Service.CreateMailGroupMemberByGroups_Global(MailGroupMemberDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/MailGroupMember/Update")]
    public IHttpActionResult UpdateMailGroupMember([FromBody] MailGroupMemberDTO MailGroupMemberDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroupMember), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            MailGroupMemberDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = MailGroupMember_Service.UpdateMailGroupMember_Global(MailGroupMemberDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/MailGroupMember/Delete")]
    public IHttpActionResult DeleteMailGroupMember([FromBody] MailGroupMemberDTO MailGroupMemberDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(MailGroupMember), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = MailGroupMember_Service.DeleteMailGroupMember_Global(MailGroupMemberDTO);
        }
        return Json(_validationResultDTO);
    }
}