using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Security.Roles.RoleType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.RoleType;

public class RoleTypeController : ApiController
{
    [HttpGet]
    [Route("api/RoleType/GetPagedList")]
    public IHttpActionResult GetRoleTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] RoleTypeDTO RoleTypeDTO)
    {
        var _pagedRoleTypeDTO = new PagedResultDTO<RoleTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = RoleTypeDTO
        };
        _pagedRoleTypeDTO.DataList = RoleType_Service.GetRoleTypeList_Global(RoleTypeDTO, _pagedRoleTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedRoleTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = RoleType_Service.GetRoleTypeTotalCount(_pagedRoleTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/RoleType/GetList")]
    public IHttpActionResult GetRoleTypeList([FromUri] RoleTypeDTO RoleTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = RoleType_Service.GetRoleTypeList_Global(RoleTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/RoleType/Create")]
    public IHttpActionResult CreateRoleType([FromBody] RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(RoleType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            RoleTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = RoleType_Service.CreateRoleType_Global(RoleTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/RoleType/Update")]
    public IHttpActionResult UpdateRoleType([FromBody] RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(RoleType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            RoleTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = RoleType_Service.UpdateRoleType_Global(RoleTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/RoleType/Delete")]
    public IHttpActionResult DeleteRoleType([FromBody] RoleTypeDTO RoleTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(RoleType), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = RoleType_Service.DeleteRoleType_Global(RoleTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}