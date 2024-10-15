using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Linq;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.UserManagement.User;

public class UserController : ApiController
{
    [HttpGet]
    [Route("api/User/GetPagedList")]
    public IHttpActionResult GetUserPagedList(DataSourceLoadOptions loadOptions, [FromUri] UserDTO UserDTO)
    {
        var _pagedUserDTO = new PagedResultDTO<UserDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = UserDTO
        };
        _pagedUserDTO.DataList = User_Service.GetUserList_Global(UserDTO, _pagedUserDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedUserDTO.DataList, loadOptions);
        _dsLoader.totalCount = User_Service.GetUserTotalCount(_pagedUserDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/User/GetList")]
    public IHttpActionResult GetUserList([FromUri] UserDTO UserDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = User_Service.GetUserList_Global(UserDTO);

        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/User/GetUserInformation")]
    public IHttpActionResult GetUserInformation()
    {
        var _userDTO = User_Service.GetUserList_Global(new UserDTO 
        {
            Login=Auth_Helper.GetDomainLogin() 
        }).FirstOrDefault();

        return Json(_userDTO);
    }

    [HttpPost]
    [Route("api/User/Create")]
    public IHttpActionResult CreateUser([FromBody] UserDTO UserDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Users), (int)Action_Enum.Create);
        //if (_validationResultDTO.Result)
        //{
        //    UserDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        //    _validationResultDTO = User_Service.CreateUser_Global(UserDTO);
        //}
        //return Json(_validationResultDTO);

        var _validationResultDTO = new ValidationResultDTO();
        UserDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        _validationResultDTO = User_Service.CreateUser_Global(UserDTO);

        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/User/Update")]
    public IHttpActionResult UpdateUser([FromBody] UserDTO UserDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(User), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = User_Service.UpdateUser_Global(UserDTO);
        }
        return Json(_validationResultDTO);
    }
   
}