using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.SparePartManagement.SparePartInventory;

public class SparePartInventoryController : ApiController
{
    [HttpGet]
    [Route("api/SparePartInventory/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SparePartInventoryDTO SparePartInventoryDTO)
    {

        var _pagedSparePartInventoryDTO = new PagedResultDTO<SparePartInventoryDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SparePartInventoryDTO
        };
        _pagedSparePartInventoryDTO.DataList = SparePartInventory_Service.GetSparePartInventoryList_Global(SparePartInventoryDTO, _pagedSparePartInventoryDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSparePartInventoryDTO.DataList, loadOptions);
        _dsLoader.totalCount = SparePartInventory_Service.GetSparePartInventoryTotalCount(_pagedSparePartInventoryDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/SparePartInventory/GetList")]
    public IHttpActionResult GetSparePartInventoryList([FromUri] SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SparePartInventory_Service.GetSparePartInventoryList_Global(SparePartInventoryDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SparePartInventory/Create")]
    public IHttpActionResult CreateSparePartInventory([FromBody] SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartInventoryDTO.SupportGroupDTO, nameof(SparePartInventory), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SparePartInventoryDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartInventory_Service.CreateSparePartInventory_Global(SparePartInventoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePartInventory/Update")]
    public IHttpActionResult UpdateSparePartInventory([FromBody] SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartInventoryDTO.SupportGroupDTO, nameof(SparePartInventory), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SparePartInventoryDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartInventory_Service.UpdateSparePartInventory_Global(SparePartInventoryDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePartInventory/Delete")]
    public IHttpActionResult DeleteSparePartInventory([FromBody] SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePartInventoryDTO.SupportGroupDTO, nameof(SparePartInventory), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            SparePartInventoryDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePartInventory_Service.DeleteSparePartInventory_Global(SparePartInventoryDTO);
        }
        return Json(_validationResultDTO);
    }
}