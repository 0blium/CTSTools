using CTSTools.BLL.Common.Files;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Ticket.Item.Item_Header;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Ticket.Item.ItemAdministration.ItemHeader
{

    public class Item_HeaderController : ApiController
    {
        #region CRUD
        [HttpGet]
        [Route("api/Item_Header/GetPagedList")]
        public IHttpActionResult GetItem_HeaderPagedList(DataSourceLoadOptions loadOptions, [FromUri] Item_HeaderDTO Item_HeaderDTO)
        {
            var _pagedItem_HeaderDTO = new PagedResultDTO<Item_HeaderDTO>()
            {
                Skip = loadOptions.Skip,
                Take = loadOptions.Take,
                dxFilters = loadOptions.Filter,
                SortDescending = loadOptions.Sort?[0]?.Desc,
                SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
                Filter = Item_HeaderDTO
            };
            _pagedItem_HeaderDTO.DataList = Item_Header_Service.GetItem_HeaderList_Global(Item_HeaderDTO, _pagedItem_HeaderDTO);

            loadOptions.Skip = 0;

            var _dsLoader = DataSourceLoader.Load(_pagedItem_HeaderDTO.DataList, loadOptions);
            _dsLoader.totalCount = Item_Header_Service.GetItem_HeaderTotalCount(_pagedItem_HeaderDTO);
            return Json(_dsLoader);
        }

        [HttpGet]
        [Route("api/Item_Header/GetList")]
        public IHttpActionResult GetItem_HeaderList([FromUri] Item_HeaderDTO Item_HeaderDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = Item_Header_Service.GetItem_HeaderList_Global(Item_HeaderDTO);

            return Json(_validationResultDTO);
        }


        [HttpPost]
        [Route("api/Item_Header/Create")]
        public IHttpActionResult CreateItem_Header([FromBody] Item_HeaderDTO Item_HeaderDTO)
        {
            //var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_HeaderDTO.SupportGroupDTO, nameof(ItemHeader), (int)Action_Enum.Create);
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemHeader), (int)Action_Enum.Create);
            if (_validationResultDTO.Result)
            {
                Item_HeaderDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Item_Header_Service.CreateItem_Header_Global(Item_HeaderDTO);
            }
            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/Item_Header/Update")]
        public IHttpActionResult UpdateItem_Header([FromBody] Item_HeaderDTO Item_HeaderDTO)
        {
            //var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_HeaderDTO.SupportGroupDTO, nameof(ItemHeader), (int)Action_Enum.Update);
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemHeader), (int)Action_Enum.Update);
            if (_validationResultDTO.Result)
            {
                Item_HeaderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Item_Header_Service.UpdateItem_Header_Global(Item_HeaderDTO);
            }
            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/Item_Header/Delete")]
        public IHttpActionResult DeleteItem_Header([FromBody] Item_HeaderDTO Item_HeaderDTO)
        {
            //var _validationResultDTO = Auth_Helper.ValidateSupportGroupMemberPermissions_Global(Item_HeaderDTO.SupportGroupDTO, nameof(ItemHeader), (int)Action_Enum.Delete);
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(ItemHeader), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                Item_HeaderDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
                _validationResultDTO = Item_Header_Service.DeleteItem_Header_Global(Item_HeaderDTO);
            }
            return Json(_validationResultDTO);
        }

        #endregion

        #region Files
        [HttpGet]
        [Route("api/Item_Header/GetFileList")]
        public IHttpActionResult GetItem_HeaderFileList([FromUri] Item_HeaderDTO Item_HeaderDTO)
        {

            var _validationResultDTO = new ValidationResultDTO();
            _validationResultDTO.Data = Item_Header_Service.GetItem_HeaderFileList(Item_HeaderDTO);

            return Json(_validationResultDTO);
        }

        [HttpPost]
        [Route("api/Item_Header/FileDelete")]
        public IHttpActionResult DeleteItem_HeaderFile([FromBody] FileDTO FileDTO)
        {
            var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(BLL.Common.Files), (int)Action_Enum.Delete);
            if (_validationResultDTO.Result)
            {
                _validationResultDTO = Item_Header_Service.DeleteItemHeaderFile(FileDTO);
            }
            return Json(_validationResultDTO);
        }
        #endregion
    }
}