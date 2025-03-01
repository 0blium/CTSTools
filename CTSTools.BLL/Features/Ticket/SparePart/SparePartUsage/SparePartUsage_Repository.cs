using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.Ticket.SparePart.SparePartUsage;

public class SparePartUsage_Repository
{
    public static List<SparePartUsageDTO> GetSparePartUsageList(SparePartUsageDTO SparePartUsageDTO, PagedResultDTO<SparePartUsageDTO> PagedResultDTO = null)
    {
        var _sparepartusageList = new List<SparePartUsageDTO>();
        try
        {
            // SparePartUsage Filters
            var _groupOperator = SparePartUsage_DXFilter.GetSparePartUsage_DXFilter(SparePartUsageDTO);
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
                var _sparepartusageCollection = new XPCollection<SparePartUsageXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SparePartUsageXPO.Oid), SortingDirection.Ascending))
                };

                if (_sparepartusageCollection.AsQueryable().Count() > 0)
                {
                    _sparepartusageList = _sparepartusageCollection.Select(SparePartUsageXPO => SparePartUsageMap.XPOToDTO(SparePartUsageXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepartusageList;
    }
    public static int GetSparePartUsageCount(SparePartUsageDTO SparePartUsageDTO, PagedResultDTO<SparePartUsageDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = SparePartUsage_DXFilter.GetSparePartUsage_DXFilter(SparePartUsageDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<SparePartUsageXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSparePartUsage(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartusageXPO = SparePartUsageMap.DTOtoXPO(SparePartUsageDTO, _unit);
                _unit.Save(_sparepartusageXPO);
                _unit.CommitChanges();
                SparePartUsageDTO.ID = _sparepartusageXPO.Oid;
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
    public static ValidationResultDTO UpdateSparePartUsage(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartusageXPO = SparePartUsageMap.DTOtoXPO(SparePartUsageDTO, _unit);
                _unit.Save(_sparepartusageXPO);
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
    public static ValidationResultDTO DeleteSparePartUsage(SparePartUsageDTO SparePartUsageDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepartusageXPO = SparePartUsageMap.DTOtoXPO(SparePartUsageDTO, _unit);
                _unit.Delete(_sparepartusageXPO);
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
