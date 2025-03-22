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

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.Item_Line;

public class Item_Line_Repository
{
    public static List<Item_LineDTO> GetItem_LineList(Item_LineDTO Item_LineDTO, PagedResultDTO<Item_LineDTO> PagedResultDTO = null)
    {
        var _item_lineList = new List<Item_LineDTO>();
        try
        {
            // Item_Line Filters
            var _groupOperator = Item_Line_DXFilter.GetItem_Line_DXFilter(Item_LineDTO);
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
                var _item_lineCollection = new XPCollection<Item_LineXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(Item_LineXPO.Oid), SortingDirection.Ascending))
                };

                if (_item_lineCollection.AsQueryable().Count() > 0)
                {
                    _item_lineList = _item_lineCollection.Select(Item_LineXPO => Item_LineMap.XPOToDTO(Item_LineXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_lineList;
    }
    public static int GetItem_LineCount(Item_LineDTO Item_LineDTO, PagedResultDTO<Item_LineDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Item_Line_DXFilter.GetItem_Line_DXFilter(Item_LineDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<Item_LineXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateItem_Line(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_lineXPO = Item_LineMap.DTOtoXPO(Item_LineDTO, _unit);
                _unit.Save(_item_lineXPO);
                _unit.CommitChanges();
                Item_LineDTO.ID = _item_lineXPO.Oid;
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
    public static ValidationResultDTO UpdateItem_Line(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_lineXPO = Item_LineMap.DTOtoXPO(Item_LineDTO, _unit);
                _unit.Save(_item_lineXPO);
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
    public static ValidationResultDTO DeleteItem_Line(Item_LineDTO Item_LineDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_lineXPO = Item_LineMap.DTOtoXPO(Item_LineDTO, _unit);
                _unit.Delete(_item_lineXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            if (ex.Message.Contains("Number:547"))
            {
                _validationResultDTO = Exception_Service.GetErrorTypeByMessage(ex.Message);
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to delete the record.");
            }
        }
        return _validationResultDTO;
    }
}
