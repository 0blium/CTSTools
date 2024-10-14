using System;
using System.Collections.Generic;
using System.Linq;
using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.MailGroupManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;


namespace CTSTools.BLL.Features.AdvancedSettings.MailGroupManagement.MailGroup;

public class MailGroup_Repository
{
    public static List<MailGroupDTO> GetMailGroupList(MailGroupDTO MailGroupDTO, PagedResultDTO<MailGroupDTO> PagedResultDTO = null)
    {
        var _mailgroupList = new List<MailGroupDTO>();
        try
        {
            // MailGroup Filters
            var _groupOperator = MailGroup_DXFilter.GetMailGroup_DXFilter(MailGroupDTO);
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
                var _mailgroupCollection = new XPCollection<MailGroupXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(MailGroupXPO.Oid), SortingDirection.Ascending))
                };

                if (_mailgroupCollection.AsQueryable().Count() > 0)
                {
                    _mailgroupList = _mailgroupCollection.Select(MailGroupXPO => MailGroupMap.XPOToDTO(MailGroupXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _mailgroupList;
    }
    public static int GetMailGroupCount(MailGroupDTO MailGroupDTO,PagedResultDTO<MailGroupDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = MailGroup_DXFilter.GetMailGroup_DXFilter(MailGroupDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<MailGroupXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateMailGroup(MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _mailgroupXPO = MailGroupMap.DTOtoXPO(MailGroupDTO, _unit);
                _unit.Save(_mailgroupXPO);
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
    public static ValidationResultDTO UpdateMailGroup(MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _mailgroupXPO = MailGroupMap.DTOtoXPO(MailGroupDTO, _unit);
                _unit.Save(_mailgroupXPO);
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
    public static ValidationResultDTO DeleteMailGroup(MailGroupDTO MailGroupDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _mailgroupXPO = MailGroupMap.DTOtoXPO(MailGroupDTO, _unit);
                _unit.Delete(_mailgroupXPO);
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
