using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Maintenance.AMS.Item;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Maintenance.AMS.ItemManagement.UserDefined;

public class UserDefined_Repository
{
    public static List<UserDefinedDTO> GetUserDefinedList(UserDefinedDTO UserDefinedDTO, PagedResultDTO<UserDefinedDTO> PagedResultDTO = null)
    {
        var _userdefinedList = new List<UserDefinedDTO>();
        try
        {
            // UserDefined Filters
            var _groupOperator = UserDefined_DXFilter.GetUserDefined_DXFilter(UserDefinedDTO);
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
                var _userdefinedCollection = new XPCollection<UserDefinedXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(UserDefinedXPO.Oid), SortingDirection.Ascending))
                };

                if (_userdefinedCollection.AsQueryable().Count() > 0)
                {
                    _userdefinedList = _userdefinedCollection.Select(UserDefinedXPO => UserDefinedMap.XPOToDTO(UserDefinedXPO)).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userdefinedList;
    }
    public static int GetUserDefinedCount(UserDefinedDTO UserDefinedDTO, PagedResultDTO<UserDefinedDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = UserDefined_DXFilter.GetUserDefined_DXFilter(UserDefinedDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<UserDefinedXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUserDefined(UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedXPO = UserDefinedMap.DTOtoXPO(UserDefinedDTO, _unit);
                _unit.Save(_userdefinedXPO);
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
    public static ValidationResultDTO UpdateUserDefined(UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedXPO = UserDefinedMap.DTOtoXPO(UserDefinedDTO, _unit);
                _unit.Save(_userdefinedXPO);
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
    public static ValidationResultDTO DeleteUserDefined(UserDefinedDTO UserDefinedDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _userdefinedXPO = UserDefinedMap.DTOtoXPO(UserDefinedDTO, _unit);
                _unit.Delete(_userdefinedXPO);
                _unit.CommitChanges();
                _unit.PurgeDeletedObjects();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            if (ex.Message.Contains("Number:547"))
            {
                _validationResultDTO = Exception_Service.GetErrorTypeByMessage(ex.Message);
            }
            else
            {
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to delete the record.");
            }
        }
        return _validationResultDTO;
    }
}
