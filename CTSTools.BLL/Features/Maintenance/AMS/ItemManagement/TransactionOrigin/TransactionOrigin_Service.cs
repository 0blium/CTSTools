using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;

public class TransactionOrigin_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateTransactionOrigin_Global(TransactionOriginDTO TransactionOriginDTO)
    {
        var _ValidationResultDTO = TransactionOrigin_Validator.CreateTransactionOrigin_Validation(TransactionOriginDTO);
        if (_ValidationResultDTO.Result)
        {
            TransactionOriginDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = TransactionOrigin_Repository.CreateTransactionOrigin(TransactionOriginDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateTransactionOrigin_Global(TransactionOriginDTO TransactionOriginDTO)
    {
        var _ValidationResultDTO = TransactionOrigin_Validator.UpdateTransactionOrigin_Validation(TransactionOriginDTO);
        if (_ValidationResultDTO.Result)
        {
            TransactionOriginDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = TransactionOrigin_Repository.UpdateTransactionOrigin(TransactionOriginDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteTransactionOrigin_Global(TransactionOriginDTO TransactionOriginDTO)
    {
        var _ValidationResultDTO = TransactionOrigin_Validator.DeleteTransactionOrigin_Validation(TransactionOriginDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = TransactionOrigin_Repository.DeleteTransactionOrigin(TransactionOriginDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<TransactionOriginDTO> GetTransactionOriginList_Global(TransactionOriginDTO TransactionOriginDTO, PagedResultDTO<TransactionOriginDTO> PagedResultDTO = null)
    {
        var _transactionoriginglobalList = new List<TransactionOriginDTO>();
        try
        {
            var _transactionoriginList = TransactionOrigin_Repository.GetTransactionOriginList(TransactionOriginDTO, PagedResultDTO);
            // if TransactionOrigin is empty, return list
            _transactionoriginglobalList = _transactionoriginList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _transactionoriginglobalList;
    }

    public static int GetTransactionOriginTotalCount(PagedResultDTO<TransactionOriginDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = TransactionOrigin_Repository.GetTransactionOriginCount(PagedResultDTO.Filter, PagedResultDTO);
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

