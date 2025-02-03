using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

internal class Supplier_Repository
{
    public static List<SupplierDTO> GetSupplierList(SupplierDTO SupplierDTO, PagedResultDTO<SupplierDTO> PagedResultDTO = null)
    {
        var _supplierList = new List<SupplierDTO>();
        try
        {
            // Supplier Filters
            var _groupOperator = Supplier_DXFilter.GetSupplier_DXFilter(SupplierDTO);
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
            var _supplierCollection = new XPCollection<SupplierXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SupplierXPO.Oid), SortingDirection.Ascending))
            };

            if (_supplierCollection.AsQueryable().Count() > 0)
            {
                _supplierList = _supplierCollection.Select(SupplierXPO => SupplierMap.XPOToDTO(SupplierXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _supplierList;
    }
    public static SupplierDTO GetSupplierByID(int SupplierID)
    {
        var _supplierDTO = new SupplierDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _supplierXPO = _unit.GetObjectByKey<SupplierXPO>(SupplierID);
            if (_supplierXPO != null)
                _supplierDTO = SupplierMap.XPOToDTO(_supplierXPO);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _supplierDTO;
    }
    public static int GetSupplierCount(SupplierDTO SupplierDTO, PagedResultDTO<SupplierDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Supplier_DXFilter.GetSupplier_DXFilter(SupplierDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using (var _session = XPO_Helper.GetNewSession())
            {
                return (int)_session.Evaluate<SupplierXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSupplier(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _supplierXPO = SupplierMap.DTOtoXPO(SupplierDTO, _unit);
            _unit.Save(_supplierXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _supplierXPO.Oid;
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
    public static ValidationResultDTO UpdateSupplier(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _supplierXPO = SupplierMap.DTOtoXPO(SupplierDTO, _unit);
                _unit.Save(_supplierXPO);
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
    public static ValidationResultDTO DeleteSupplier(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using (var _unit = XPO_Helper.GetNewUnitOfWork())
            {
                var _supplierXPO = SupplierMap.DTOtoXPO(SupplierDTO, _unit);
                _unit.Delete(_supplierXPO);
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
    public static ValidationResultDTO CreateMultipleSupplier(List<SupplierDTO> SupplierDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _supplierXPOList = SupplierMap.DTOListToXPOList(SupplierDTOList, _unit);
            _unit.Save(_supplierXPOList);
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
}
