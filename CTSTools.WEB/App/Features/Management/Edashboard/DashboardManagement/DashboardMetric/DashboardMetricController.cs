using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.DashboardMetric;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.DashboardManagement.DashboardMetric;

public class DashboardMetricController : ApiController
{
    [HttpGet]
    [Route("api/DashboardMetric/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] DashboardMetricDTO DashboardMetricDTO)
    {
        var _pagedDashboardMetricDTO = new PagedResultDTO<DashboardMetricDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = DashboardMetricDTO
        };
        _pagedDashboardMetricDTO.DataList = DashboardMetric_Service.GetDashboardMetricList_Global(DashboardMetricDTO, _pagedDashboardMetricDTO);
        loadOptions.Skip = 0;
        var _dsLoader = DataSourceLoader.Load(_pagedDashboardMetricDTO.DataList, loadOptions);
        _dsLoader.totalCount = DashboardMetric_Service.GetDashboardMetricTotalCount(_pagedDashboardMetricDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/DashboardMetric/GetDashboardMetricWithUI")]
    public IHttpActionResult GetDashboardMetricWithUI([FromUri] DashboardMetricDTO DashboardMetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DashboardMetric_Service.GetDashboardMetricWithUI(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/DashboardMetric/GetDashboardMetricList")]
    public IHttpActionResult GetDashboardMetricList([FromUri] DashboardMetricDTO DashboardMetricDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = DashboardMetric_Service.GetDashboardMetricList_Global(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DashboardMetric/Create")]
    public IHttpActionResult CreateDashboardMetric([FromBody] DashboardMetricDTO DashboardMetricDTO)
    {

        DashboardMetricDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardMetric_Service.CreateDashboardMetric_Global(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardMetric/CreateFromMetricList")]
    public IHttpActionResult CreateDashboardMetricFromMetricList([FromBody] DashboardMetricDTO DashboardMetricDTO)
    {

        DashboardMetricDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardMetric_Service.CreateDashboardMetric_FromMetricList(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardMetric/Update")]
    public IHttpActionResult UpdateDashboardMetric([FromBody] DashboardMetricDTO DashboardMetricDTO)
    {

        DashboardMetricDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardMetric_Service.UpdateDashboardMetric_Global(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/DashboardMetric/Delete")]
    public IHttpActionResult DeleteDashboardMetric([FromBody] DashboardMetricDTO DashboardMetricDTO)
    {
        DashboardMetricDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardMetric_Service.DeleteDashboardMetric_Global(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/DashboardMetric/UpdateDashboardMetricOrder")]
    public IHttpActionResult UpdateDashboardMetricOrder([FromBody] DashboardMetricDTO DashboardMetricDTO)
    {

        DashboardMetricDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
        var _validationResultDTO = DashboardMetric_Service.UpdateDashboardMetricOrder_Global(DashboardMetricDTO);
        return Json(_validationResultDTO);
    }
}