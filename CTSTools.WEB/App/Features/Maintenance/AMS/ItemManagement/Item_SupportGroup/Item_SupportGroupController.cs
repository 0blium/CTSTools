using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Item.Item_SupportGroup;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.Item_SupportGroup;

public class Item_SupportGroupController : ApiController
{
    [HttpGet]
    [Route("api/Item_SupportGroup/GetPagedList")]
    public IHttpActionResult Get(DataSourceLoadOptions loadOptions, [FromUri] Item_SupportGroupDTO Item_SupportGroupDTO)
    {

        var _pagedItem_SupportGroupDTO = new PagedResultDTO<Item_SupportGroupDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = Item_SupportGroupDTO
        };
        _pagedItem_SupportGroupDTO.DataList = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(Item_SupportGroupDTO, _pagedItem_SupportGroupDTO);
        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedItem_SupportGroupDTO.DataList, loadOptions);
        _dsLoader.totalCount = Item_SupportGroup_Service.GetItem_SupportGroupTotalCount(_pagedItem_SupportGroupDTO);
        return Json(_dsLoader);
    }
    [HttpGet]
    [Route("api/Item_SupportGroup/GetList")]
    public IHttpActionResult GetItem_SupportGroupList([FromUri] Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Item_SupportGroup_Service.GetItem_SupportGroupList_Global(Item_SupportGroupDTO);
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Item_SupportGroup/Create")]
    public IHttpActionResult CreateItem_SupportGroup([FromBody] Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_SupportGroup), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            Item_SupportGroupDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_SupportGroup_Service.CreateItem_SupportGroup_Global(Item_SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Item_SupportGroup/Update")]
    public IHttpActionResult UpdateItem_SupportGroup([FromBody] Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Item_SupportGroup), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_SupportGroupDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_SupportGroup_Service.UpdateItem_SupportGroup_Global(Item_SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }
    [HttpPost]
    [Route("api/Item_SupportGroup/Delete")]
    public IHttpActionResult DeleteItem_SupportGroup([FromBody] Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Item_SupportGroup), (int)Action_Enum.Delete);
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_SupportGroup), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            Item_SupportGroupDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_SupportGroup_Service.DeleteItem_SupportGroup_Global(Item_SupportGroupDTO);
        }
        return Json(_validationResultDTO);
    }
}