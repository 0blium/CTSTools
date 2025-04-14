using CTSTools.BLL.Common;
using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Management.Edashboard.DashboardManagement.KPI;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Management.Edashboard.KPIManagement.KPI;

public class KPIController : ApiController
{
    [HttpGet]
    [Route("api/KPI/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] KPIDTO KPIDTO)
    {

        var _pagedKPIDTO = new PagedResultDTO<KPIDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = KPIDTO
        };
        _pagedKPIDTO.DataList = KPI_Service.GetKPIList_Global(KPIDTO, _pagedKPIDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedKPIDTO.DataList, loadOptions);
        _dsLoader.totalCount = KPI_Service.GetKPITotalCount(_pagedKPIDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/KPI/GetKPIList")]
    public IHttpActionResult GetKPIList([FromUri] KPIDTO KPIDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = KPI_Service.GetKPIList_Global(KPIDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/KPI/Create")]
    public IHttpActionResult CreateKPI([FromBody] KPIDTO KPIDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(KPI), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            KPIDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = KPI_Service.CreateKPI_Global(KPIDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/KPI/CreateMassive")]
    public IHttpActionResult CreateMassiveKPI([FromBody] FileDTO FileDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(KPI), (int)Action_Enum.Import);
        if (_validationResultDTO.Result)
        {
            FileDTO.ID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = KPI_Service.GenerateKPIsFromExcel(FileDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/KPI/Update")]
    public IHttpActionResult UpdateKPI([FromBody] KPIDTO KPIDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(KPI), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            KPIDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = KPI_Service.UpdateKPI_Global(KPIDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/KPI/Delete")]
    public IHttpActionResult DeleteKPI([FromBody] KPIDTO KPIDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(KPI), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
             _validationResultDTO = KPI_Service.DeleteKPI_Global(KPIDTO);
        }
        return Json(_validationResultDTO);
    }
}