using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardCategory;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.Settings.DashboardCategory;

public class DashboardCategoryController : ApiController
{
    [HttpGet]
    [Route("api/DashboardCategory/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _pagedDashboardCategoryDTO = new PagedResultDTO<DashboardCategoryDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DashboardCategoryDTO
        };
        _pagedDashboardCategoryDTO.DataList = DashboardCategory_Service.GetDashboardCategoryList_Global(DashboardCategoryDTO, _pagedDashboardCategoryDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDashboardCategoryDTO.DataList, loadOptions);
        _dsLoader.totalCount = DashboardCategory_Service.GetDashboardCategoryTotalCount(_pagedDashboardCategoryDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DashboardCategory/GetList")]
    public IHttpActionResult GetDashboardCategoryList([FromUri] DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DashboardCategory_Service.GetDashboardCategoryList_Global(DashboardCategoryDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DashboardCategory/Create")]
    public IHttpActionResult CreateDashboardCategory([FromBody] DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DashboardCategory), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DashboardCategoryDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DashboardCategory_Service.CreateDashboardCategory_Global(DashboardCategoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardCategory/Update")]
    public IHttpActionResult UpdateDashboardCategory([FromBody] DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DashboardCategory), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DashboardCategoryDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = DashboardCategory_Service.UpdateDashboardCategory_Global(DashboardCategoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardCategory/Delete")]
    public IHttpActionResult DeleteDashboardCategory([FromBody] DashboardCategoryDTO DashboardCategoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(DashboardCategory), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = DashboardCategory_Service.DeleteDashboardCategory_Global(DashboardCategoryDTO);
        }
        return Json(_validationResultDTO);
    }
}