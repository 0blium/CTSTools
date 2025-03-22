using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SparePart;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SparePartManagement.SparePart_Lot;

public class SparePart_Lot_Repository
{
    public static List<SparePart_LotDTO> GetSparePart_LotList(SparePart_LotDTO SparePart_LotDTO, PagedResultDTO<SparePart_LotDTO> PagedResultDTO = null)
    {
        var _sparepart_lotList = new List<SparePart_LotDTO>();
        try
        {
            // SparePart_Lot Filters
            var _groupOperator = SparePart_Lot_DXFilter.GetSparePart_Lot_DXFilter(SparePart_LotDTO);
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
                var _sparepart_lotCollection = new XPCollection<SparePart_LotXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SparePart_LotXPO.Oid), SortingDirection.Ascending))
                };

                if (_sparepart_lotCollection.AsQueryable().Count() > 0)
                {
                    _sparepart_lotList = _sparepart_lotCollection.Select(SparePart_LotXPO => SparePart_LotMap.XPOToDTO(SparePart_LotXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _sparepart_lotList;
    }
    public static int GetSparePart_LotCount(SparePart_LotDTO SparePart_LotDTO, PagedResultDTO<SparePart_LotDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = SparePart_Lot_DXFilter.GetSparePart_Lot_DXFilter(SparePart_LotDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<SparePart_LotXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSparePart_Lot(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepart_lotXPO = SparePart_LotMap.DTOtoXPO(SparePart_LotDTO, _unit);
                _unit.Save(_sparepart_lotXPO);
                _unit.CommitChanges();
                SparePart_LotDTO.ID = _sparepart_lotXPO.Oid;
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
    public static ValidationResultDTO UpdateSparePart_Lot(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepart_lotXPO = SparePart_LotMap.DTOtoXPO(SparePart_LotDTO, _unit);
                _unit.Save(_sparepart_lotXPO);
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
    public static ValidationResultDTO DeleteSparePart_Lot(SparePart_LotDTO SparePart_LotDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _sparepart_lotXPO = SparePart_LotMap.DTOtoXPO(SparePart_LotDTO, _unit);
                _unit.Delete(_sparepart_lotXPO);
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
