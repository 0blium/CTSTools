using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.SparePart.SparePart;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.SparePartManagement.SparePart;

public class SparePartController : ApiController
{
    [HttpGet]
    [Route("api/SparePart/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] SparePartDTO SparePartDTO)
    {

        var _pagedSparePartDTO = new PagedResultDTO<SparePartDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = SparePartDTO
        };
        _pagedSparePartDTO.DataList = SparePart_Service.GetSparePartList_Global(SparePartDTO, _pagedSparePartDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedSparePartDTO.DataList, loadOptions);
        _dsLoader.totalCount = SparePart_Service.GetSparePartTotalCount(_pagedSparePartDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/SparePart/GetList")]
    public IHttpActionResult GetSparePartList([FromUri] SparePartDTO SparePartDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = SparePart_Service.GetSparePartList_Global(SparePartDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/SparePart/Create")]
    public IHttpActionResult CreateSparePart([FromBody] SparePartDTO SparePartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SparePart), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            SparePartDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Service.CreateSparePart_Global(SparePartDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePart/Update")]
    public IHttpActionResult UpdateSparePart([FromBody] SparePartDTO SparePartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SparePart), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            SparePartDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Service.UpdateSparePart_Global(SparePartDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/SparePart/Delete")]
    public IHttpActionResult DeleteSparePart([FromBody] SparePartDTO SparePartDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(SparePart), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            SparePartDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = SparePart_Service.DeleteSparePart_Global(SparePartDTO);
        }
        return Json(_validationResultDTO);
    }
}