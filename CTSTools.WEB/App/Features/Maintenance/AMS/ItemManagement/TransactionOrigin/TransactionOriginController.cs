using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;

public class TransactionOriginController : ApiController
{
    [HttpGet]
    [Route("api/TransactionOrigin/GetPagedList")]
    public IHttpActionResult GetTransactionOriginPagedList(DataSourceLoadOptions loadOptions, [FromUri] TransactionOriginDTO TransactionOriginDTO)
    {
        var _pagedTransactionOriginDTO = new PagedResultDTO<TransactionOriginDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = TransactionOriginDTO
        };
        _pagedTransactionOriginDTO.DataList = TransactionOrigin_Service.GetTransactionOriginList_Global(TransactionOriginDTO, _pagedTransactionOriginDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedTransactionOriginDTO.DataList, loadOptions);
        _dsLoader.totalCount = TransactionOrigin_Service.GetTransactionOriginTotalCount(_pagedTransactionOriginDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/TransactionOrigin/GetList")]
    public IHttpActionResult GetTransactionOriginList([FromUri] TransactionOriginDTO TransactionOriginDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = TransactionOrigin_Service.GetTransactionOriginList_Global(TransactionOriginDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/TransactionOrigin/Create")]
    public IHttpActionResult CreateTransactionOrigin([FromBody] TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(TransactionOrigin), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            TransactionOriginDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = TransactionOrigin_Service.CreateTransactionOrigin_Global(TransactionOriginDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/TransactionOrigin/Update")]
    public IHttpActionResult UpdateTransactionOrigin([FromBody] TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(TransactionOrigin), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            TransactionOriginDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = TransactionOrigin_Service.UpdateTransactionOrigin_Global(TransactionOriginDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/TransactionOrigin/Delete")]
    public IHttpActionResult DeleteTransactionOrigin([FromBody] TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(TransactionOrigin), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = TransactionOrigin_Service.DeleteTransactionOrigin_Global(TransactionOriginDTO);
        }
        return Json(_validationResultDTO);
    }
}