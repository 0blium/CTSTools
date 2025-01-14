using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User_Permission;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.User_Permission;

public class User_PermissionController : ApiController
{
    [HttpGet]
    [Route("api/User_Permission/GetPagedList")]
    public IHttpActionResult GetUser_PermissionPagedList(DataSourceLoadOptions loadOptions, [FromUri] User_PermissionDTO User_PermissionDTO)
    {
        var _pagedUser_PermissionDTO = new PagedResultDTO<User_PermissionDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = User_PermissionDTO
        };
        _pagedUser_PermissionDTO.DataList = User_Permission_Service.GetUser_PermissionList_Global(User_PermissionDTO, _pagedUser_PermissionDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedUser_PermissionDTO.DataList, loadOptions);
        _dsLoader.totalCount = User_Permission_Service.GetUser_PermissionTotalCount(_pagedUser_PermissionDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/User_Permission/GetList")]
    public IHttpActionResult GetUser_PermissionList([FromUri] User_PermissionDTO User_PermissionDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = User_Permission_Service.GetUser_PermissionList_Global(User_PermissionDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/User_Permission/Create")]
    public IHttpActionResult CreateUser_Permission([FromBody] User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(User_Permission), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            User_PermissionDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = User_Permission_Service.CreateUser_PermissionByArray(User_PermissionDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/User_Permission/Delete")]
    public IHttpActionResult DeleteUser_Permission([FromBody] User_PermissionDTO User_PermissionDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(User_Permission), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Permission_Service.DeleteUser_Permission_Global(User_PermissionDTO);
        }
        return Json(_validationResultDTO);
    }
}