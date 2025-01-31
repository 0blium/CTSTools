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
    public static ValidationResultDTO CreateProduct_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.CreateProduct_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            ProductDTO.AddedDate = DateTime.Now;
            _ValidationResultDTO = Product_Repository.CreateProduct(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO UpdateProduct_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.UpdateProduct_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            ProductDTO.LastUpdate = DateTime.Now;
            _ValidationResultDTO = Product_Repository.UpdateProduct(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static ValidationResultDTO DeleteProduct_Global(ProductDTO ProductDTO)
    {
        var _ValidationResultDTO = Product_Validator.DeleteProduct_Validation(ProductDTO);
        if (_ValidationResultDTO.Result)
        {
            _ValidationResultDTO = Product_Repository.DeleteProduct(ProductDTO);
        }
        return _ValidationResultDTO;
    }
    public static List<ProductDTO> GetProductList_Global(ProductDTO ProductDTO, PagedResultDTO<ProductDTO> PagedResultDTO = null)
    {
        var _productglobalList = new List<ProductDTO>();
        try
        {
            var _productList = Product_Repository.GetProductList(ProductDTO, PagedResultDTO);
            // if Product is empty, return list
            _productglobalList = _productList;


        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _productglobalList;
    }


    public static int GetProductTotalCount(PagedResultDTO<ProductDTO> PagedResultDTO)
    {
        try
        {
            //Get Total Count
            PagedResultDTO.TotalCount = Product_Repository.GetProductCount(PagedResultDTO.Filter, PagedResultDTO);
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
