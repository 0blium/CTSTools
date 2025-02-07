using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.AttributeManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.ValueLink;
public class ValueLink_Repository
{
    public static List<ValueLinkDTO> GetValueLinkList(ValueLinkDTO ValueLinkDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        var _valuelinkList = new List<ValueLinkDTO>();
        try
        {
            // ValueLink Filters
            var _groupOperator = ValueLink_DXFilter.GetValueLink_DXFilter(ValueLinkDTO);
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
            using var _session = XPO_Helper.GetNewSession();
            var _valuelinkCollection = new XPCollection<ValueLinkXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ValueLinkXPO.Oid), SortingDirection.Ascending))
            };
            if (_valuelinkCollection.AsQueryable().Count() > 0)
            {
                _valuelinkList = _valuelinkCollection.Select(ValueLinkXPO => ValueLinkMap.XPOToDTO(ValueLinkXPO))
                                                     .ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valuelinkList;
    }
    public static int GetValueLinkCount(ValueLinkDTO ValueLinkDTO, PagedResultDTO<ValueLinkDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = ValueLink_DXFilter.GetValueLink_DXFilter(ValueLinkDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<ValueLinkXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    public static ValidationResultDTO CreateValueLink(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valuelinkXPO = ValueLinkMap.DTOtoXPO(ValueLinkDTO, _unit);
            _unit.Save(_valuelinkXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _valuelinkXPO.Oid;
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
    public static ValidationResultDTO UpdateValueLink(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valuelinkXPO = ValueLinkMap.DTOtoXPO(ValueLinkDTO, _unit);
            _unit.Save(_valuelinkXPO);
            _unit.CommitChanges();

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
    public static ValidationResultDTO DeleteValueLink(ValueLinkDTO ValueLinkDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valuelinkXPO = ValueLinkMap.DTOtoXPO(ValueLinkDTO, _unit);
            _unit.Delete(_valuelinkXPO);
            _unit.CommitChanges();
            _unit.PurgeDeletedObjects();

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
    public static ValidationResultDTO DeleteMultipleValueLink(List<ValueLinkDTO> ValueLinkList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();

            var _valueLinkXPCollection = ValueLinkMap.DTOListToXPOList(ValueLinkList, _unit);
            _unit.Delete(_valueLinkXPCollection);
            _unit.CommitChanges();
            _unit.PurgeDeletedObjects();

        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            _validationResultDTO.Result = false;
            _validationResultDTO.Message = "Error!";
            _validationResultDTO.Description = string.Format("There was an error trying to delete the record.");
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO CreateMultiple(List<ValueLinkDTO> ValueLinkDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = ValueLinkMap.DTOListToXPOList(ValueLinkDTOList, _unit);
            _unit.Save(_xPOList);
            _unit.CommitChanges();
            _validationResultDTO.Data = _xPOList;
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

