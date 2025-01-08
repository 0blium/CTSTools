using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Management.Edashboard.Settings;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Management.Edashboard.Settings.Equivalence;

public class Equivalence_Repository
{
    public static List<EquivalenceDTO> GetEquivalenceList(EquivalenceDTO EquivalenceDTO, PagedResultDTO<EquivalenceDTO> PagedResultDTO = null)
    {
        var _equivalenceList = new List<EquivalenceDTO>();
        try
        {
            // Equivalence Filters
            var _groupOperator = Equivalence_DXFilter.GetEquivalence_DXFilter(EquivalenceDTO);
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
                var _equivalenceCollection = new XPCollection<EquivalenceXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(EquivalenceXPO.Oid), SortingDirection.Ascending))
                };

                if (_equivalenceCollection.AsQueryable().Count() > 0)
                {
                    _equivalenceList = _equivalenceCollection.Select(EquivalenceXPO => EquivalenceMap.XPOToDTO(EquivalenceXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return _equivalenceList;
    }
    public static int GetEquivalenceCount(EquivalenceDTO EquivalenceDTO, PagedResultDTO<EquivalenceDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Equivalence_DXFilter.GetEquivalence_DXFilter(EquivalenceDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<EquivalenceXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateEquivalence(EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _equivalenceXPO = EquivalenceMap.DTOtoXPO(EquivalenceDTO, _unit);
                _unit.Save(_equivalenceXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {

            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateEquivalence(EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _equivalenceXPO = EquivalenceMap.DTOtoXPO(EquivalenceDTO, _unit);
                _unit.Save(_equivalenceXPO);
                _unit.CommitChanges();
            }
        }
        catch (Exception ex)
        {

            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteEquivalence(EquivalenceDTO EquivalenceDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _equivalenceXPO = EquivalenceMap.DTOtoXPO(EquivalenceDTO, _unit);
                _unit.Delete(_equivalenceXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {

            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
        }
        return _validationResultDTO;
    }
}
