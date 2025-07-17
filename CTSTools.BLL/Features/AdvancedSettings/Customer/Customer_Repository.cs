using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CTSTools.BLL.Features.AdvancedSettings.Customer;

public class Customer_Repository
{
    public static List<CustomerDTO> GetList(CustomerDTO CustomerDTO, PagedResultDTO<CustomerDTO> PagedResultDTO = null)
    {
        var _customerList = new List<CustomerDTO>();
        try
        {
            // Customer Filters
            var _groupOperator = Customer_DXFilter.GetDXFilter(CustomerDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
            if (PagedResultDTO?.dxFilters != null)
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;

            using var _session = XPO_Helper.GetNewSession();
            var _customerCollection = new XPCollection<CustomerXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(CustomerXPO.Oid), SortingDirection.Ascending))
            };
            if (_customerCollection.AsQueryable().Count() > 0)
                _customerList = _customerCollection.Select(CustomerXPO => CustomerMap.XPOToDTO(CustomerXPO)).ToList();
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _customerList;
    }
    public static int GetCount(CustomerDTO CustomerDTO, PagedResultDTO<CustomerDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Customer_DXFilter.GetDXFilter(CustomerDTO);
            if (PagedResultDTO?.dxFilters != null)
                  _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<CustomerXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO Create(CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _customerXPO = CustomerMap.DTOtoXPO(CustomerDTO, _unit);
            _unit.Save(_customerXPO);
            _unit.CommitChanges();
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
    public static ValidationResultDTO Update(CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _customerXPO = CustomerMap.DTOtoXPO(CustomerDTO, _unit);
            _unit.Save(_customerXPO);
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
    public static ValidationResultDTO Delete(CustomerDTO CustomerDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _customerXPO = CustomerMap.DTOtoXPO(CustomerDTO, _unit);
            _unit.Delete(_customerXPO);
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
