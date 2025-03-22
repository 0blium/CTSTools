using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Ticket.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefinedValue;

public class UserDefinedValue_Repository
{
    public static List<UserDefinedValueDTO> GetUserDefinedValueList(UserDefinedValueDTO UserDefinedValueDTO, PagedResultDTO<UserDefinedValueDTO> PagedResultDTO = null)
    {
        var _userdefinedvalueList = new List<UserDefinedValueDTO>();
        try
        {
            // UserDefinedValue Filters
            var _groupOperator = UserDefinedValue_DXFilter.GetUserDefinedValue_DXFilter(UserDefinedValueDTO);
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
                var _userdefinedvalueCollection = new XPCollection<UserDefinedValueXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(UserDefinedValueXPO.Oid), SortingDirection.Ascending))
                };

                if (_userdefinedvalueCollection.AsQueryable().Count() > 0)
                {
                    _userdefinedvalueList = _userdefinedvalueCollection.Select(UserDefinedValueXPO => UserDefinedValueMap.XPOToDTO(UserDefinedValueXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedvalueList;
    }
    public static int GetUserDefinedValueCount(UserDefinedValueDTO UserDefinedValueDTO, PagedResultDTO<UserDefinedValueDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = UserDefinedValue_DXFilter.GetUserDefinedValue_DXFilter(UserDefinedValueDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<UserDefinedValueXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUserDefinedValue(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedvalueXPO = UserDefinedValueMap.DTOtoXPO(UserDefinedValueDTO, _unit);
                _unit.Save(_userdefinedvalueXPO);
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
    public static ValidationResultDTO UpdateUserDefinedValue(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedvalueXPO = UserDefinedValueMap.DTOtoXPO(UserDefinedValueDTO, _unit);
                _unit.Save(_userdefinedvalueXPO);
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
    public static ValidationResultDTO DeleteUserDefinedValue(UserDefinedValueDTO UserDefinedValueDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedvalueXPO = UserDefinedValueMap.DTOtoXPO(UserDefinedValueDTO, _unit);
                _unit.Delete(_userdefinedvalueXPO);
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
