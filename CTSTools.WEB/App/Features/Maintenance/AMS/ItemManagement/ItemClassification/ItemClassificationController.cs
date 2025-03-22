using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.ItemClassification;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.ItemClassification;

public class ItemClassificationController : ApiController
{
    [HttpGet]
    [Route("api/ItemClassification/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] ItemClassificationDTO ItemClassificationDTO)
    {

        var _pagedItemClassificationDTO = new PagedResultDTO<ItemClassificationDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = ItemClassificationDTO
        };
        _pagedItemClassificationDTO.DataList = ItemClassification_Service.GetItemClassificationList_Global(ItemClassificationDTO, _pagedItemClassificationDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedItemClassificationDTO.DataList, loadOptions);
        _dsLoader.totalCount = ItemClassification_Service.GetItemClassificationTotalCount(_pagedItemClassificationDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/ItemClassification/GetList")]
    public IHttpActionResult GetItemClassificationList([FromUri] ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = ItemClassification_Service.GetItemClassificationList_Global(ItemClassificationDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/ItemClassification/Create")]
    public IHttpActionResult CreateItemClassification([FromBody] ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemClassification), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            ItemClassificationDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = ItemClassification_Service.CreateItemClassification_Global(ItemClassificationDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ItemClassification/Update")]
    public IHttpActionResult UpdateItemClassification([FromBody] ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemClassification), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            ItemClassificationDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = ItemClassification_Service.UpdateItemClassification_Global(ItemClassificationDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/ItemClassification/Delete")]
    public IHttpActionResult DeleteItemClassification([FromBody] ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemClassification), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = ItemClassification_Service.DeleteItemClassification_Global(ItemClassificationDTO);
        }
        return Json(_validationResultDTO);
    }
}