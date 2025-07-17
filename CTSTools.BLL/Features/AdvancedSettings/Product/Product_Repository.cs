using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo.DB;

namespace CTSTools.BLL.Features.AdvancedSettings.Product;

public class Product_Repository
{
    public static List<ProductDTO> GetList(ProductDTO ProductDTO, PagedResultDTO<ProductDTO> PagedResultDTO = null)
    {
        var _productList = new List<ProductDTO>();
        try
        {
            // Product Filters
            var _groupOperator = Product_DXFilter.GetDXFilter(ProductDTO);
            var _sortProperty = new SortProperty();
            if (PagedResultDTO?.SortPropertyName != null)
                _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);

            if (PagedResultDTO?.dxFilters != null)
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;

            using var _session = XPO_Helper.GetNewSession();
            var _productCollection = new XPCollection<ProductXPO>(_session, _groupOperator, _sortProperty)
            {
                TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ProductXPO.Oid), SortingDirection.Ascending))
            };

            if (_productCollection.AsQueryable().Count() > 0)
                _productList = _productCollection.Select(ProductXPO => ProductMap.XPOToDTO(ProductXPO)).ToList();
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return _productList;
    }
    public static int GetCount(ProductDTO ProductDTO, PagedResultDTO<ProductDTO> PagedResultDTO = null)
    {
        try
        {
            var _groupOperator = Product_DXFilter.GetDXFilter(ProductDTO);
            if (PagedResultDTO?.dxFilters != null)
                _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO)); ;
            using var _session = XPO_Helper.GetNewSession();
            return (int)_session.Evaluate<ProductXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static ValidationResultDTO Create(ProductDTO ProductDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been saved successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _productXPO = ProductMap.DTOtoXPO(ProductDTO, _unit);
            _unit.Save(_productXPO);
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
    public static ValidationResultDTO Update(ProductDTO ProductDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been updated successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _productXPO = ProductMap.DTOtoXPO(ProductDTO, _unit);
            _unit.Save(_productXPO);
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
    public static ValidationResultDTO Delete(ProductDTO ProductDTO)
    {
        var _validationResultDTO = new ValidationResultDTO
        {
            Description = "The record has been deleted successfully."
        };
        try
        {
            using var _unit = XPO_Helper.GetNewUnitOfWork();
            var _productXPO = ProductMap.DTOtoXPO(ProductDTO, _unit);
            _unit.Delete(_productXPO);
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
