using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.StatusManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.StatusManagement.Status_StatusType;

public class Status_StatusType_Repository
{
    public static List<Status_StatusTypeDTO> GetStatus_StatusTypeList(Status_StatusTypeDTO Status_StatusTypeDTO, PagedResultDTO<Status_StatusTypeDTO> PagedResultDTO = null)
    {
        var _status_statustypeList = new List<Status_StatusTypeDTO>();
        try
        {
            // Status_StatusType Filters
            var _groupOperator = Status_StatusType_DXFilter.GetStatus_StatusTypeFilters(Status_StatusTypeDTO);
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
                var _status_statustypeCollection = new XPCollection<Status_StatusTypeXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(Status_StatusTypeXPO.Oid), SortingDirection.Ascending))
                };

                if (_status_statustypeCollection.AsQueryable().Count() > 0)
                {
                    _status_statustypeList = _status_statustypeCollection.Select(Status_StatusTypeXPO => Status_StatusTypeMap.XPOToDTO(Status_StatusTypeXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _status_statustypeList;
    }
    public static int GetStatus_StatusTypeCount(Status_StatusTypeDTO Status_StatusTypeDTO, PagedResultDTO<Status_StatusTypeDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Status_StatusType_DXFilter.GetStatus_StatusTypeFilters(Status_StatusTypeDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<Status_StatusTypeXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateStatus_StatusType(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _status_statustypeXPO = Status_StatusTypeMap.DTOtoXPO(Status_StatusTypeDTO, _unit);
                _unit.Save(_status_statustypeXPO);
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
    public static ValidationResultDTO UpdateStatus_StatusType(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _status_statustypeXPO = Status_StatusTypeMap.DTOtoXPO(Status_StatusTypeDTO, _unit);
                _unit.Save(_status_statustypeXPO);
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
    public static ValidationResultDTO DeleteStatus_StatusType(Status_StatusTypeDTO Status_StatusTypeDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _status_statustypeXPO = Status_StatusTypeMap.DTOtoXPO(Status_StatusTypeDTO, _unit);
                _unit.Delete(_status_statustypeXPO);
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
