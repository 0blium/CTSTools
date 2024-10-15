using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.RoleManagement.Role;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.SecurityManagement.Role;

public class RoleController : ApiController
{
    [HttpGet]
    [Route("api/Role/GetPagedList")]
    public IHttpActionResult GetRolePagedList(DataSourceLoadOptions loadOptions, [FromUri] RoleDTO RoleDTO)
    {
        var _pagedRoleDTO = new PagedResultDTO<RoleDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = RoleDTO
        };
        _pagedRoleDTO.DataList = Role_Service.GetRoleList_Global(RoleDTO, _pagedRoleDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedRoleDTO.DataList, loadOptions);
        _dsLoader.totalCount = Role_Service.GetRoleTotalCount(_pagedRoleDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Role/GetList")]
    public IHttpActionResult GetRoleList([FromUri] RoleDTO RoleDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Role_Service.GetRoleList_Global(RoleDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role/Create")]
    public IHttpActionResult CreateRole([FromBody] RoleDTO RoleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            RoleDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Role_Service.CreateRole_Global(RoleDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role/Update")]
    public IHttpActionResult UpdateRole([FromBody] RoleDTO RoleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            RoleDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Role_Service.UpdateRole_Global(RoleDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Role/Delete")]
    public IHttpActionResult DeleteRole([FromBody] RoleDTO RoleDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Role), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Role_Service.DeleteRole_Global(RoleDTO);
        }
        return Json(_validationResultDTO);
    }
}