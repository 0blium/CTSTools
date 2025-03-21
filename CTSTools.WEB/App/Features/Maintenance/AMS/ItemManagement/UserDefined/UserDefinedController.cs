using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Item.UserDefined;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefinedController : ApiController
{
    [HttpGet]
    [Route("api/UserDefined/GetPagedList")]
    public IHttpActionResult GetUserDefinedPagedList(DataSourceLoadOptions loadOptions, [FromUri] UserDefinedDTO UserDefinedDTO)
    {
        var _pagedUserDefinedDTO = new PagedResultDTO<UserDefinedDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = UserDefinedDTO
        };
        _pagedUserDefinedDTO.DataList = UserDefined_Service.GetUserDefinedList_Global(UserDefinedDTO, _pagedUserDefinedDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedUserDefinedDTO.DataList, loadOptions);
        _dsLoader.totalCount = UserDefined_Service.GetUserDefinedTotalCount(_pagedUserDefinedDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/UserDefined/GetList")]
    public IHttpActionResult GetUserDefinedList([FromUri] UserDefinedDTO UserDefinedDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = UserDefined_Service.GetUserDefinedList_Global(UserDefinedDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefined/Create")]
    public IHttpActionResult CreateUserDefined([FromBody] UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedDTO.SupportGroupDTO, nameof(UserDefined), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            UserDefinedDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = UserDefined_Service.CreateUserDefined_Global(UserDefinedDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefined/Update")]
    public IHttpActionResult UpdateUserDefined([FromBody] UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedDTO.SupportGroupDTO, nameof(UserDefined), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            UserDefinedDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = UserDefined_Service.UpdateUserDefined_Global(UserDefinedDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/UserDefined/Delete")]
    public IHttpActionResult DeleteUserDefined([FromBody] UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(UserDefinedDTO.SupportGroupDTO, nameof(UserDefined), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = UserDefined_Service.DeleteUserDefined_Global(UserDefinedDTO);
        }
        return Json(_validationResultDTO);
    }
}