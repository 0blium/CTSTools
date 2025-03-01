using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePartInventory;

public class SparePartInventory_Repository
{
    public static List<SparePartInventoryDTO> GetSparePartInventoryList(SparePartInventoryDTO SparePartInventoryDTO, PagedResultDTO<SparePartInventoryDTO> PagedResultDTO = null)
    {
        var _sparepartinventoryList = new List<SparePartInventoryDTO>();
        try
        {
            // SparePartInventory Filters
            var _groupOperator = SparePartInventory_DXFilter.GetSparePartInventory_DXFilter(SparePartInventoryDTO);
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
                var _sparepartinventoryCollection = new XPCollection<SparePartInventoryXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SparePartInventoryXPO.Oid), SortingDirection.Ascending))
                };

                if (_sparepartinventoryCollection.AsQueryable().Count() > 0)
                {
                    _sparepartinventoryList = _sparepartinventoryCollection.Select(SparePartInventoryXPO => SparePartInventoryMap.XPOToDTO(SparePartInventoryXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartinventoryList;
    }
    public static int GetSparePartInventoryCount(SparePartInventoryDTO SparePartInventoryDTO, PagedResultDTO<SparePartInventoryDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = SparePartInventory_DXFilter.GetSparePartInventory_DXFilter(SparePartInventoryDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<SparePartInventoryXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSparePartInventory(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartinventoryXPO = SparePartInventoryMap.DTOtoXPO(SparePartInventoryDTO, _unit);
                _unit.Save(_sparepartinventoryXPO);
                _unit.CommitChanges();
                SparePartInventoryDTO.ID = _sparepartinventoryXPO.Oid;
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
    public static ValidationResultDTO UpdateSparePartInventory(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartinventoryXPO = SparePartInventoryMap.DTOtoXPO(SparePartInventoryDTO, _unit);
                _unit.Save(_sparepartinventoryXPO);
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
    public static ValidationResultDTO DeleteSparePartInventory(SparePartInventoryDTO SparePartInventoryDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartinventoryXPO = SparePartInventoryMap.DTOtoXPO(SparePartInventoryDTO, _unit);
                _unit.Delete(_sparepartinventoryXPO);
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
