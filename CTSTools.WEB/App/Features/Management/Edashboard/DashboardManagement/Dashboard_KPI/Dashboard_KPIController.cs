using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.Dashboard_KPI;

public class DashboardKPIController : ApiController
{
    [HttpGet]
    [Route("api/Dashboard_KPI/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        var _pagedDashboardKPIDTO = new PagedResultDTO<Dashboard_KPIDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = Dashboard_KPIDTO
        };
        _pagedDashboardKPIDTO.DataList = Dashboard_KPI_Service.GetDashboard_KPIList_Global(Dashboard_KPIDTO, _pagedDashboardKPIDTO);
        loadOptions.Skip = 0;
        var _dsLoader = DataSourceLoader.Load(_pagedDashboardKPIDTO.DataList, loadOptions);
        _dsLoader.totalCount = Dashboard_KPI_Service.GetDashboardKPITotalCount(_pagedDashboardKPIDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Dashboard_KPI/GetDashboard_KPIWithUI")]
    public IHttpActionResult GetDashboardKPIWithUI([FromUri] Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Dashboard_KPI_Service.GetDashboard_KPIWithUI(DashboardKPIDTO);
        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/Dashboard_KPI/GetDashboard_KPIList")]
    public IHttpActionResult GetDashboardKPIList([FromUri] Dashboard_KPIDTO DashboardKPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Dashboard_KPI_Service.GetDashboard_KPIList_Global(DashboardKPIDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Dashboard_KPI/Create")]
    public IHttpActionResult CreateDashboardKPI([FromBody] Dashboard_KPIDTO DashboardKPIDTO)
    {

        DashboardKPIDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Dashboard_KPI_Service.CreateDashboard_KPI_Global(DashboardKPIDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Dashboard_KPI/CreateFromKPIList")]
    public IHttpActionResult CreateDashboardKPIFromKPIList([FromBody] Dashboard_KPIDTO DashboardKPIDTO)
    {

        DashboardKPIDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Dashboard_KPI_Service.CreateDashboard_KPI_FromKPIList(DashboardKPIDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Dashboard_KPI/Update")]
    public IHttpActionResult UpdateDashboardKPI([FromBody] Dashboard_KPIDTO Dashboard_KPIDTO)
    {

        Dashboard_KPIDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Dashboard_KPI_Service.UpdateDashboard_KPI_Global(Dashboard_KPIDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Dashboard_KPI/Delete")]
    public IHttpActionResult DeleteDashboardKPI([FromBody] Dashboard_KPIDTO Dashboard_KPIDTO)
    {
        Dashboard_KPIDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Dashboard_KPI_Service.DeleteDashboard_KPI_Global(Dashboard_KPIDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Dashboard_KPI/UpdateDashboard_KPIOrder")]
    public IHttpActionResult UpdateDashboardKPIOrder([FromBody] Dashboard_KPIDTO Dashboard_KPIDTO)
    {

        Dashboard_KPIDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = Dashboard_KPI_Service.UpdateDashboard_KPIOrder_Global(Dashboard_KPIDTO);
        return Json(_validationResultDTO);
    }
}