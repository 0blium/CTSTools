using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;


namespace CTSTools.BLL.Features.Engineering.ComponentID.DecoderManagement.SubClass_Supplier;

public class SubClass_Supplier_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSubClass_Supplier_Global(SubClass_SupplierDTO SubClass_SupplierDTO)
    {

        var _validationResultDTO = SubClass_Supplier_Validator.CreateSubClass_Supplier_Validation(SubClass_SupplierDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = SubClass_Supplier_Repository.CreateSubClass_Supplier(SubClass_SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateSubClass_Supplier_Global(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = SubClass_Supplier_Validator.UpdateSubClass_Supplier_Validation(SubClass_SupplierDTO);
        if (_validationResultDTO.Result)
        {
            SubClass_SupplierDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = SubClass_Supplier_Repository.UpdateSubClass_Supplier(SubClass_SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteSubClass_Supplier_Global(SubClass_SupplierDTO SubClass_SupplierDTO)
    {
        var _validationResultDTO = SubClass_Supplier_Validator.DeleteSubClass_Supplier_Validation(SubClass_SupplierDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = SubClass_Supplier_Repository.DeleteSubClass_Supplier(SubClass_SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static List<SubClass_SupplierDTO> GetSubClass_SupplierList_Global(SubClass_SupplierDTO SubClass_SupplierDTO, PagedResultDTO<SubClass_SupplierDTO> PagedSubClass_SupplierDTO = null)
    {
        var _subClass_SupplierGlobalList = new List<SubClass_SupplierDTO>();
        try
        {
            var _subClass_SupplierList = SubClass_Supplier_Repository.GetSubClass_SupplierList(SubClass_SupplierDTO, PagedSubClass_SupplierDTO);
            _subClass_SupplierGlobalList = _subClass_SupplierList;
            return _subClass_SupplierGlobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _subClass_SupplierGlobalList;
    }

    public static int GetTotalCount(PagedResultDTO<SubClass_SupplierDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = SubClass_Supplier_Repository.GetSubClass_SupplierCount(PagedResultDTO.Filter, PagedResultDTO);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return PagedResultDTO.TotalCount;
    }
    #endregion

    #region Business Logic

    // Aqui va la logica 

    #endregion
}
