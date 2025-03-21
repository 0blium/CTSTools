using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Station.Station;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.StationManagement.Station;

public class StationController : ApiController
{
    [HttpGet]
    [Route("api/Station/GetPagedList")]
    public IHttpActionResult GetStationPagedList(DataSourceLoadOptions loadOptions, [FromUri] StationDTO StationDTO)
    {
        var _pagedStationDTO = new PagedResultDTO<StationDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = StationDTO
        };
        _pagedStationDTO.DataList = Station_Service.GetStationList_Global(StationDTO, _pagedStationDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedStationDTO.DataList, loadOptions);
        _dsLoader.totalCount = Station_Service.GetStationTotalCount(_pagedStationDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Station/GetStationList")]
    public IHttpActionResult GetStationList([FromUri] StationDTO StationDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Station_Service.GetStationList_Global(StationDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Station/GetStationListByFilter")]
    public IHttpActionResult GetStationListByFilter([FromBody] StationDTO StationDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Station_Service.GetStationList_Global(StationDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Station/Create")]
    public IHttpActionResult CreateStation([FromBody] StationDTO StationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Station), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            StationDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Station_Service.CreateStation_Global(StationDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Station/Update")]
    public IHttpActionResult UpdateStation([FromBody] StationDTO StationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Station), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            StationDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Station_Service.UpdateStation_Global(StationDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Station/Delete")]
    public IHttpActionResult DeleteStation([FromBody] StationDTO StationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Station), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Station_Service.DeleteStation_Global(StationDTO);
        }
        return Json(_validationResultDTO);
    }
}