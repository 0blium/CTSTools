using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Management.Edashboard.KPISettings;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.KPISettings.UnitOfMeasure;

public class UnitOfMeasure_Repository
{
    public static List<UnitOfMeasureDTO> GetUnitOfMeasureList(UnitOfMeasureDTO UnitOfMeasureDTO, PagedResultDTO<UnitOfMeasureDTO> PagedResultDTO = null)
    {
        var _unitofmeasureList = new List<UnitOfMeasureDTO>();
        try
        {
            // UnitOfMeasure Filters
            var _groupOperator = UnitOfMeasure_DXFilter.GetUnitOfMeasure_DXFilter(UnitOfMeasureDTO);
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
                var _unitofmeasureCollection = new XPCollection<UnitOfMeasureXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(UnitOfMeasureXPO.Oid), SortingDirection.Ascending))
                };

                if (_unitofmeasureCollection.AsQueryable().Count() > 0)
                {
                    _unitofmeasureList = _unitofmeasureCollection.Select(UnitOfMeasureXPO => UnitOfMeasureMap.XPOToDTO(UnitOfMeasureXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _unitofmeasureList;
    }
    public static int GetUnitOfMeasureCount(UnitOfMeasureDTO UnitOfMeasureDTO, PagedResultDTO<UnitOfMeasureDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = UnitOfMeasure_DXFilter.GetUnitOfMeasure_DXFilter(UnitOfMeasureDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<UnitOfMeasureXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUnitOfMeasure(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _unitofmeasureXPO = UnitOfMeasureMap.DTOtoXPO(UnitOfMeasureDTO, _unit);
                _unit.Save(_unitofmeasureXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateUnitOfMeasure(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _unitofmeasureXPO = UnitOfMeasureMap.DTOtoXPO(UnitOfMeasureDTO, _unit);
                _unit.Save(_unitofmeasureXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteUnitOfMeasure(UnitOfMeasureDTO UnitOfMeasureDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _unitofmeasureXPO = UnitOfMeasureMap.DTOtoXPO(UnitOfMeasureDTO, _unit);
                _unit.Delete(_unitofmeasureXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            //ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
