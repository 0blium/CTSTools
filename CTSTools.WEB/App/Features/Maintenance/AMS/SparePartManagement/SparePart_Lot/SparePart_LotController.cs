using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_LotController : ApiController
{
    [HttpGet]
    [Route("api/SparePart_Lot/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SparePart_LotDTO SparePart_LotDTO)
    {

        var _pagedSparePart_LotDTO = new PagedResultDTO<SparePart_LotDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SparePart_LotDTO
        };
        _pagedSparePart_LotDTO.DataList = SparePart_Lot_Service.GetSparePart_LotList_Global(SparePart_LotDTO, _pagedSparePart_LotDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSparePart_LotDTO.DataList, loadOptions);
        _dsLoader.totalCount = SparePart_Lot_Service.GetSparePart_LotTotalCount(_pagedSparePart_LotDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/SparePart_Lot/GetList")]
    public IHttpActionResult GetSparePart_LotList([FromUri] SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SparePart_Lot_Service.GetSparePart_LotList_Global(SparePart_LotDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SparePart_Lot/Create")]
    public IHttpActionResult CreateSparePart_Lot([FromBody] SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePart_LotDTO.SupportGroupDTO, nameof(SparePart_Lot), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SparePart_LotDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Lot_Service.CreateSparePart_Lot_Global(SparePart_LotDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePart_Lot/Update")]
    public IHttpActionResult UpdateSparePart_Lot([FromBody] SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePart_LotDTO.SupportGroupDTO, nameof(SparePart_Lot), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SparePart_LotDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Lot_Service.UpdateSparePart_Lot_Global(SparePart_LotDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePart_Lot/Delete")]
    public IHttpActionResult DeleteSparePart_Lot([FromBody] SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(SparePart_LotDTO.SupportGroupDTO, nameof(SparePart_Lot), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            SparePart_LotDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Lot_Service.DeleteSparePart_Lot_Global(SparePart_LotDTO);
        }
        return Json(_validationResultDTO);
    }
}