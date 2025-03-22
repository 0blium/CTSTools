using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.SupportGroup;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.SupportGroupManagement.SupportGroup;

public class SupportGroup_Repository
{
    public static List<SupportGroupDTO> GetSupportGroupList(SupportGroupDTO SupportGroupDTO, PagedResultDTO<SupportGroupDTO> PagedResultDTO = null)
    {
        var _supportgroupList = new List<SupportGroupDTO>();
        try
        {
            // SupportGroup Filters
            var _groupOperator = SupportGroup_DXFilter.GetSupportGroup_DXFilter(SupportGroupDTO);
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
                var _supportgroupCollection = new XPCollection<SupportGroupXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SupportGroupXPO.Oid), SortingDirection.Ascending))
                };

                if (_supportgroupCollection.AsQueryable().Count() > 0)
                {
                    _supportgroupList = _supportgroupCollection.Select(SupportGroupXPO => SupportGroupMap.XPOToDTO(SupportGroupXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _supportgroupList;
    }
    public static int GetSupportGroupCount(SupportGroupDTO SupportGroupDTO, PagedResultDTO<SupportGroupDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = SupportGroup_DXFilter.GetSupportGroup_DXFilter(SupportGroupDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<SupportGroupXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSupportGroup(SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _supportgroupXPO = SupportGroupMap.DTOtoXPO(SupportGroupDTO, _unit);
                _unit.Save(_supportgroupXPO);
                _unit.CommitChanges();
                SupportGroupDTO.ID = _supportgroupXPO.Oid;
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
    public static ValidationResultDTO UpdateSupportGroup(SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _supportgroupXPO = SupportGroupMap.DTOtoXPO(SupportGroupDTO, _unit);
                _unit.Save(_supportgroupXPO);
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
    public static ValidationResultDTO DeleteSupportGroup(SupportGroupDTO SupportGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _supportgroupXPO = SupportGroupMap.DTOtoXPO(SupportGroupDTO, _unit);
                _unit.Delete(_supportgroupXPO);
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
