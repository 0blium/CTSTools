using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.DecoderManagement;
using CTSTools.DAL.Features.Engineering.ComponentID.SupplierManagement;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;

public class SubClass_Supplier_Repository
{
    public static List<SubClass_SupplierDTO> GetSubClass_SupplierList(SubClass_SupplierDTO SubClass_SupplierDTO, PagedResultDTO<SubClass_SupplierDTO> PagedResultDTO = null)
    {
        var _subClass_SupplierList = new List<SubClass_SupplierDTO>();
        try
        {
            // Supplier Filters
            var _groupOperator = SubClass_Supplier_DXFilter.GetSubClass_Supplier_DXFilter(SubClass_SupplierDTO);
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
            var _subClass_SupplierCollection = new XPCollection<SubClass_SupplierXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(SubClass_SupplierXPO.Oid), SortingDirection.Ascending))
            };

            if (_subClass_SupplierCollection.AsQueryable().Count() > 0)
            {
                _subClass_SupplierList = _subClass_SupplierCollection.Select(SubClass_SupplierXPO => SubClass_SupplierMap.XPOToDTO(SubClass_SupplierXPO)).ToList();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _subClass_SupplierList;
    }
    public static SubClass_SupplierDTO GetSubClass_SupplierByID(int SubClass_SupplierID)
    {
        var _subClass_SupplierDTO = new SubClass_SupplierDTO();
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _subClass_SupplierXPO = _unit.GetObjectByKey<SubClass_SupplierXPO>(SubClass_SupplierID);
            if (_subClass_SupplierXPO != null)
                _subClass_SupplierDTO = SubClass_SupplierMap.XPOToDTO(_subClass_SupplierXPO);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _subClass_SupplierDTO;
    }
    public static int GetSubClass_SupplierCount(SubClass_SupplierDTO SubClass_SupplierDTO, PagedResultDTO<SubClass_SupplierDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = SubClass_Supplier_DXFilter.GetSubClass_Supplier_DXFilter(SubClass_SupplierDTO);
            if (PagedResultDTO?.dxFilters != null)
            {
                //DevExtreme Filter
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
            }
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<SupplierXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO CreateSubClass_Supplier(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _subclass_supplierXPO = SubClass_SupplierMap.DTOtoXPO(SubClass_SupplierDTO, _unit);
            _unit.Save(_subclass_supplierXPO);
            _unit.CommitChanges();
            _validationResultDTO.Data = _subclass_supplierXPO.Oid;
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
    public static ValidationResultDTO UpdateSubClass_Supplier(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _subclass_supplierXPO = SubClass_SupplierMap.DTOtoXPO(SubClass_SupplierDTO, _unit);
            _unit.Save(_subclass_supplierXPO);
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
    public static ValidationResultDTO DeleteSubClass_Supplier(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _subclass_supplierXPO = SubClass_SupplierMap.DTOtoXPO(SubClass_SupplierDTO, _unit);
            _unit.Delete(_subclass_supplierXPO);
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
    public static ValidationResultDTO CreateMultiple(List<SubClass_SupplierDTO> SubClass_SupplierDTOList)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _xPOList = SubClass_SupplierMap.DTOListToXPOList(SubClass_SupplierDTOList, _unit);
            _unit.Save(_xPOList);
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
