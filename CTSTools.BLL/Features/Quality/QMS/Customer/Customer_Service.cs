using CTSTools.BLL.Common;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.Customer;

public class Customer_Service
{
    #region Global CRUD
    public static ValidationResultDTO CreateCustomer_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.CreateCustomer_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            CustomerDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Customer_Repository.CreateCustomer(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateCustomer_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.UpdateCustomer_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            CustomerDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Customer_Repository.UpdateCustomer(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteCustomer_Global(CustomerDTO CustomerDTO)
    {
        var _ValidationResultDTO = Customer_Validator.DeleteCustomer_Validation(CustomerDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Customer_Repository.DeleteCustomer(CustomerDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<CustomerDTO> GetCustomerList_Global(CustomerDTO CustomerDTO, PagedResultDTO<CustomerDTO> PagedResultDTO = null)
    {
        var _CustomerglobalList = new List<CustomerDTO>();
        try
        {
            var _CustomerList = Customer_Repository.GetCustomerList(CustomerDTO, PagedResultDTO);
            // if Customer is empty, return list
            _CustomerglobalList = _CustomerList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _CustomerglobalList;
    }


    public static int GetCustomerTotalCount(PagedResultDTO<CustomerDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Customer_Repository.GetCustomerCount(PagedResultDTO.Filter, PagedResultDTO);
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
