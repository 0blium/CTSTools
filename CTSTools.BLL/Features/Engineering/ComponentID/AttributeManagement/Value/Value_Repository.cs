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

namespace CTSTools.BLL.Features.Engineering.ComponentID.AttributeManagement.Value;

public class Value_Repository
{
    public static List<ValueDTO> GetValueList(ValueDTO ValueDTO, PagedResultDTO<ValueDTO> PagedResultDTO = null)
    {
        var _valueList = new List<ValueDTO>();
        try
        {
            // Value Filters
            var _groupOperator = Value_DXFilter.GetValue_DXFilter(ValueDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);

            if (PagedResultDTO?.dxFilters != null)     
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            if (ValueDTO.ValueIDArray.Count() > 2000 && PagedResultDTO?.dxFilters == null)
            {
                var _groupOperatorList = DXFilters_Helper.SplitIDArrayToGroupOperator(ValueDTO.ValueIDArray);
                foreach (var _subGroupOperator in _groupOperatorList)
                    _valueList.AddRange(GetXPOCollection(_subGroupOperator, _sortProperty, PagedResultDTO));
            }
            else
                _valueList = GetXPOCollection(_groupOperator, _sortProperty, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _valueList;
    }
    public static List<ValueDTO> GetXPOCollection(GroupOperator GroupOperator, SortProperty SortProperty, PagedResultDTO<ValueDTO> PagedResultDTO = null)
    {

        var _valueList = new List<ValueDTO>();
        try
        {
            using (var _session = XPO_Helper.GetNewSession())
            {
                var _valueCollection = new XPCollection<ValueXPO>(_session, GroupOperator, SortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(SortProperty) : new SortingCollection(new SortProperty(nameof(ValueXPO.Oid), SortingDirection.Ascending))
                };

                if (_valueCollection.AsQueryable().Count() > 0)
                {
                    _valueList = _valueCollection.Select(ValueXPO => ValueMap.XPOToDTO(ValueXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueList;
    }
    public static ValueDTO GetValueByID(int ValueID)
    {
        var _valueDTO = new ValueDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valueXPO = _unit.GetObjectByKey<ValueXPO>(ValueID);
            if (_valueXPO != null)
                _valueDTO = ValueMap.XPOToDTO(_valueXPO);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _valueDTO;
    }

    public static int GetValueCount(ValueDTO ValueDTO, PagedResultDTO<ValueDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Value_DXFilter.GetValue_DXFilter(ValueDTO);
            if (PagedResultDTO?.dxFilters != null)
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<ValueXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateValue(ValueDTO ValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valueXPO = ValueMap.DTOtoXPO(ValueDTO, _unit);
            _unit.Save(_valueXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _valueXPO.Oid;
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
    public static ValidationResultDTO UpdateValue(ValueDTO ValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valueXPO = ValueMap.DTOtoXPO(ValueDTO, _unit);
            _unit.Save(_valueXPO);
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
    public static ValidationResultDTO DeleteValue(ValueDTO ValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _valueXPO = ValueMap.DTOtoXPO(ValueDTO, _unit);
            _unit.Delete(_valueXPO);
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
    public static ValidationResultDTO CreateMultiple(List<ValueDTO> ValueDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = ValueMap.DTOListToXPOList(ValueDTOList, _unit);
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
    public static ValidationResultDTO DeleteMultiple(List<ValueDTO> ValueDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = ValueMap.DTOListToXPOList(ValueDTOList, _unit);
            _unit.Delete(_xPOList);
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
}
