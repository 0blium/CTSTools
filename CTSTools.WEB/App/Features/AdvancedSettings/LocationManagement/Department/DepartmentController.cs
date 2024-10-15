using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.LocationManagement.Department;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

//Department catalog testing still pending, no services have been added yet in BLL layer

namespace CTSTools.WEB.App.Features.AdvancedSettings.LocationManagement.Department;

public class DepartmentController : ApiController
{
    [HttpGet]
    [Route("api/Department/GetPagedList")]
    public IHttpActionResult GetDepartmentPagedList(DataSourceLoadOptions loadOptions, [FromUri] DepartmentDTO DepartmentDTO)
    {
        var _pagedDepartmentDTO = new PagedResultDTO<DepartmentDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DepartmentDTO
        };
        _pagedDepartmentDTO.DataList = Department_Service.GetDepartmentList_Global(DepartmentDTO, _pagedDepartmentDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDepartmentDTO.DataList, loadOptions);
        _dsLoader.totalCount = Department_Service.GetDepartmentTotalCount(_pagedDepartmentDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Department/GetList")]
    public IHttpActionResult GetDepartmentList([FromUri] DepartmentDTO DepartmentDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Department_Service.GetDepartmentList_Global(DepartmentDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Department/Create")]
    public IHttpActionResult CreateDepartment([FromBody] DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Department), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DepartmentDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Department_Service.CreateDepartmentWithResponsibles(DepartmentDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Department/Update")]
    public IHttpActionResult UpdateDepartment([FromBody] DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Department), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DepartmentDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Department_Service.UpdateDepartmentWithResponsibles(DepartmentDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Department/Delete")]
    public IHttpActionResult DeleteDepartment([FromBody] DepartmentDTO DepartmentDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Department), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Department_Service.DeleteDepartment_Global(DepartmentDTO);
        }
        return Json(_validationResultDTO);
    }
}