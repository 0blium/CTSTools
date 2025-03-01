using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.Ticket.Item.SupportGroup;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.SupportGroup;

public class SupportGroupController : ApiController
{
    [HttpGet]
    [Route("api/SupportGroup/GetPagedList")]
    public IHttpActionResult GetSupportGroupPagedList(DataSourceLoadOptions loadOptions, [FromUri] SupportGroupDTO SupportGroupDTO)
    {
        var _pagedSupportGroupDTO = new PagedResultDTO<SupportGroupDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SupportGroupDTO
        };
        _pagedSupportGroupDTO.DataList = SupportGroup_Service.GetSupportGroupList_Global(SupportGroupDTO, _pagedSupportGroupDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSupportGroupDTO.DataList, loadOptions);
        _dsLoader.totalCount = SupportGroup_Service.GetSupportGroupTotalCount(_pagedSupportGroupDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/SupportGroup/GetList")]
    public IHttpActionResult GetSupportGroupList([FromUri] SupportGroupDTO SupportGroupDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SupportGroup_Service.GetSupportGroupList_Global(SupportGroupDTO);

        return Json(_validationResultDTO);
    }

    [HttpGet]
    [Route("api/SupportGroup/GetListByUser")]
    public IHttpActionResult GetSupportGroupListByUser()
    {
        var _userDTO = new UserDTO { ID = Auth_Helper.GetLoggedUserOid() };

        var _supportGroupList = SupportGroup_Service.GetSupportGroupByUser(_userDTO);

        return Json(_supportGroupList);
    }

    [HttpPost]
    [Route("api/SupportGroup/Create")]
    public IHttpActionResult CreateSupportGroup([FromBody] SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupportGroup), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SupportGroupDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SupportGroup_Service.CreateSupportGroup_Global(SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SupportGroup/Update")]
    public IHttpActionResult UpdateSupportGroup([FromBody] SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupportGroup), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SupportGroupDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SupportGroup_Service.UpdateSupportGroup_Global(SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SupportGroup/Delete")]
    public IHttpActionResult DeleteSupportGroup([FromBody] SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SupportGroup), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            SupportGroupDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SupportGroup_Service.DeleteSupportGroup_Global(SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }
}