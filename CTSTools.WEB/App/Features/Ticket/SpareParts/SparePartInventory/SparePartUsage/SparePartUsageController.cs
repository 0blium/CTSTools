using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartUsage;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTSTools.WEB.App.Features.Ticket.SpareParts.SparePartInventory.SparePartUsage;

public class SparePartUsageController : ApiController
{
    [HttpGet]
    [Route("api/SparePartUsage/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SparePartUsageDTO SparePartUsageDTO)
    {

        var _pagedSparePartUsageDTO = new PagedResultDTO<SparePartUsageDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SparePartUsageDTO
        };
        _pagedSparePartUsageDTO.DataList = SparePartUsage_Service.GetSparePartUsageList_Global(SparePartUsageDTO, _pagedSparePartUsageDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSparePartUsageDTO.DataList, loadOptions);
        _dsLoader.totalCount = SparePartUsage_Service.GetSparePartUsageTotalCount(_pagedSparePartUsageDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/SparePartUsage/GetList")]
    public IHttpActionResult GetSparePartUsageList([FromUri] SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SparePartUsage_Service.GetSparePartUsageList_Global(SparePartUsageDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SparePartUsage/Create")]
    public IHttpActionResult CreateSparePartUsage([FromBody] SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartUsageDTO.SupportGroupDTO, nameof(SparePartUsage), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SparePartUsageDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartUsage_Service.CreateSparePartUsage_Global(SparePartUsageDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePartUsage/Update")]
    public IHttpActionResult UpdateSparePartUsage([FromBody] SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartUsageDTO.SupportGroupDTO, nameof(SparePartUsage), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SparePartUsageDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartUsage_Service.UpdateSparePartUsage_Global(SparePartUsageDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePartUsage/Delete")]
    public IHttpActionResult DeleteSparePartUsage([FromBody] SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartUsageDTO.SupportGroupDTO, nameof(SparePartUsage), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            SparePartUsageDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartUsage_Service.DeleteSparePartUsage_Global(SparePartUsageDTO);
        }
        return Json(_validationResultDTO);
    }
}