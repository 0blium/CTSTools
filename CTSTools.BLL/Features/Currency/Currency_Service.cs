using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Currency;

public class Currency_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateCurrency_Global(CurrencyDTO CurrencyDTO)
    {
        var _ValidationResultDTO = Currency_Validator.CreateCurrency_Validation(CurrencyDTO);
        if (_ValidationResultDTO.Result)
        {
            CurrencyDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Currency_Repository.CreateCurrency(CurrencyDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateCurrency_Global(CurrencyDTO CurrencyDTO)
    {
        var _ValidationResultDTO = Currency_Validator.UpdateCurrency_Validation(CurrencyDTO);
        if (_ValidationResultDTO.Result)
        {
            CurrencyDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Currency_Repository.UpdateCurrency(CurrencyDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteCurrency_Global(CurrencyDTO CurrencyDTO)
    {
        var _ValidationResultDTO = Currency_Validator.DeleteCurrency_Validation(CurrencyDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Currency_Repository.DeleteCurrency(CurrencyDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<CurrencyDTO> GetCurrencyList_Global(CurrencyDTO CurrencyDTO, PagedResultDTO<CurrencyDTO> PagedResultDTO = null)
    {
        var _currencyglobalList = new List<CurrencyDTO>();
        try
        {
            var _currencyList = Currency_Repository.GetCurrencyList(CurrencyDTO, PagedResultDTO);
            // if Currency is empty, return list
            _currencyglobalList = _currencyList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _currencyglobalList;
    }




    public static int GetCurrencyTotalCount(PagedResultDTO<CurrencyDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Currency_Repository.GetCurrencyCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
