using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.ItemClassification;

public class ItemClassification_Repository
{
    public static List<ItemClassificationDTO> GetItemClassificationList(ItemClassificationDTO ItemClassificationDTO, PagedResultDTO<ItemClassificationDTO> PagedResultDTO = null)
    {
        var _itemclassificationList = new List<ItemClassificationDTO>();
        try
        {
            // ItemClassification Filters
            var _groupOperator = ItemClassification_DXFilter.GetItemClassification_DXFilter(ItemClassificationDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
            {
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            }
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }

            using (var _session = XPO_Helper.GetNewSession())
            {
                var _itemclassificationCollection = new XPCollection<ItemClassificationXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ItemClassificationXPO.Oid), SortingDirection.Ascending))
                };

                if (_itemclassificationCollection.AsQueryable().Count() > 0)
                {
                    _itemclassificationList = _itemclassificationCollection.Select(ItemClassificationXPO => ItemClassificationMap.XPOToDTO(ItemClassificationXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _itemclassificationList;
    }
    public static int GetItemClassificationCount(ItemClassificationDTO ItemClassificationDTO, PagedResultDTO<ItemClassificationDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = ItemClassification_DXFilter.GetItemClassification_DXFilter(ItemClassificationDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<ItemClassificationXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateItemClassification(ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _itemclassificationXPO = ItemClassificationMap.DTOtoXPO(ItemClassificationDTO, _unit);
                _unit.Save(_itemclassificationXPO);
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
    public static ValidationResultDTO UpdateItemClassification(ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _itemclassificationXPO = ItemClassificationMap.DTOtoXPO(ItemClassificationDTO, _unit);
                _unit.Save(_itemclassificationXPO);
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
    public static ValidationResultDTO DeleteItemClassification(ItemClassificationDTO ItemClassificationDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _itemclassificationXPO = ItemClassificationMap.DTOtoXPO(ItemClassificationDTO, _unit);
                _unit.Delete(_itemclassificationXPO);
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
