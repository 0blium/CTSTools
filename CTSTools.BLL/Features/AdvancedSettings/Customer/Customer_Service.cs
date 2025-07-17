using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;

namespace CTSTools.BLL.Features.AdvancedSettings.Customer;

public class Customer_Service
{
    #region Global CRUD
    public static ValidationResultDTO Create_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.Create_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            CustomerDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Customer_Repository.Create(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Update_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.Update_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            CustomerDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Customer_Repository.Update(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Delete_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.Delete_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Customer_Repository.Delete(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<CustomerDTO> GetList_Global(CustomerDTO CustomerDTO, PagedResultDTO<CustomerDTO> PagedResultDTO = null)
    {
        var _CustomerglobalList = new List<CustomerDTO>();
        try
        {
            var _CustomerList = Customer_Repository.GetList(CustomerDTO, PagedResultDTO);
            // if Customer is empty, return list
            _CustomerglobalList = _CustomerList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _CustomerglobalList;
    }


    public static int GetTotalCount(PagedResultDTO<CustomerDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Customer_Repository.GetCount(PagedResultDTO.Filter, PagedResultDTO);
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
