using CTSTools.DAL.Features.AdvancedSettings.UserManagement;
using CTSTools.DAL.Features.Quality.QMS;
using DevExpress.Xpo;
using System;

namespace CTSTools.BLL.Features.AdvancedSettings.Product;

public class ProductMap
{
    public static ProductDTO XPOToDTO(ProductXPO ProductXPO)
    {
        var _productDTO = new ProductDTO();
        try
        {
            _productDTO.ID = ProductXPO.Oid;
            _productDTO.Name = ProductXPO.Name;
            _productDTO.Description = ProductXPO.Description;
            _productDTO.AddedDate = (ProductXPO.AddedDate.ToString() != DateTime.MinValue.ToString()) ? ProductXPO.AddedDate : (DateTime?)null;
            _productDTO.AddedByID = (ProductXPO.AddedBy != null) ? ProductXPO.AddedBy.Oid : 0;
            _productDTO.AddedByName = (ProductXPO.AddedBy != null) ? ProductXPO.AddedBy.Name : "Unnassigned";
            _productDTO.LastUpdate = (ProductXPO.LastUpdate.ToString() != DateTime.MinValue.ToString()) ? ProductXPO.LastUpdate : (DateTime?)null;
            _productDTO.LastUpdateByID = (ProductXPO.LastUpdateBy != null) ? ProductXPO.LastUpdateBy.Oid : 0;
            _productDTO.LastUpdateByName = (ProductXPO.LastUpdateBy != null) ? ProductXPO.LastUpdateBy.Name : "Unnassigned";
            _productDTO.IsActive = ProductXPO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _productDTO;
    }

    public static ProductXPO DTOtoXPO(ProductDTO ProductDTO, UnitOfWork UnitOfWork)
    {
        ProductXPO _productXPO;
        try
        {
            _productXPO = ProductDTO.ID == null || ProductDTO.ID == 0 ? new ProductXPO(UnitOfWork) : UnitOfWork.GetObjectByKey<ProductXPO>(ProductDTO.ID);
            _productXPO.Name = _productXPO.Name == ProductDTO.Name ? _productXPO.Name : ProductDTO.Name;
            _productXPO.Description = _productXPO.Description == ProductDTO.Description ? _productXPO.Description : ProductDTO.Description;
            _productXPO.AddedDate = _productXPO.AddedDate != null ? _productXPO.AddedDate : ProductDTO.AddedDate;
            _productXPO.AddedBy = (_productXPO.AddedBy != null) ? _productXPO.AddedBy : UnitOfWork.GetObjectByKey<UserXPO>(ProductDTO.AddedByID);
            _productXPO.LastUpdate = _productXPO.LastUpdate == ProductDTO.LastUpdate ? _productXPO.LastUpdate : ProductDTO.LastUpdate;
            _productXPO.LastUpdateBy = (_productXPO.LastUpdateBy != null && _productXPO.LastUpdateBy.Oid == ProductDTO.LastUpdateByID) ? _productXPO.LastUpdateBy : UnitOfWork.GetObjectByKey<UserXPO>(ProductDTO.LastUpdateByID);
            _productXPO.IsActive = _productXPO.IsActive == ProductDTO.IsActive ? (bool)_productXPO.IsActive : (bool)ProductDTO.IsActive;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return _productXPO;
    }
}
