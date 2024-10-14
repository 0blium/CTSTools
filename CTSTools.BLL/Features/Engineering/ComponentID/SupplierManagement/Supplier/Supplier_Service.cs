using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Engineering.ComponentID.SupplierManagement.Supplier;

public class Supplier_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateSupplier_Global(SupplierDTO SupplierDTO)
    {

        var _validationResultDTO = Supplier_Validator.CreateSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Supplier_Repository.CreateSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO UpdateSupplier_Global(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Supplier_Validator.UpdateSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            SupplierDTO.LastUpdate = DateTime.Now;
            _validationResultDTO = Supplier_Repository.UpdateSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static ValidationResultDTO DeleteSupplier_Global(SupplierDTO SupplierDTO)
    {
        var _validationResultDTO = Supplier_Validator.DeleteSupplier_Validation(SupplierDTO);
        if (_validationResultDTO.Result)
        {
            _validationResultDTO = Supplier_Repository.DeleteSupplier(SupplierDTO);
        }
        return _validationResultDTO;
    }
    public static List<SupplierDTO> GetSupplierList_Global(SupplierDTO SupplierDTO, PagedResultDTO<SupplierDTO> PagedSupplierDTO = null)
    {
        var _facilityGlobalList = new List<SupplierDTO>();
        try
        {
            var _facilityList = Supplier_Repository.GetSupplierList(SupplierDTO, PagedSupplierDTO);
            _facilityGlobalList = _facilityList;
            return _facilityGlobalList;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _facilityGlobalList;
    }

    public static int GetTotalCount(PagedResultDTO<SupplierDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Supplier_Repository.GetSupplierCount(PagedResultDTO.Filter, PagedResultDTO);
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
