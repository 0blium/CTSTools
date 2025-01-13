using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.Dashboard;

public class DashboardController : ApiController
{
    [HttpGet]
    [Route("api/Dashboard/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DashboardDTO DashboardDTO)
    {

        var _pagedDashboardDTO = new PagedResultDTO<DashboardDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DashboardDTO
        };
        _pagedDashboardDTO.DataList = Dashboard_Service.GetDashboardList_Global(DashboardDTO, _pagedDashboardDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedDashboardDTO.DataList, loadOptions);
        _dsLoader.totalCount = Dashboard_Service.GetDashboardTotalCount(_pagedDashboardDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Dashboard/GetDashboardList")]
    public IHttpActionResult GetDashboardList([FromUri] DashboardDTO DashboardDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Dashboard_Service.GetDashboardList_Global(DashboardDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Dashboard/Create")]
    public IHttpActionResult CreateDashboard([FromBody] DashboardDTO DashboardDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Dashboard), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            DashboardDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Dashboard_Service.CreateDashboard_Global(DashboardDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Dashboard/Update")]
    public IHttpActionResult UpdateDashboard([FromBody] DashboardDTO DashboardDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Dashboard), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            DashboardDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Dashboard_Service.UpdateDashboard_Global(DashboardDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Dashboard/Delete")]
    public IHttpActionResult DeleteDashboard([FromBody] DashboardDTO DashboardDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Dashboard), (int)Action_Enum.Delete);
        if (!_validationResultDTO.Result)
        {
            DashboardDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Dashboard_Service.DeleteDashboard_Global(DashboardDTO);
        }        
        return Json(_validationResultDTO);
    }
}