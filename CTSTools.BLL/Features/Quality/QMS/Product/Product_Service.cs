using CTSTools.BLL.Common;
using CTSTools.BLL.Features.Quality.QMS.Product;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.Quality.QMS.Product;

public class Product_Service
{
    #region Global CRUD
    public static ValidationResultDTO Create_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.Create_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            ProductDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Product_Repository.Create(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Update_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.Update_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            ProductDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Product_Repository.Update(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO Delete_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.Delete_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Product_Repository.Delete(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ProductDTO> GetList_Global(ProductDTO ProductDTO, PagedResultDTO<ProductDTO> PagedResultDTO = null)
    {
        var _productglobalList = new List<ProductDTO>();
        try
        {
            var _productList = Product_Repository.GetList(ProductDTO, PagedResultDTO);
            // if Product is empty, return list
            _productglobalList = _productList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _productglobalList;
    }


    public static int GetTotalCount(PagedResultDTO<ProductDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Product_Repository.GetCount(PagedResultDTO.Filter, PagedResultDTO);
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
