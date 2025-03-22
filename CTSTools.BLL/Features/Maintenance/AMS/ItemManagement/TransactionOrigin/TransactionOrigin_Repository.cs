using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.TransactionOrigin;

public class TransactionOrigin_Repository
{
    public static List<TransactionOriginDTO> GetTransactionOriginList(TransactionOriginDTO TransactionOriginDTO, PagedResultDTO<TransactionOriginDTO> PagedResultDTO = null)
    {
        var _transactionoriginList = new List<TransactionOriginDTO>();
        try
        {
            // TransactionOrigin Filters
            var _groupOperator = TransactionOrigin_DXFilter.GetTransactionOrigin_DXFilter(TransactionOriginDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _transactionoriginCollection = new XPCollection<TransactionOriginXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(TransactionOriginXPO.Oid), SortingDirection.Ascending))
                };

                if (_transactionoriginCollection.AsQueryable().Count() > 0)
                {
                    _transactionoriginList = _transactionoriginCollection.Select(TransactionOriginXPO => TransactionOriginMap.XPOToDTO(TransactionOriginXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _transactionoriginList;
    }
    public static int GetTransactionOriginCount(TransactionOriginDTO TransactionOriginDTO, PagedResultDTO<TransactionOriginDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = TransactionOrigin_DXFilter.GetTransactionOrigin_DXFilter(TransactionOriginDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<TransactionOriginXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateTransactionOrigin(TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _transactionoriginXPO = TransactionOriginMap.DTOtoXPO(TransactionOriginDTO, _unit);
                _unit.Save(_transactionoriginXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateTransactionOrigin(TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _transactionoriginXPO = TransactionOriginMap.DTOtoXPO(TransactionOriginDTO, _unit);
                _unit.Save(_transactionoriginXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteTransactionOrigin(TransactionOriginDTO TransactionOriginDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _transactionoriginXPO = TransactionOriginMap.DTOtoXPO(TransactionOriginDTO, _unit);
                _unit.Delete(_transactionoriginXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
