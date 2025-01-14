using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.SecurityManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;

namespace CTSTools.BLL.Features.AdvancedSettings.SecurityManagement.Module;

public class Module_Repository
{
    public static List<ModuleDTO> GetModuleList(ModuleDTO ModuleDTO, PagedResultDTO<ModuleDTO> PagedResultDTO = null)
    {
        var _ModuleList = new List<ModuleDTO>();
        try
        {
            // Module Filters
            var _groupOperator = Module_DXFilter.GetModule_DXFilter(ModuleDTO);
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
                var _ModuleCollection = new XPCollection<ModuleXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ModuleXPO.Oid), SortingDirection.Ascending))
                };

                if (_ModuleCollection.AsQueryable().Count() > 0)
                {
                    _ModuleList = _ModuleCollection.Select(ModuleXPO => ModuleMap.XPOToDTO(ModuleXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _ModuleList;
    }
    public static int GetModuleCount(ModuleDTO ModuleDTO, PagedResultDTO<ModuleDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Module_DXFilter.GetModule_DXFilter(ModuleDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<ModuleXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateModule(ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ModuleXPO = ModuleMap.DTOtoXPO(ModuleDTO, _unit);
                _unit.Save(_ModuleXPO);
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
    public static ValidationResultDTO UpdateModule(ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ModuleXPO = ModuleMap.DTOtoXPO(ModuleDTO, _unit);
                _unit.Save(_ModuleXPO);
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
    public static ValidationResultDTO DeleteModule(ModuleDTO ModuleDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _ModuleXPO = ModuleMap.DTOtoXPO(ModuleDTO, _unit);
                _unit.Delete(_ModuleXPO);
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
