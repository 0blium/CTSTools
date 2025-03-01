using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Station.StationType;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.Station.StationType;

public class StationTypeController : ApiController
{
    [HttpGet]
    [Route("api/StationType/GetPagedList")]
    public IHttpActionResult GetStationTypePagedList(DataSourceLoadOptions loadOptions, [FromUri] StationTypeDTO StationTypeDTO)
    {
        var _pagedStationTypeDTO = new PagedResultDTO<StationTypeDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = StationTypeDTO
        };
        _pagedStationTypeDTO.DataList = StationType_Service.GetStationTypeList_Global(StationTypeDTO, _pagedStationTypeDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedStationTypeDTO.DataList, loadOptions);
        _dsLoader.totalCount = StationType_Service.GetStationTypeTotalCount(_pagedStationTypeDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/StationType/GetList")]
    public IHttpActionResult GetStationTypeList([FromUri] StationTypeDTO StationTypeDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = StationType_Service.GetStationTypeList_Global(StationTypeDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StationType/Create")]
    public IHttpActionResult CreateStationType([FromBody] StationTypeDTO StationTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(StationType), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            StationTypeDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = StationType_Service.CreateStationType_Global(StationTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StationType/Update")]
    public IHttpActionResult UpdateStationType([FromBody] StationTypeDTO StationTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(StationType), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            StationTypeDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = StationType_Service.UpdateStationType_Global(StationTypeDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/StationType/Delete")]
    public IHttpActionResult DeleteStationType([FromBody] StationTypeDTO StationTypeDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(StationType), (int)Action_Enum.Delete);

        if (_validationResultDTO.Result)
        {
            _validationResultDTO = StationType_Service.DeleteStationType_Global(StationTypeDTO);
        }
        return Json(_validationResultDTO);
    }
}