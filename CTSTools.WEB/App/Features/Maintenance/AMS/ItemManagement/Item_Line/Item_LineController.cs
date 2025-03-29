using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_LineController : ApiController
{
    #region CRUD
    [HttpGet]
    [Route("api/Item_Line/GetPagedList")]
    public IHttpActionResult GetItem_LinePagedList(DataSourceLoadOptions loadOptions, [FromUri] Item_LineDTO Item_LineDTO)
    {
        var _pagedItem_LineDTO = new PagedResultDTO<Item_LineDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = Item_LineDTO
        };
        _pagedItem_LineDTO.DataList = Item_Line_Service.GetItem_LineList_Global(Item_LineDTO, _pagedItem_LineDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedItem_LineDTO.DataList, loadOptions);
        _dsLoader.totalCount = Item_Line_Service.GetItem_LineTotalCount(_pagedItem_LineDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Item_Line/GetList")]
    public IHttpActionResult GetItem_LineList([FromUri] Item_LineDTO Item_LineDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Item_Line_Service.GetItem_LineList_Global(Item_LineDTO);

        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/Item_Line/GetListBySupportGroup")]
    public IHttpActionResult GetItem_LineListBySupportGroup([FromUri] Item_LineDTO Item_LineDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Item_Line_Service.GetItem_LineListBySupportGroup(Item_LineDTO);

        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/Item_Line/GeMasterDetailtList")]
    public IHttpActionResult GetItem_LineMasterDetail([FromUri] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Item_Line_Service.GetItem_LineWithUserDefined(Item_LineDTO);

        return Json(_validationResultDTO);
    }
    [HttpGet]
    [Route("api/Item_Line/GeMasterDetailByStationList")]
    public IHttpActionResult GetItem_LineMasterDetailByStation([FromUri] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Item_Line_Service.GetItem_LineWithUserDefinedByStation(Item_LineDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Item_Line/Create")]
    public IHttpActionResult CreateItem_Line([FromBody] Item_LineDTO Item_LineDTO)
    {
        //var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_Line), (int)Action_Enum.Create);
        var _validationResultDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO.ID != null ?
            Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_Line), (int)Action_Enum.Create)
            : Auth_Helper.ValidatePermission_Global(nameof(Item_Line), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.CreateItem_Line_Global(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }


    [HttpPost]
    [Route("api/Item_Line/Update")]
    public IHttpActionResult UpdateItem_Line([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.SpecialUpdateItemLineValidation(Item_LineDTO, nameof(Item_Line), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.UpdateItem_Line_Global(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Item_Line/Delete")]
    public IHttpActionResult DeleteItem_Line([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO.ID != null ?
            Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_Line), (int)Action_Enum.Delete)
            : Auth_Helper.ValidatePermission_Global(nameof(Item_Line), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.DeleteItem_Line_Global(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion

    #region Item Station
    [HttpPost]
    [Route("api/Item_Line/CreateItem_Station")]
    public IHttpActionResult CreateItem_Station([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Item_Line), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.CreateItem_StationByArrayGlobal(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Item_Line/UpdateItem_Station")]
    public IHttpActionResult UpdateItem_Station([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_LineDTO.Item_SupportGroupDTO.SupportGroupDTO, nameof(Item_Line), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.UpdateItem_StationGlobal(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion

    #region Reassign Support Group To Item
    [HttpPost]
    [Route("api/Item_Line/ReassignSupportGroup")]
    public IHttpActionResult ReassignSupportGroupToItem([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.SpecialUpdateItemLineValidation(Item_LineDTO, nameof(Item_SupportGroup), (int)Action_Enum.Update); ;
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.ReassignSupportGroupToItemGlobal(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion

    #region item Delivery 
    [HttpPost]
    [Route("api/Item_Line/ItemDelivery")]
    public IHttpActionResult ItemDelivery([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemRegistration), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.ItemDeliveryGlobal(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion
    #region item Delivery 
    [HttpPost]
    [Route("api/Item_Line/ReassignOwner")]
    public IHttpActionResult ReassignOwnerToItem([FromBody] Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = Auth_Helper.SpecialUpdateItemLineValidation(Item_LineDTO, nameof(Item_Line), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            Item_LineDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Item_Line_Service.ReassignOwnerToItemGlobal(Item_LineDTO);
        }
        return Json(_validationResultDTO);
    }
    #endregion
}