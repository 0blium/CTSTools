using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.Ticket.Item.Item_SupportGroup;

public class Item_SupportGroup_Repository
{
    public static List<Item_SupportGroupDTO> GetItem_SupportGroupList(Item_SupportGroupDTO Item_SupportGroupDTO, PagedResultDTO<Item_SupportGroupDTO> PagedResultDTO = null)
    {
        var _item_supportgroupList = new List<Item_SupportGroupDTO>();
        try
        {
            // Item_SupportGroup Filters
            var _groupOperator = Item_SupportGroup_DXFilter.GetItem_SupportGroup_DXFilter(Item_SupportGroupDTO);
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
                var _item_supportgroupCollection = new XPCollection<Item_SupportGroupXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(Item_SupportGroupXPO.Oid), SortingDirection.Ascending))
                };

                if (_item_supportgroupCollection.AsQueryable().Count() > 0)
                {
                    _item_supportgroupList = _item_supportgroupCollection.Select(Item_SupportGroupXPO => Item_SupportGroupMap.XPOToDTO(Item_SupportGroupXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _item_supportgroupList;
    }
    public static int GetItem_SupportGroupCount(Item_SupportGroupDTO Item_SupportGroupDTO, PagedResultDTO<Item_SupportGroupDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Item_SupportGroup_DXFilter.GetItem_SupportGroup_DXFilter(Item_SupportGroupDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<Item_SupportGroupXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateItem_SupportGroup(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_supportgroupXPO = Item_SupportGroupMap.DTOtoXPO(Item_SupportGroupDTO, _unit);
                _unit.Save(_item_supportgroupXPO);
                _unit.CommitChanges();
                _validationResultDTO.Data = _item_supportgroupXPO.Oid;
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
    public static ValidationResultDTO UpdateItem_SupportGroup(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_supportgroupXPO = Item_SupportGroupMap.DTOtoXPO(Item_SupportGroupDTO, _unit);
                _unit.Save(_item_supportgroupXPO);
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
    public static ValidationResultDTO DeleteItem_SupportGroup(Item_SupportGroupDTO Item_SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _item_supportgroupXPO = Item_SupportGroupMap.DTOtoXPO(Item_SupportGroupDTO, _unit);
                _unit.Delete(_item_supportgroupXPO);
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
