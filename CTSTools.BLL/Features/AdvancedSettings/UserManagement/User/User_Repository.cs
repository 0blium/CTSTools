using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.AdvancedSettings.UserManagement.User;
public class User_Repository
{
    public static List<UserDTO> GetUserList(UserDTO UserDTO, PagedResultDTO<UserDTO> PagedResultDTO = null)
    {
        var _userList = new List<UserDTO>();
        try
        {
            // User Filters
            var _groupOperator = User_DXFilter.GetUserFilters(UserDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                //Sorting
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);

            if (PagedResultDTO?.dxFilters != null)
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;

            using var _session = XPO_Helper.GetNewSession();
            var _userCollection = new XPCollection<UserXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(UserXPO.Oid), SortingDirection.Ascending))
            };

            if (_userCollection.AsQueryable().Count() > 0)
                _userList = _userCollection.Select(UserXPO => UserMap.XPOToDTO(UserXPO)).ToList();
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _userList;
    }
    public static int GetUserCount(UserDTO UserDTO, PagedResultDTO<UserDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = User_DXFilter.GetUserFilters(UserDTO);
            if (PagedResultDTO?.dxFilters != null)            
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<UserXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateUser(UserDTO UserDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _userXPO = UserMap.DTOtoXPO(UserDTO, _unit);
            _unit.Save(_userXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _userXPO.Oid;
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
    public static ValidationResultDTO UpdateUser(UserDTO UserDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _userXPO = UserMap.DTOtoXPO(UserDTO, _unit);
            _unit.Save(_userXPO);
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
    public static ValidationResultDTO DeleteUser(UserDTO UserDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _userXPO = UserMap.DTOtoXPO(UserDTO, _unit);
            _unit.Delete(_userXPO);
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
