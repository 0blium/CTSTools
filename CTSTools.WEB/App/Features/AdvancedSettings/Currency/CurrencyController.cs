using CTSTools.BLL.Common;
using CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Action;
using CTSTools.BLL.Features.Currency;
using CTSTools.WEB.App_Start;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CTSTools.WEB.App.Features.AdvancedSettings.Currency;

public class CurrencyController : ApiController
{
    [HttpGet]
    [Route("api/Currency/GetPagedList")]
    public IHttpActionResult GetCurrencyPagedList(DataSourceLoadOptions loadOptions, [FromUri] CurrencyDTO CurrencyDTO)
    {
        var _pagedCurrencyDTO = new PagedResultDTO<CurrencyDTO>()
        {
            Skip = loadOptions.Skip,
            Take = loadOptions.Take,
            dxFilters = loadOptions.Filter,
            SortDescending = loadOptions.Sort?[0]?.Desc,
            SortPropertyName = loadOptions.Sort?[0].Selector.Replace("DTO", ""),
            Filter = CurrencyDTO
        };
        _pagedCurrencyDTO.DataList = Currency_Service.GetCurrencyList_Global(CurrencyDTO, _pagedCurrencyDTO);

        loadOptions.Skip = 0;

        var _dsLoader = DataSourceLoader.Load(_pagedCurrencyDTO.DataList, loadOptions);
        _dsLoader.totalCount = Currency_Service.GetCurrencyTotalCount(_pagedCurrencyDTO);
        return Json(_dsLoader);
    }

    [HttpGet]
    [Route("api/Currency/GetList")]
    public IHttpActionResult GetCurrencyList([FromUri] CurrencyDTO CurrencyDTO)
    {

        var _validationResultDTO = new ValidationResultDTO();
        _validationResultDTO.Data = Currency_Service.GetCurrencyList_Global(CurrencyDTO);

        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Currency/Create")]
    public IHttpActionResult CreateCurrency([FromBody] CurrencyDTO CurrencyDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Currency), (int)Action_Enum.Create);
        if (_validationResultDTO.Result)
        {
            CurrencyDTO.AddedByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Currency_Service.CreateCurrency_Global(CurrencyDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Currency/Update")]
    public IHttpActionResult UpdateCurrency([FromBody] CurrencyDTO CurrencyDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Currency), (int)Action_Enum.Update);
        if (_validationResultDTO.Result)
        {
            CurrencyDTO.LastUpdateByID = Auth_Helper.GetLoggedUserOid();
            _validationResultDTO = Currency_Service.UpdateCurrency_Global(CurrencyDTO);
        }
        return Json(_validationResultDTO);
    }

    [HttpPost]
    [Route("api/Currency/Delete")]
    public IHttpActionResult DeleteCurrency([FromBody] CurrencyDTO CurrencyDTO)
    {
        var _validationResultDTO = Auth_Helper.ValidatePermission_Global(nameof(Currency), (int)Action_Enum.Delete);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Currency_Service.DeleteCurrency_Global(CurrencyDTO);
        }
        return Json(_validationResultDTO);
    }
}